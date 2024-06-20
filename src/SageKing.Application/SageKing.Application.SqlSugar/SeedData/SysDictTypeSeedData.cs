namespace SageKing.Application.AspNetCore.SqlSugar.SeedData;
/// <summary>
/// 系统字典类型表种子数据
/// </summary>
public class SysDictTypeSeedData : ISqlSugarEntitySeedData<SysDictType>
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    public IEnumerable<SysDictType> HasData()
    {
        return new[]
        {
            new SysDictType{ Id=SeedDataConst.DefaultSysDictTypeId, Name="SageKing消息类型", Code="code_message_type", OrderNo=100, Remark="Ice 类型 映射", Status=StatusEnum.Enable, CreateTime= DateTime.Now },
        };
    }
}