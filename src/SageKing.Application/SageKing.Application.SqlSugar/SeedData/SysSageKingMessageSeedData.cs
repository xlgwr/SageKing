
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Application.AspNetCore.SqlSugar.SeedData;

public class SysSageKingMessageSeedData : ISqlSugarEntitySeedData<SysSageKingMessage>
{
    public IEnumerable<SysSageKingMessage> HasData()
    {
        return new[]
        {
            new SysSageKingMessage() {Id=SeedDataConst.DefaultMessageId,Name="测试001",Type= 1}
        };
    }
}
