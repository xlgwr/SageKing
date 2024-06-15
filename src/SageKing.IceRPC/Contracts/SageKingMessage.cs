using SageKing.IceRPC.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Contracts
{
    /// <summary>
    /// 实现消息定义.
    /// </summary>
    public record class SageKingMessage : ISageKingMessage
    {

        bool _isChange = false;

        StreamPackageData _packageData;


        #region 数据容器
        /// <summary>
        /// 属性值
        /// </summary>
        ConcurrentDictionary<string, string> _AttributeValuestring;
        ConcurrentDictionary<string, sbyte> _AttributeValueInt8Arr_sbyte;
        ConcurrentDictionary<string, byte> _AttributeValueUint8Arr_byte;
        ConcurrentDictionary<string, short> _AttributeValueInt16_short;
        ConcurrentDictionary<string, ushort> _AttributeValueuint16_ushort;
        ConcurrentDictionary<string, int> _AttributeValueint32_int;
        ConcurrentDictionary<string, uint> _AttributeValueuint32_uint;
        ConcurrentDictionary<string, long> _AttributeValueint64_long;
        ConcurrentDictionary<string, ulong> _AttributeValueUint64_ulong;
        ConcurrentDictionary<string, float> _AttributeValueFloat32_float;
        ConcurrentDictionary<string, double> _AttributeValueFloat64_double;
        #endregion

        /// <summary>
        /// 属性位置
        /// 用于解包定位
        /// DataStreamTypeEnum 使有带有MapCsharp枚举
        /// </summary>
        ConcurrentDictionary<DataStreamTypeEnum, ConcurrentDictionary<string, int>> _AttributePosition;

        /// <summary>
        /// 
        /// </summary>
        public SageKingMessage()
        {
            _isChange = false;
            _AttributePosition = new();
        }

        public string Id { get; set; }

        public string Varsion { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 从数据流包中解码数据.
        /// </summary>
        /// <param name="packageData"></param>
        /// <exception cref="NotSupportedException"></exception>
        public void LoadData(StreamPackageData packageData)
        {
            //读取ICE数据
            DataStreamTypValue<string[]> tempValuestring = null;
            DataStreamTypValue<sbyte[]> tempValueInt8Arr_sbyte = null;
            DataStreamTypValue<byte[]> tempValueUint8Arr_byte = null;
            DataStreamTypValue<short[]> tempValueInt16_short = null;
            DataStreamTypValue<ushort[]> tempValueuint16_ushort = null;
            DataStreamTypValue<int[]> tempValueint32_int = null;
            DataStreamTypValue<uint[]> tempValueuint32_uint = null;
            DataStreamTypValue<long[]> tempValueint64_long = null;
            DataStreamTypValue<ulong[]> tempValueUint64_ulong = null;
            DataStreamTypValue<float[]> tempValueFloat32_float = null;
            DataStreamTypValue<double[]> tempValueFloat64_double = null;

            foreach (var rowType in packageData.RowType)
            {
                if (Enum.IsDefined(typeof(DataStreamTypeEnum), rowType))
                {
                    throw new NotSupportedException($"当前类型【{rowType}】不支持范围内");
                }

                var getTypeMenu = (DataStreamTypeEnum)rowType;
                int index = 0;

                switch (getTypeMenu)
                {
                    case DataStreamTypeEnum.StringArr:
                        tempValuestring = new DataStreamTypValue<string[]>(
                            DataStreamTypeEnum.StringArr,
                            packageData.DataBody[index].GetString()
                            );
                        break;
                    case DataStreamTypeEnum.Int8Arr:
                        tempValueInt8Arr_sbyte = new DataStreamTypValue<sbyte[]>(
                            DataStreamTypeEnum.Int8Arr,
                            packageData.DataBody[index].Getsbyte()
                            );
                        break;
                    case DataStreamTypeEnum.Uint8Arr:
                        tempValueUint8Arr_byte = new DataStreamTypValue<byte[]>(
                            DataStreamTypeEnum.Uint8Arr,
                            packageData.DataBody[index].Getbyte()
                            );
                        break;
                    case DataStreamTypeEnum.Int16Arr:
                        tempValueInt16_short = new DataStreamTypValue<short[]>(
                            DataStreamTypeEnum.Int16Arr,
                            packageData.DataBody[index].Getshort()
                            );
                        break;
                    case DataStreamTypeEnum.Uint16Arr:
                        tempValueuint16_ushort = new DataStreamTypValue<ushort[]>(
                            DataStreamTypeEnum.Uint16Arr,
                            packageData.DataBody[index].Getushort()
                            );
                        break;
                    case DataStreamTypeEnum.Int32Arr:
                        tempValueint32_int = new DataStreamTypValue<int[]>(
                            DataStreamTypeEnum.Int32Arr,
                            packageData.DataBody[index].Getint()
                            );
                        break;
                    case DataStreamTypeEnum.Uint32Arr:
                        tempValueuint32_uint = new DataStreamTypValue<uint[]>(
                             DataStreamTypeEnum.Uint32Arr,
                             packageData.DataBody[index].Getuint()
                             );
                        break;
                    case DataStreamTypeEnum.Int64Arr:
                        tempValueint64_long = new DataStreamTypValue<long[]>(
                             DataStreamTypeEnum.Int64Arr,
                             packageData.DataBody[index].Getlong()
                             );
                        break;
                    case DataStreamTypeEnum.Uint64Arr:
                        tempValueUint64_ulong = new DataStreamTypValue<ulong[]>(
                             DataStreamTypeEnum.Uint64Arr,
                             packageData.DataBody[index].Getulong()
                             );
                        break;
                    case DataStreamTypeEnum.Float32Arr:
                        tempValueFloat32_float = new DataStreamTypValue<float[]>(
                             DataStreamTypeEnum.Float32Arr,
                             packageData.DataBody[index].Getfloat()
                             );
                        break;
                    case DataStreamTypeEnum.Float64Arr:
                        tempValueFloat64_double = new DataStreamTypValue<double[]>(
                             DataStreamTypeEnum.Float64Arr,
                             packageData.DataBody[index].Getdouble()
                             );
                        break;
                    default:
                        throw new NotSupportedException($"当前类型【{getTypeMenu}】暂时不支持");
                        break;
                }

            }

            tempValuestring.GetPostData<string>(_AttributeValuestring, _AttributePosition);

            tempValueInt8Arr_sbyte.GetPostData<sbyte>(_AttributeValueInt8Arr_sbyte, _AttributePosition);

            tempValueUint8Arr_byte.GetPostData<byte>(_AttributeValueUint8Arr_byte, _AttributePosition);

            tempValueInt16_short.GetPostData<short>(_AttributeValueInt16_short, _AttributePosition);

            tempValueuint16_ushort.GetPostData<ushort>(_AttributeValueuint16_ushort, _AttributePosition);

            tempValueint32_int.GetPostData<int>(_AttributeValueint32_int, _AttributePosition);

            tempValueuint32_uint.GetPostData<uint>(_AttributeValueuint32_uint, _AttributePosition);

            tempValueint64_long.GetPostData<long>(_AttributeValueint64_long, _AttributePosition);

            tempValueUint64_ulong.GetPostData<ulong>(_AttributeValueUint64_ulong, _AttributePosition);

            tempValueFloat32_float.GetPostData<float>(_AttributeValueFloat32_float, _AttributePosition);

            tempValueFloat64_double.GetPostData<double>(_AttributeValueFloat64_double, _AttributePosition);

            //清理
            _packageData = packageData;
            _isChange = false;
        }

        /// <summary>
        /// 生成ICE数据流包
        /// </summary>
        /// <returns></returns>
        public StreamPackageData ToData()
        {
            if (!_isChange)
            {
                return _packageData;
            }
            //todo 
            var rowType = _AttributePosition.Keys.OrderBy(a => a);

            List<byte[]> databody = new List<byte[]>();

            foreach (var type in rowType)
            {
                var itemPos = _AttributePosition[type];
                var sortAttribute = itemPos.OrderBy(a => a.Value).Select(a => a.Key).ToList();

                switch (type)
                {
                    case DataStreamTypeEnum.StringArr:
                        List<string> strings = new();
                        foreach (var item in sortAttribute)
                        {
                            strings.Add(_AttributeValuestring[item]);
                        }
                        databody.Add(strings.ToArray().ToIceByte());
                        break;
                    case DataStreamTypeEnum.Int8Arr:
                        List<sbyte> data2 = new();
                        foreach (var item in sortAttribute)
                        {
                            data2.Add(_AttributeValueInt8Arr_sbyte[item]);
                        }
                        databody.Add(data2.ToArray().ToIceByte());
                        break;
                    case DataStreamTypeEnum.Uint8Arr:
                        List<byte> data3 = new();
                        foreach (var item in sortAttribute)
                        {
                            data3.Add(_AttributeValueUint8Arr_byte[item]);
                        }
                        databody.Add(data3.ToArray().ToIceByte());
                        break;
                    case DataStreamTypeEnum.Int16Arr:
                        List<short> data4 = new();
                        foreach (var item in sortAttribute)
                        {
                            data4.Add(_AttributeValueInt16_short[item]);
                        }
                        databody.Add(data4.ToArray().ToIceByte());
                        break;
                    case DataStreamTypeEnum.Uint16Arr:
                        List<ushort> data5 = new();
                        foreach (var item in sortAttribute)
                        {
                            data5.Add(_AttributeValueuint16_ushort[item]);
                        }
                        databody.Add(data5.ToArray().ToIceByte());
                        break;
                    case DataStreamTypeEnum.Int32Arr:
                        List<int> data6 = new();
                        foreach (var item in sortAttribute)
                        {
                            data6.Add(_AttributeValueint32_int[item]);
                        }
                        databody.Add(data6.ToArray().ToIceByte());
                        break;
                    case DataStreamTypeEnum.Uint32Arr:
                        List<uint> data7 = new();
                        foreach (var item in sortAttribute)
                        {
                            data7.Add(_AttributeValueuint32_uint[item]);
                        }
                        databody.Add(data7.ToArray().ToIceByte());
                        break;
                    case DataStreamTypeEnum.Int64Arr:
                        List<long> data8 = new();
                        foreach (var item in sortAttribute)
                        {
                            data8.Add(_AttributeValueint64_long[item]);
                        }
                        databody.Add(data8.ToArray().ToIceByte());
                        break;
                    case DataStreamTypeEnum.Uint64Arr:
                        List<ulong> data9 = new();
                        foreach (var item in sortAttribute)
                        {
                            data9.Add(_AttributeValueUint64_ulong[item]);
                        }
                        databody.Add(data9.ToArray().ToIceByte());
                        break;
                    case DataStreamTypeEnum.Float32Arr:
                        List<float> data10 = new();
                        foreach (var item in sortAttribute)
                        {
                            data10.Add(_AttributeValueFloat32_float[item]);
                        }
                        databody.Add(data10.ToArray().ToIceByte());
                        break;
                    case DataStreamTypeEnum.Float64Arr:
                        List<double> data11 = new();
                        foreach (var item in sortAttribute)
                        {
                            data11.Add(_AttributeValueFloat64_double[item]);
                        }
                        databody.Add(data11.ToArray().ToIceByte());
                        break;
                    default:
                        break;
                }

            }

            var rowTypeArr = rowType.Select(a => (byte)a).ToArray();
            _packageData = new StreamPackageData(rowTypeArr, databody.ToArray());

            return _packageData;
        }

        #region AddOrUpdate

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<string> value)
        {
            return _AttributeValuestring.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<sbyte> value)
        {
            return _AttributeValueInt8Arr_sbyte.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<byte> value)
        {
            return _AttributeValueUint8Arr_byte.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<short> value)
        {
            return _AttributeValueInt16_short.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<int> value)
        {
            return _AttributeValueint32_int.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<uint> value)
        {
            return _AttributeValueuint32_uint.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<long> value)
        {
            return _AttributeValueint64_long.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<ulong> value)
        {
            return _AttributeValueUint64_ulong.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<float> value)
        {
            return _AttributeValueFloat32_float.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<double> value)
        {
            return _AttributeValueFloat64_double.AddOrUpdatePost(attributeName, value, _AttributePosition);

        }

        #endregion

        #region Get
        public string Get(string attributeName)
        {
            if (_AttributeValuestring.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return string.Empty;
        }

        public sbyte Getsbyte(string attributeName)
        {
            if (_AttributeValueInt8Arr_sbyte.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public byte Getbyte(string attributeName)
        {
            if (_AttributeValueUint8Arr_byte.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public short Getshort(string attributeName)
        {
            if (_AttributeValueInt16_short.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public ushort Getushort(string attributeName)
        {
            if (_AttributeValueuint16_ushort.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public int Getint(string attributeName)
        {
            if (_AttributeValueint32_int.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public uint Getuint(string attributeName)
        {
            if (_AttributeValueuint32_uint.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public long Getlong(string attributeName)
        {
            if (_AttributeValueint64_long.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public ulong Getulong(string attributeName)
        {
            if (_AttributeValueUint64_ulong.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public float Getfloat(string attributeName)
        {
            if (_AttributeValueFloat32_float.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public double Getdouble(string attributeName)
        {
            if (_AttributeValueFloat64_double.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        #endregion
        public bool Remove(string attributeName, DataStreamTypeEnum type)
        {
            if (Enum.IsDefined(typeof(DataStreamTypeEnum), type))
            {
                throw new NotSupportedException($"当前类型【{type}】不支持范围内");
            }

            switch (type)
            {
                case DataStreamTypeEnum.StringArr:
                    return _AttributeValuestring.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Int8Arr:
                    return _AttributeValueInt8Arr_sbyte.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Uint8Arr:
                    return _AttributeValueUint8Arr_byte.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Int16Arr:
                    return _AttributeValueInt16_short.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Uint16Arr:
                    return _AttributeValueuint16_ushort.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Int32Arr:
                    return _AttributeValueint32_int.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Uint32Arr:
                    return _AttributeValueuint32_uint.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Int64Arr:
                    return _AttributeValueint64_long.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Uint64Arr:
                    return _AttributeValueUint64_ulong.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Float32Arr:
                    return _AttributeValueFloat32_float.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Float64Arr:
                    return _AttributeValueFloat64_double.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                default:
                    throw new NotSupportedException($"当前类型【{type}】暂时不支持");
                    break;
            }
            return true;
        }

    }
}
