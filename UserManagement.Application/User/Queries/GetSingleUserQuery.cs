﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Common.BaseClass;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Application.User.DTO;
using UserManagement.Application.User.VM;
using UserManagement.Domain.UnitOfWork;

namespace UserManagement.Application.User.Queries
{
    public class GetSingleUserQuery : IRequest<UserVM>
    {
        public int UserID { get; set; }
        public class GetSingleUserHandler : ApplicationBase, IRequestHandler<GetSingleUserQuery, UserVM>
        {
            public GetSingleUserHandler(IConfigConstants constant, IMapper mapper, IUnitOfWork unitOfWork, IDistributedCache cache)
                : base(constant, unitOfWork, mapper, cache)
            {
            }

            public async Task<UserVM> Handle(GetSingleUserQuery request, CancellationToken cancellationToken)
            {
                var res = this.Mapper.Map(this.UnitOfWork.Users.GetUser(request.UserID).Result, new UserDTO());
                return await Task.FromResult(new UserVM() { UserList = new List<UserDTO> { res } });
            }
        }
    }
}