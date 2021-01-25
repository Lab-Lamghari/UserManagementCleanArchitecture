using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UserManagement.API.Checks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private Random rnd = new Random();

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
        {
            int responseTime = rnd.Next(1, 500);

            if (responseTime < 150)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy($"The response time : { responseTime }")
                    );
            }
            else if (responseTime < 300)
            {
                return Task.FromResult(
                    HealthCheckResult.Degraded($"The response time : { responseTime }")
                    );
            }
            else
            {
                return Task.FromResult(
                    HealthCheckResult.Unhealthy($"The response time { responseTime }")
                    );
            }
        }
    }
}
