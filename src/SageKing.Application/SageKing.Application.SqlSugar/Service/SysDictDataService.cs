namespace SageKing.Application.AspNetCore.SqlSugar.Service;

/// <summary>
/// 配置字典服务 
/// </summary>
/// <param name="repository"></param>
public class SysDictDataService(SageKingRepository<SysDictData> repository)
    : BaseService<SysDictData>(repository)
{
}
