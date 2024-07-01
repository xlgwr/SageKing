using SageKing.Cache.Service;

namespace SageKing.Application.AspNetCore.SqlSugar.Service;

/// <summary>
/// 菜单服务 
/// </summary>
/// <param name="repository"></param>
public class SysMenuService(SageKingRepository<SysMenu> repository, SageKingCacheService _cache)
    : BaseService<SysMenu>(repository)
{
    /// <summary>
    /// 获取集合
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取集合")]
    public virtual async Task<List<SysMenu>> GetToTreeCacheAsync()
    {
        var cacheKey = CachePrefixConst.MenuCache + "AllTree";
        var result = _cache.Get<List<SysMenu>>(cacheKey);
        if (result != null)
        {
            return result;
        }
        result = await repository.AsQueryable().Where(a => a.Path != "/").OrderBy(u => u.OrderNo).ToTreeAsync(u => u.Children, u => u.Pid, 0);
        _cache.Set(cacheKey, result);
        return result;
    }

    /// <summary>
    /// 获取菜单
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取菜单")]
    public virtual async Task<SysMenu> GetMenuCacheAsync(long id)
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
