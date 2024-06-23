

using System;
using System.Collections.Concurrent;
using Microsoft.Identity.Client;

namespace eShop.ServiceDefaults;
public class CacheService
{
    private readonly ConcurrentDictionary<string, (object Data, DateTime Expiration)> _cache 
        = new ConcurrentDictionary<string, (object Data, DateTime Expiration)>();

    public void Set(string key, object data, TimeSpan duration)
    {
        var expiration = DateTime.UtcNow.Add(duration);
        _cache[key] = (data, expiration);
    }

    public T? Get<T>(string key)
    {
        if (_cache.TryGetValue(key, out var cacheEntry) && cacheEntry.Expiration > DateTime.UtcNow)
        {
            return (T)cacheEntry.Data;
        }
        return default(T);
    }

    public void Remove(string key)
    {
        _cache.TryRemove(key, out _);
    }

    public int Count { get{return _cache.Count;}  }
}
