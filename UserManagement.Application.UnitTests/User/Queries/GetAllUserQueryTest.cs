using AutoMapper;
using Microsoft.Extensions.Logging;
using Shouldly;
using System.Linq;
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
    public class GetAllUserQueryTest
    {
        private readonly IConfigConstants constant;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly IUnitOfWork unitOfWork;

        public GetAllUserQueryTest(UserFixture userFixture)
        {
            constant = userFixture.Constant;
            mapper = userFixture.Mapper;
            logger = userFixture.Logger;
            unitOfWork = userFixture.UnitOfWork;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectVM()
        {
            var query = new GetAllUserQuery();
            var handler = new GetAllUserQuery.GetAllUserHandler(constant, mapper, unitOfWork);
            var result = await handler.Handle(query, CancellationToken.None);
            result.ShouldBeOfType<UserVM>();
        }

        [Fact]
        public async Task Handle_ReturnTwoRecords_WhenRun()
        {
            var query = new GetAllUserQuery();
            var handler = new GetAllUserQuery.GetAllUserHandler(constant, mapper, unitOfWork);
            var result = await handler.Handle(query, CancellationToken.None);
            result.UserList.Count().ShouldBe(2);
        }
    }
}