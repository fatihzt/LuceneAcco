using LuceneAcco.Business.Contracts;
using LuceneAcco.Business.Dto;
using LuceneAcco.RedisCache.Abstractions;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneAcco.RedisCache.Services
{
    public class RedisCache : IRedisCache
    {
        private readonly IDatabase db;
        private readonly bool isConnected;
        private readonly IAccommodationsService _service;

        public bool IsConnected => isConnected;
        public RedisCache(IConfiguration configuration, IAccommodationsService service)
        {
            db = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"))?.GetDatabase();
            if (db == null) throw new Exception("saffa");
            isConnected = true;
            _service = service;
        }
        public IDatabase GetDb()
        {
            return db;
        }
        static string GenerateCacheKey(string key)
        {
            string generated = $"DSS:{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?[..3]}:AP:{key}";
            return generated;
        }

        public async Task<bool> AddAccommodationsHash()
        {
            var cacheKey = GenerateCacheKey("Accommodation");
            Stopwatch stopwatch = Stopwatch.StartNew();

            var accommodations = await _service.GetAllAccommodations();

            foreach (var accommodation in accommodations)
            {
                db.HashSet(cacheKey, new HashEntry[] { new HashEntry(accommodation.AccommodationCode, accommodation.AccommodationName) });
            }
            var elapsedTime = stopwatch.Elapsed.TotalMilliseconds;
            return true;
        }

        public async Task<List<AccommodationGetDto>> GetAllAccommodationsHash()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var key = GenerateCacheKey("Accommodation");
            var hashEntries = db.HashGetAll(key);
            var accommodations = hashEntries.Select(e => new AccommodationGetDto
            {
                AccommodationCode = e.Name.ToString(),
                AccommodationName = e.Value.ToString(),
            }).ToList();
            var elapsedTime = stopwatch.Elapsed.TotalMilliseconds;

            return accommodations;
        }
    }
}
