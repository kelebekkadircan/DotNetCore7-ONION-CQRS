﻿using HepsiApi.Application.Interfaces.RedisCache;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Infrastructure.RedisCache
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private readonly IDatabase _database;
        private readonly RedisCacheSettings _settings;

        public RedisCacheService(IOptions<RedisCacheSettings> options)
        {
            _settings = options.Value;
            ConfigurationOptions opt = ConfigurationOptions.Parse(_settings.ConnectionString);
            _redisConnection = ConnectionMultiplexer.Connect(opt);
            _database = _redisConnection.GetDatabase();

            
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if ( value.HasValue)
                return JsonConvert.DeserializeObject<T>(value);

            return default;
        }

        public async Task SetAsync<T>(string key, T value, DateTime? expirationTime = null)
        {
            TimeSpan timeUnitExpiration = expirationTime.HasValue ? expirationTime.Value - DateTime.Now : TimeSpan.Zero;
            bool result = await _database.StringSetAsync(key, JsonConvert.SerializeObject(value), timeUnitExpiration);
            if (!result)
            {
                throw new Exception("Cache set operation failed");
            }
            
        }
    }
}
