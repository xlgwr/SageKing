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
            new SysDictData{ Id=crrrStarId, DictTypeId=crrrStarDictTypeId, Code="DB表类", ValueInt=1, OrderNo=100, Remark="", Status=StatusEnum.Enable },
            new SysDictData{ Id=crrrStarId+1, DictTypeId=crrrStarDictTypeId, Code="业务类", ValueInt=2, OrderNo=100, Remark="", Status=StatusEnum.Enable },
            new SysDictData{ Id=crrrStarId+3, DictTypeId=crrrStarDictTypeId, Code="通知类", ValueInt=3, OrderNo=100, Remark="", Status=StatusEnum.Enable },
            new SysDictData{ Id=crrrStarId+5, DictTypeId=crrrStarDictTypeId, Code="行情类", ValueInt=4, OrderNo=100, Remark="", Status=StatusEnum.Enable},

            new SysDictData{ Id=crrrStarId+7, DictTypeId=crrrStarDictTypeId+5, Code="String", ValueInt=20, OrderNo=100, Remark="", Status=StatusEnum.Enable },
            new SysDictData{ Id=crrrStarId+8, DictTypeId=crrrStarDictTypeId+5, Code="sbyte", ValueInt=21, OrderNo=100, Remark="", Status=StatusEnum.Enable },
            new SysDictData{ Id=crrrStarId+9, DictTypeId=crrrStarDictTypeId+5, Code="byte", ValueInt=22, OrderNo=100, Remark="", Status=StatusEnum.Enable },
            new SysDictData{ Id=crrrStarId+10, DictTypeId=crrrStarDictTypeId+5, Code="short", ValueInt=23, OrderNo=100, Remark="", Status=StatusEnum.Enable},
            new SysDictData{ Id=crrrStarId+11, DictTypeId=crrrStarDictTypeId+5, Code="ushort", ValueInt=24, OrderNo=100, Remark="", Status=StatusEnum.Enable},
            new SysDictData{ Id=crrrStarId+12, DictTypeId=crrrStarDictTypeId+5, Code="int", ValueInt=25, OrderNo=100, Remark="", Status=StatusEnum.Enable},
            new SysDictData{ Id=crrrStarId+13, DictTypeId=crrrStarDictTypeId+5, Code="uint", ValueInt=26, OrderNo=100, Remark="", Status=StatusEnum.Enable},
            new SysDictData{ Id=crrrStarId+14, DictTypeId=crrrStarDictTypeId+5, Code="long", ValueInt=27, OrderNo=100, Remark="", Status=StatusEnum.Enable},
            new SysDictData{ Id=crrrStarId+15, DictTypeId=crrrStarDictTypeId+5, Code="ulong", ValueInt=28, OrderNo=100, Remark="", Status=StatusEnum.Enable},
            new SysDictData{ Id=crrrStarId+16, DictTypeId=crrrStarDictTypeId+5, Code="float", ValueInt=29, OrderNo=100, Remark="", Status=StatusEnum.Enable},
            new SysDictData{ Id=crrrStarId+17, DictTypeId=crrrStarDictTypeId+5, Code="double", ValueInt=30, OrderNo=100, Remark="", Status=StatusEnum.Enable},
        };
    }
}