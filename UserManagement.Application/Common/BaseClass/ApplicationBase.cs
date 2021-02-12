using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Domain.UnitOfWork;

namespace UserManagement.Application.Common.BaseClass
{
    public class ApplicationBase
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IConfigConstants ConfigConstants { get; set; }
        public IMapper Mapper { get; set; }
        public IDistributedCache Cache { get; set; }

        public ApplicationBase(IConfigConstants configConstants, IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache cache)
        {
            ConfigConstants = configConstants;
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            Cache = cache;
        }
    }
}