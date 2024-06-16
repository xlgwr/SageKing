using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts
{
    /// <summary>
    /// C# 与 ICE 类型
    /// </summary>
    public static class CsharpTypeForICE
    {
        public static Type String = typeof(string);
        public static Type sbyte_Int8 = typeof(sbyte);
        public static Type byte_Uint8 = typeof(byte);
        public static Type short_Int16 = typeof(short);
        public static Type ushort_Uint16 = typeof(ushort);
        public static Type int_Int32 = typeof(int);
        public static Type uint_Uint32 = typeof(uint);
        public static Type long_Int64 = typeof(long);
        public static Type ulong_Uint64 = typeof(ulong);
        public static Type float_Float32 = typeof(float);
        public static Type double_Float64 = typeof(double);

        public static DataStreamTypeEnum GetDataStreamTypeEnum(Type type)
        {
            if (type == CsharpTypeForICE.String)
            {
                return DataStreamTypeEnum.String;
            }
            else if (type == CsharpTypeForICE.sbyte_Int8)
            {
                return DataStreamTypeEnum.Int8;
            }
            else if (type == CsharpTypeForICE.byte_Uint8)
            {
                return DataStreamTypeEnum.Int8;
            }
            else if (type == CsharpTypeForICE.short_Int16)
            {
                return DataStreamTypeEnum.Int16;
            }
            else if (type == CsharpTypeForICE.ushort_Uint16)
            {
                return DataStreamTypeEnum.Uint16;
            }
            else if (type == CsharpTypeForICE.int_Int32)
            {
                return DataStreamTypeEnum.Int32;
            }
            else if (type == CsharpTypeForICE.uint_Uint32)
            {
                return DataStreamTypeEnum.Uint32;
            }
            else if (type == CsharpTypeForICE.long_Int64)
            {
                return DataStreamTypeEnum.Int64;
            }
            else if (type == CsharpTypeForICE.float_Float32)
            {
                return DataStreamTypeEnum.Float32;
            }
            else if (type == CsharpTypeForICE.double_Float64)
            {
                return DataStreamTypeEnum.Float64;
            }
            else
            {
                return DataStreamTypeEnum.IceObject;
            }
        }
    }
}
