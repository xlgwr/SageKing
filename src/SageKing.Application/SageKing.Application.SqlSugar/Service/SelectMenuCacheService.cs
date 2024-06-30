using SageKing.Cache.Contracts;
using SageKing.Cache.Service;
using SageKing.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Application.AspNetCore.SqlSugar.Service
{
    public class SelectMenuCacheService<T>(SageKingCacheService cache) : ISelectTypeCache<string, T>
        where T : struct
    {
        public IList<KeyValue<string, T>> GetCache(int start, int end)
        {
            var getName = typeof(T).Name;
            string cachekey = CachePrefixConst.BaseMenuCache + $"{getName}_{start}_{end}";
            var getResult = cache.Get<IList<KeyValue<string, T>>>(cachekey);
            if (getResult != null)
            {
                return getResult;
            }
            var defaultT = default(T);
            getResult = defaultT.ToList(start, end);
            cache.Set(cachekey, getResult);
            return getResult;
        }
    }
}
