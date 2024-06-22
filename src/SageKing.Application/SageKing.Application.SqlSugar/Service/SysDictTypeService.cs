namespace SageKing.Application.AspNetCore.SqlSugar.Service;

/// <summary>
/// 配置字典类型服务 
/// </summary>
/// <param name="repository"></param>
public class SysDictTypeService(SageKingRepository<SysDictType> repository)
    : BaseService<SysDictType>(repository)
{
}
