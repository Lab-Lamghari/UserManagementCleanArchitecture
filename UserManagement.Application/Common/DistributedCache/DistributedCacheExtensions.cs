using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;

namespace UserManagement.Application.Common.DistributedCache
{
    public static class DistributedCacheExtensions
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache, string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? inactiveExpireTime = null)
        {
            Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration configuration = Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration.CreateDefault();
            var telemetryClient = new TelemetryClient(configuration);

            var options = new DistributedCacheEntryOptions();

            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = inactiveExpireTime;

            var startTime = DateTime.UtcNow;
            var timer = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                var jsonData = JsonConvert.SerializeObject(data);
                await cache.SetStringAsync(recordId, jsonData, options);
            }
            finally
            {
                timer.Stop();
                telemetryClient.InstrumentationKey = "c52736c3-79bd-4bcf-bb97-d16524cb38be";
                telemetryClient.Context.Cloud.RoleName = "WebAPI";
                telemetryClient.TrackDependency("REDIS", "RedisSetStringAsync", recordId, startTime, timer.Elapsed, true);

            }
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration configuration = Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration.CreateDefault();
            var telemetryClient = new TelemetryClient(configuration);

            string jsonData = null;

            var startTime = DateTime.UtcNow;
            var timer = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                jsonData = await cache.GetStringAsync(recordId);
            }
            finally
            {
                timer.Stop();
                telemetryClient.InstrumentationKey = "c52736c3-79bd-4bcf-bb97-d16524cb38be";
                telemetryClient.Context.Cloud.RoleName = "WebAPI";
                telemetryClient.TrackDependency("REDIS", "RedisGetStringAsync", recordId + " : " + jsonData, startTime, timer.Elapsed, true);
            }           

            if (jsonData is null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(jsonData);
        }
    }
}
