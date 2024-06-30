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
        int ParentStep = 5;
        var crrrStarId = SeedDataConst.DefaultSysDictTypeId + setp;
        var crrrStarDictTypeId = SeedDataConst.DefaultSysDictTypeId;
        return new[]
        {
            new SysDictData{ Id=crrrStarId, DictTypeId=crrrStarDictTypeId, Code="DB表类", ValueInt=1, OrderNo=100, Remark="", Status=StatusEnum.Enable },
            new SysDictData{ Id=crrrStarId+1, DictTypeId=crrrStarDictTypeId, Code="业务类", ValueInt=2, OrderNo=100, Remark="", Status=StatusEnum.Enable },
            new SysDictData{ Id=crrrStarId+3, DictTypeId=crrrStarDictTypeId, Code="通知类", ValueInt=3, OrderNo=100, Remark="", Status=StatusEnum.Enable },
            new SysDictData{ Id=crrrStarId+5, DictTypeId=crrrStarDictTypeId, Code="行情类", ValueInt=4, OrderNo=100, Remark="", Status=StatusEnum.Enable},

            //ValueInt 请参照 DataStreamTypeEnumExts
            new SysDictData{ Id=crrrStarId+100, DictTypeId=crrrStarDictTypeId+ParentStep*2, Code="status", ValueInt=21, OrderNo=100, Remark="", Status=StatusEnum.Enable},
             new SysDictData{ Id=crrrStarId+101, DictTypeId=crrrStarDictTypeId+ParentStep*2, Code="enable", ValueInt=21, OrderNo=100, Remark="", Status=StatusEnum.Enable},
             new SysDictData{ Id=crrrStarId+102, DictTypeId=crrrStarDictTypeId+ParentStep*2, Code="number", ValueInt=25, OrderNo=100, Remark="", Status=StatusEnum.Enable},
             new SysDictData{ Id=crrrStarId+103, DictTypeId=crrrStarDictTypeId+ParentStep*2, Code="date", ValueInt=25, OrderNo=100, Remark="", Status=StatusEnum.Enable},
             new SysDictData{ Id=crrrStarId+104, DictTypeId=crrrStarDictTypeId+ParentStep*2, Code="time", ValueInt=25, OrderNo=100, Remark="", Status=StatusEnum.Enable},
             new SysDictData{ Id=crrrStarId+105, DictTypeId=crrrStarDictTypeId+ParentStep*2, Code="price", ValueInt=29, OrderNo=100, Remark="", Status=StatusEnum.Enable},
            new SysDictData{ Id=crrrStarId+106, DictTypeId=crrrStarDictTypeId+ParentStep*2, Code="type", ValueInt=25, OrderNo=100, Remark="", Status=StatusEnum.Enable},
        };
    }
}