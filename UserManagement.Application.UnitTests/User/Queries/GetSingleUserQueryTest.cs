﻿using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Application.User.Queries;
using UserManagement.Application.User.VM;
using UserManagement.Domain.UnitOfWork;
using Xunit;

namespace UserManagement.ApplicationTests.User.Queries
{
    [Collection("UserCollection")]
    public class GetSingleUserQueryTest
    {
        private readonly IConfigConstants constant;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDistributedCache cache;

        public GetSingleUserQueryTest(UserFixture userFixture)
        {
            constant = userFixture.Constant;
            mapper = userFixture.Mapper;
            unitOfWork = userFixture.UnitOfWork;
            cache = userFixture.Cache;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectVM()
        {
            var query = new GetSingleUserQuery
            {
                UserID = 110,
            };

            var handler = new GetSingleUserQuery.GetSingleUserHandler(constant, mapper, unitOfWork, cache);
            var result = await handler.Handle(query, CancellationToken.None);
            result.ShouldBeOfType<UserVM>();
        }

        [Fact]
        public async Task Handle_ReturnCorrectAge_WhenDOBIsProvided()
        {
            var query = new GetSingleUserQuery
            {
                UserID = 110,
            };

            var handler = new GetSingleUserQuery.GetSingleUserHandler(constant, mapper, unitOfWork, cache);
            var result = await handler.Handle(query, CancellationToken.None);
            result.UserList[0].Age.ShouldBe(22);
        }

        [Fact]
        public async Task Handle_ReturnCorrectsalutation_WhenGenderIsProvided()
        {
            var query = new GetSingleUserQuery
            {
                UserID = 100,
            };

            var handler = new GetSingleUserQuery.GetSingleUserHandler(constant, mapper, unitOfWork, cache);
            var result = await handler.Handle(query, CancellationToken.None);
            result.UserList[0].Salutation.ShouldContain("Sir");
        }
    }
}