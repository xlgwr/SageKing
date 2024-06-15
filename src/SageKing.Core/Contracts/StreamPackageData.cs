using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts
{
    /// <summary>
    /// StreamPackage 基础数据定义
    /// </summary>
    /// <param name="RowType"></param>
    /// <param name="DataBody"></param>
    public record class StreamPackageData(byte[] RowType, byte[][] DataBody)
}
