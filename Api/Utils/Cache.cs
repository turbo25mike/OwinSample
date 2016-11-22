using Amazon.ElastiCacheCluster;
using Enyim.Caching;
using Enyim.Caching.Memcached;
using System;
using Business;

namespace Api.Utils
{
    public interface ICache {
        string SetItem(User user);
        User GetItem(string token);
    }

    public class Cache : ICache
    {
        private MemcachedClient _MemClient;
        public Cache()
        {
            // instantiate a new client.
            var config = new ElastiCacheClusterConfig();
            _MemClient = new MemcachedClient(config);
        }

        public string SetItem(User user)
        {
            Guid token = Guid.NewGuid();
            TimeSpan freshness = new TimeSpan(0, 0, 20, 0);
            
            // Store the data for 20 min. in the cluster. 
            // The client will decide which cache host will store this item.
            _MemClient.Store(StoreMode.Set, token.ToString(), user, freshness);

            return token.ToString();
        }

        public User GetItem(string token)
        {
            return _MemClient.Get<User>(token);
        }
    }
}
