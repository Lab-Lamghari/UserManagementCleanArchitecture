﻿using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Domain.UnitOfWork;
using UserManagement.Persistence.Constant;

namespace UserManagement.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            services.AddSingleton<IConfigConstants, ConfigConstants>();
            services.AddSingleton<IDbConnection>(conn => new SqlConnection(conn.GetService<IConfigConstants>().FullStackConnection));
            services.AddTransient<IUnitOfWork>(uof => new UnitOfWork.UnitOfWork(uof.GetService<IDbConnection>()));
            return services;
        }
    }
}