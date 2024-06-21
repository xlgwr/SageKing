using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Cache.Service;

public class SageKingRedisCache : FullRedis, ICache
{
    public SageKingRedisCache(IOptions<SageKingCacheOptions> options) : base(
        new RedisOptions
        {
            Configuration = options.Value.Cache.Redis.Configuration,
            Prefix = options.Value.Cache.Redis.Prefix

        })
    {
        if (options.Value.Cache.Redis.MaxMessageSize > 0)
        {
            MaxMessageSize = options.Value.Cache.Redis.MaxMessageSize;
        }
    }
}
