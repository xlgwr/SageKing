using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts
{
    /// <summary>
    /// 数据流类型
    /// </summary>
    public enum DataStreamTypeEnum : byte
    {
        /// <summary>
        /// ICE 实体类型
        /// </summary>
        IceObject = 0,
        /// <summary>
        /// 单个类型
        /// </summary>
        String = 20, Int8, Uint8, Int16, Uint16, Int32, Uint32, Int64, Uint64, Float32, Float64,
        /// <summary>
        /// 集合类型
        /// </summary>
        StringArr = 50, Int8Arr, Uint8Arr, Int16Arr, Uint16Arr, Int32Arr, Uint32Arr, Int64Arr, Uint64Arr, Float32Arr, Float64Arr,
    }
}
