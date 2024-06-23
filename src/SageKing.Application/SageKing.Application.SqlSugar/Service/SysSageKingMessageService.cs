namespace SageKing.Application.AspNetCore.SqlSugar.Service;

/// <summary>
/// SageKing消息结构表服务 
/// </summary>
/// <param name="repository"></param>
public class SysSageKingMessageService(SageKingRepository<SysSageKingMessage> repository)
    : BaseService<SysSageKingMessage>(repository)
{

}
