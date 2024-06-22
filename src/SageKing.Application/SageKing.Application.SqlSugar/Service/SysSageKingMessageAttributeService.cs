namespace SageKing.Application.AspNetCore.SqlSugar.Service;

/// <summary>
/// SageKing消息结构属性表服务 
/// </summary>
/// <param name="repository"></param>
public class SysSageKingMessageAttributeService(SageKingRepository<SysSageKingMessageAttribute> repository)
    : BaseService<SysSageKingMessageAttribute>(repository)
{
}
