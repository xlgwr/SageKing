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
        int step = 5;
        return new[]
        {
            new SysDictType{ Id=SeedDataConst.DefaultSysDictTypeId, Name="SageKing消息类型", Code="code_message_type", OrderNo=100, Remark="", Status=StatusEnum.Enable, CreateTime= DateTime.Now },
             new SysDictType{ Id=SeedDataConst.DefaultSysDictTypeId+5, Name="SageKing消息属性类型", Code="code_message_attr_type", OrderNo=100, Remark="Ice 消息属性 映射", Status=StatusEnum.Enable, CreateTime= DateTime.Now },
        };
    }
}