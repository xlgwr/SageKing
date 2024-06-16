using SageKing.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        None = 0,
        /// <summary>
        /// ICE 实体类型
        /// </summary>
        IceObject = 1,
        /// <summary>
        /// 单个类型
        /// </summary>
        [MapCsharp("string")]
        String = 20,
        /// <summary>
        /// int8	-128 to 127	sbyte
        /// </summary>
        [MapCsharp("sbyte")]
        Int8,
        /// <summary>
        /// uint8	0 to 255	byte
        /// </summary>
        [MapCsharp("byte")]
        Uint8,
        /// <summary>
        /// int16 -32,768 to 32,767	short
        /// </summary>
        [MapCsharp("short")]
        Int16,
        /// <summary>
        /// uint16 0 to 65,535	ushort
        /// </summary>
        [MapCsharp("ushort")]
        Uint16,
        /// <summary>
        /// int32 -2,147,483,648 to 2,147,483,647	int
        /// </summary>
        [MapCsharp("int")]
        Int32,
        /// <summary>
        /// uint32	0 to 4,294,967,295	uint
        /// </summary>
        [MapCsharp("uint")]
        Uint32,
        /// <summary>
        /// int64	-9,223,372,036,854,775,808 to 9,223,372,036,854,775,807	long
        /// </summary>
        [MapCsharp("long")]
        Int64,
        /// <summary>
        /// 0 to 18,446,744,073,709,551,615	ulong
        /// </summary>
        [MapCsharp("ulong")]
        Uint64,
        /// <summary>
        /// float
        /// </summary>
        [MapCsharp("float")]
        Float32,
        /// <summary>
        /// double
        /// </summary>
        [MapCsharp("double")]
        Float64,
        /// <summary>
        /// 集合类型
        /// </summary>
        StringArr = 50, Int8Arr, Uint8Arr, Int16Arr, Uint16Arr, Int32Arr, Uint32Arr, Int64Arr, Uint64Arr, Float32Arr, Float64Arr,
    }
}
