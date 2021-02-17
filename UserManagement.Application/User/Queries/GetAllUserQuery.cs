using AutoMapper;
using MediatR;

using UserManagement.Application.Common.DistributedCache;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Common.BaseClass;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Application.User.DTO;
using UserManagement.Application.User.VM;
using UserManagement.Domain.UnitOfWork;
using Microsoft.Extensions.Caching.Distributed;

namespace UserManagement.Application.User.Queries
{
    public class GetAllUserQuery : IRequest<UserVM>
    {
        public class GetAllUserHandler : ApplicationBase, IRequestHandler<GetAllUserQuery, UserVM>
        {          
            public GetAllUserHandler(IConfigConstants constant, IMapper mapper, IUnitOfWork unitOfWork, IDistributedCache cache)
                : base(constant, unitOfWork, mapper, cache)
            {                
            }

            public async Task<UserVM> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
            {                
                string recordKey = "GetAllUserQuery_" + DateTime.Now.ToString("ddMMyyyy"); 
                
                var cacheRes = await Cache.GetRecordAsync<List<UserDTO>>(recordKey);

                if(cacheRes != null)
                {
                    return await Task.FromResult(new UserVM() { UserList = cacheRes });
                }
                else
                {
                    var res = Mapper.Map(UnitOfWork.Users.GetAllUsers().Result, new List<UserDTO>());
                    
                    await Cache.SetRecordAsync<List<UserDTO>>(recordKey, res);
                    
                    return await Task.FromResult(new UserVM() { UserList = res });
                }
            }
        }
    }
}