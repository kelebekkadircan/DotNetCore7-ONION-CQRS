using HepsiApi.Application.Interfaces.RedisCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Behaviors
{
    public class RedisCacheBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IRedisCacheService _redisCacheService;

        public RedisCacheBehavior(IRedisCacheService redisCacheService)
        {
            _redisCacheService = redisCacheService;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is ICacheableQuery query)
            {
                string cacheKey = query.CacheKey;
                double cacheTime = query.CacheTime;

                var cachedData = await _redisCacheService.GetAsync<TResponse>(cacheKey);
                if (cachedData is not null)
                    return cachedData;

                var response = await next();
                if (response is not null)
                    await _redisCacheService.SetAsync(cacheKey, response, DateTime.Now.AddMinutes(cacheTime));

                return response;
            }
            return await next();

        }
    }


    //public class RedisCacheBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    //{
    //    private readonly IRedisCacheService _redisCacheService;

    //    public RedisCacheBehavior(IRedisCacheService redisCacheService)
    //    {
    //        _redisCacheService = redisCacheService;
    //    }

    //    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    //    {
    //        // Önbelleklenebilir bir sorgu mu?
    //        if (request is ICacheableQuery query)
    //        {
    //            // Önbellek anahtarı ve süresi alınıyor
    //            string cacheKey = query.CacheKey ?? throw new ArgumentNullException(nameof(query.CacheKey), "Cache anahtarı geçerli olmalıdır.");
    //            double cacheTime = query.CacheTime;

    //            // Redis'ten veri getiriliyor
    //            var cachedData = await _redisCacheService.GetAsync<TResponse>(cacheKey);
    //            if (cachedData is not null)
    //            {
    //                return cachedData; // Önbellekten döndür
    //            }

    //            // İşlem yap ve sonucu Redis'e yaz
    //            var response = await next();
    //            if (response is not null)
    //            {
    //                                await _redisCacheService.SetAsync(cacheKey, response, DateTime.Now.AddMinutes(cacheTime));
    //            }
    //            return response;
    //        }

    //        // Önbelleklenemeyen durumlarda devam et
    //        return await next();
    //    }
    //}


}
