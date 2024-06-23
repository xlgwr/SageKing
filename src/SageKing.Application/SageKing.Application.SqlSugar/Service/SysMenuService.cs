namespace SageKing.Application.AspNetCore.SqlSugar.Service;

/// <summary>
/// 菜单服务 
/// </summary>
/// <param name="repository"></param>
public class SysMenuService(SageKingRepository<SysMenu> repository)
    : BaseService<SysMenu>(repository)
{
    /// <summary>
    /// 获取集合
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取集合")]
    public virtual async Task<List<SysMenu>> GetToTreeAsync()
    {
        return await repository.AsQueryable().OrderBy(u => u.OrderNo).ToTreeAsync(u => u.Children, u => u.Pid, 0);
    }
}
