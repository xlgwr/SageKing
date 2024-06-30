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

           new SysDictType{ Id=SeedDataConst.DefaultSysDictTypeId+step*2, Name="快速导入属性映射", Code="code_message_attr_import", OrderNo=100, Remark="快速导入属性映射", Status=StatusEnum.Enable, CreateTime= DateTime.Now },
        };
    }
}