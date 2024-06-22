namespace SageKing.Application.AspNetCore.SqlSugar.Service;

/// <summary>
/// 配置服务 
/// </summary>
/// <param name="repository"></param>
public class SysConfigService(SageKingRepository<SysConfig> repository)
    : BaseService<SysConfig>(repository)
{
}
