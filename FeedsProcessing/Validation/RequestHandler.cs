using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;

namespace FeedsProcessing.Validation
{
    public interface IRequestHandler
    {
        public bool IsValid(string requestId);
        public void Save(string requestId);
    }

    public class RequestHandler : IRequestHandler
    {
        private readonly ILogger<IRequestHandler> _logger;

        private const string CacheKeyFormat = "_REQUEST_ID_{0}";
        private static readonly TimeSpan Expiration = TimeSpan.FromMinutes(1);
        private readonly IMemoryCache _cache;

        public RequestHandler(IMemoryCache cache, ILogger<IRequestHandler> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public bool IsValid(string requestId)
        {
            var key = string.Format(CacheKeyFormat, requestId);
            return !_cache.TryGetValue(key, out _);
        }

        public void Save(string requestId)
        {
            _logger.LogInformation("Adding request Id to cache.");
            var key = string.Format(CacheKeyFormat, requestId);

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSize(1)
                .SetAbsoluteExpiration(Expiration);

            _cache.Set(key, true, cacheOptions);
        }


    }
}
