using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Data;
using UserManagement.Application.Common.Mappings;

namespace UserManagement.ApplicationTests
{
    public class BaseFixture
    {
        public IMapper Mapper { get; }
        public ILogger Logger { get; }
        public IDbConnection DBConnection { get; }        

        public BaseFixture()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            this.Mapper = configurationProvider.CreateMapper();
        }
    }
}