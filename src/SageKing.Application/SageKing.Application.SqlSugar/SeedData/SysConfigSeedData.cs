namespace SageKing.Application.AspNetCore.SqlSugar;


/// <summary>
/// 系统配置表种子数据
/// </summary>
public class SysConfigSeedData : ISqlSugarEntitySeedData<SysConfig>
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    public IEnumerable<SysConfig> HasData()
    {
        return new[]
        {
            new SysConfig{ Id=1300000000101, Name="演示环境", Code="sys_demo", Value="False", SysFlag=YesNoEnum.Y, Remark="演示环境", OrderNo=10, GroupCode="Default" },
        };
    }
}