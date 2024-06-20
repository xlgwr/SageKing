namespace SageKing.Application.AspNetCore.SqlSugar;

/// <summary>
/// 系统字典值表种子数据
/// </summary>
public class SysDictDataSeedData : ISqlSugarEntitySeedData<SysDictData>
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    public IEnumerable<SysDictData> HasData()
    {
        int setp = 100;
        var crrrStarId = SeedDataConst.DefaultSysDictTypeId + setp;
        var crrrStarDictTypeId = SeedDataConst.DefaultSysDictTypeId;
        return new[]
        {
            new SysDictData{ Id=crrrStarId, DictTypeId=crrrStarDictTypeId, Value="DB表类", Code="1", OrderNo=100, Remark="", Status=StatusEnum.Enable },
            new SysDictData{ Id=crrrStarId+1, DictTypeId=crrrStarDictTypeId, Value="业务类", Code="2", OrderNo=100, Remark="", Status=StatusEnum.Enable },
            new SysDictData{ Id=crrrStarId+3, DictTypeId=crrrStarDictTypeId, Value="通知类", Code="3", OrderNo=100, Remark="", Status=StatusEnum.Enable },
            new SysDictData{ Id=crrrStarId+5, DictTypeId=crrrStarDictTypeId, Value="行情类", Code="4", OrderNo=100, Remark="", Status=StatusEnum.Enable},
        };
    }
}