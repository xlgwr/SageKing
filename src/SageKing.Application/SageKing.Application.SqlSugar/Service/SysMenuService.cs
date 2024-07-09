using SageKing.Cache.Service;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace SageKing.Application.AspNetCore.SqlSugar.Service;

/// <summary>
/// 菜单服务 
/// </summary>
/// <param name="repository"></param>
public class SysMenuService(SageKingRepository<SysMenu> repository, SageKingCacheService _cache)
    : BaseService<SysMenu>(repository), IBaseServiceCache<SysMenu, long>
{
    /// <summary>
    /// 获取集合
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取集合")]
    public virtual async Task<List<SysMenu>> GetToTreeCacheAsync(Expression<Func<SysMenu, bool>> func = null)
    {
        var cacheKey = CachePrefixConst.MenuCache + "AllTree";
        var result = _cache.Get<List<SysMenu>>(cacheKey);
        if (func != null)
        {
            result = result.AsQueryable().Where(func).ToList();
        }
        if (result != null)
        {
            return result;
        }
        result = await repository.AsQueryable().Where(a => a.Path != "/").OrderBy(u => u.OrderNo).ToTreeAsync(u => u.Children, u => u.Pid, 0);
        _cache.Set(cacheKey, result);
        if (func != null)
        {
            result = result.AsQueryable().Where(func).ToList();
        }
        return result;
    }
    /// <summary>
    ///  <=0 all
    /// </summary>
    /// <param name="id"></param>
    public void CacheRefresh(long id)
    {
        var cacheKey = CachePrefixConst.MenuCache + id;
        if (id <= 0)
        {
            cacheKey = CachePrefixConst.MenuCache + "AllTree";
        }
        _cache.Remove(cacheKey);
    }

    /// <summary>
    /// 获取菜单
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取菜单")]
    public virtual async Task<SysMenu> GetDetailCache(long id)
    {
        var cacheKey = CachePrefixConst.MenuCache + id;
        var result = _cache.Get<SysMenu>(cacheKey);
        if (result != null)
        {
            return result;
        }
        result = await repository.GetByIdAsync(id);
        _cache.Set(cacheKey, result);
        return result;
    }
}
