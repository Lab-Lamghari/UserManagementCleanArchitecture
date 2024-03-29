﻿using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Application.User.Commands;
using UserManagement.Domain.UnitOfWork;
using Xunit;

namespace UserManagement.ApplicationTests.User.Commands
{
    [Collection("UserCollection")]
    public class UpdateUserCommandTest
    {
        private readonly IConfigConstants constant;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDistributedCache cache;        

        public UpdateUserCommandTest(UserFixture userFixture)
        {
            constant = userFixture.Constant;
            mapper = userFixture.Mapper;
            unitOfWork = userFixture.UnitOfWork;
            cache = userFixture.Cache;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectVM()
        {
            var command = new UpdateUserCommand
            {
                UserID = 100,
                City = "SpringField",
                Country = "USA",
                State = "VA",
                Zip = "66006",
                PhoneNumber = "888-88-8888",
            };

            var handler = new UpdateUserCommand.UpdateUserHandler(constant, mapper, unitOfWork, cache);
            var result = await handler.Handle(command, CancellationToken.None);
            result.ShouldBeOfType<bool>();
        }

        [Fact]
        public async Task Handle_ReturnTrue_WhenSendCorrectPayloadIsSent()
        {
            var command = new UpdateUserCommand
            {
                UserID = 100,
                City = "SpringField",
                Country = "USA",
                State = "VA",
                Zip = "66006",
                PhoneNumber = "888-88-8888",
            };

            var handler = new UpdateUserCommand.UpdateUserHandler(constant, mapper, unitOfWork, cache);
            var result = await handler.Handle(command, CancellationToken.None);
            result.ShouldBe(true);
        }
    }
}