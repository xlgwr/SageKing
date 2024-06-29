using SageKing.Cache.Contracts;
using SageKing.Cache.Service;
using System.Linq.Expressions;

namespace SageKing.Application.AspNetCore.SqlSugar.Service;

/// <summary>
/// 配置字典类型服务 
/// </summary>
/// <param name="repository"></param>
public class SysDictTypeService(SageKingRepository<SysDictType> repository, SageKingCacheService sageKingCache)
    : BaseService<SysDictType>(repository), IBaseServiceCache<SysDictType>
{
    public void CacheRefresh(string codeType)
    {
        string cachekey = CachePrefixConst.SysDictTypePrefix + codeType;
        sageKingCache.Remove(cachekey);
    }

    public async Task<SysDictType> GetDetailCache(string codeType)
    {
        string cachekey = CachePrefixConst.SysDictTypePrefix + codeType;
        var getCache = sageKingCache.Get<SysDictType>(cachekey);
        if (getCache != null)
        {
            return getCache;
        }
        var result = await repository.AsQueryable().Includes(a => a.Children).Where(a => a.Code == codeType).FirstAsync();
        if (result != null)
        {
            sageKingCache.Set(cachekey, result);
        }
        return result;
    }
}
