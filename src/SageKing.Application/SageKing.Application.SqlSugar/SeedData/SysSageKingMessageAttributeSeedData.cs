using SageKing.Application.AspNetCore.SqlSugar.Contracts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Application.AspNetCore.SqlSugar.SeedData;

public class SysSageKingMessageAttributeSeedData : ISqlSugarEntitySeedData<SysSageKingMessageAttribute>
{
    public IEnumerable<SysSageKingMessageAttribute> HasData()
    {
        int setp = 10;
        var crrrStarId = SeedDataConst.DefaultSysDictTypeId + setp;
        return new[]
        {
            new SysSageKingMessageAttribute { Id= crrrStarId, MessageId= SeedDataConst.DefaultMessageId, Name="属性str", Type= DataStreamTypeEnum.String },
            new SysSageKingMessageAttribute { Id= crrrStarId+1, MessageId= SeedDataConst.DefaultMessageId, Name="属性int8", Type= DataStreamTypeEnum.Int8 },
            new SysSageKingMessageAttribute { Id= crrrStarId+2, MessageId= SeedDataConst.DefaultMessageId, Name="属性int16", Type= DataStreamTypeEnum.Int16 },
            new SysSageKingMessageAttribute { Id= crrrStarId+3, MessageId= SeedDataConst.DefaultMessageId, Name="属性int32", Type= DataStreamTypeEnum.Int32 },
            new SysSageKingMessageAttribute { Id= crrrStarId+4, MessageId= SeedDataConst.DefaultMessageId, Name="属性int64", Type= DataStreamTypeEnum.Int64 },
            new SysSageKingMessageAttribute { Id= crrrStarId+5, MessageId= SeedDataConst.DefaultMessageId, Name="属性float32", Type= DataStreamTypeEnum.Float32 },
            new SysSageKingMessageAttribute { Id= crrrStarId+6, MessageId= SeedDataConst.DefaultMessageId, Name="属性float64", Type= DataStreamTypeEnum.Float64 },
        };
    }
}
