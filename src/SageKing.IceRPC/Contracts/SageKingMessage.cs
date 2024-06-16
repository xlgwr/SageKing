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


        #region 数据容器 AttributeValue
        /// <summary>
        /// 属性值 
        /// </summary>
        ConcurrentDictionary<string, string> _0string;
        ConcurrentDictionary<string, sbyte> _1Int8Arr_sbyte;
        ConcurrentDictionary<string, byte> _2Uint8Arr_byte;
        ConcurrentDictionary<string, short> _3Int16_short;
        ConcurrentDictionary<string, ushort> _4uint16_ushort;
        ConcurrentDictionary<string, int> _5eint32_int;
        ConcurrentDictionary<string, uint> _6uint32_uint;
        ConcurrentDictionary<string, long> _7int64_long;
        ConcurrentDictionary<string, ulong> _8Uint64_ulong;
        ConcurrentDictionary<string, float> _9Float32_float;
        ConcurrentDictionary<string, double> _10Float64_double;
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

        #region AddOrUpdate

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<string> value)
        {
            if (_0string == null) _0string = new();
            return _0string.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<sbyte> value)
        {
            if (_1Int8Arr_sbyte == null) _1Int8Arr_sbyte = new();
            return _1Int8Arr_sbyte.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<byte> value)
        {
            if (_2Uint8Arr_byte == null) _2Uint8Arr_byte = new();
            return _2Uint8Arr_byte.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<short> value)
        {
            if (_3Int16_short == null) _3Int16_short = new();
            return _3Int16_short.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<int> value)
        {
            if (_5eint32_int == null) _5eint32_int = new();
            return _5eint32_int.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<uint> value)
        {
            if (_6uint32_uint == null) _6uint32_uint = new();
            return _6uint32_uint.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<long> value)
        {
            if (_7int64_long == null) _7int64_long = new();
            return _7int64_long.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<ulong> value)
        {
            if (_8Uint64_ulong == null) _8Uint64_ulong = new();
            return _8Uint64_ulong.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<float> value)
        {
            if (_9Float32_float == null) _9Float32_float = new();
            return _9Float32_float.AddOrUpdatePost(attributeName, value, _AttributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<double> value)
        {
            if (_10Float64_double == null) _10Float64_double = new();
            return _10Float64_double.AddOrUpdatePost(attributeName, value, _AttributePosition);

        }

        #endregion

        #region Get
        public string Get(string attributeName)
        {
            if (_0string.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return string.Empty;
        }

        public sbyte Getsbyte(string attributeName)
        {
            if (_1Int8Arr_sbyte.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public byte Getbyte(string attributeName)
        {
            if (_2Uint8Arr_byte.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public short Getshort(string attributeName)
        {
            if (_3Int16_short.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public ushort Getushort(string attributeName)
        {
            if (_4uint16_ushort.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public int Getint(string attributeName)
        {
            if (_5eint32_int.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public uint Getuint(string attributeName)
        {
            if (_6uint32_uint.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public long Getlong(string attributeName)
        {
            if (_7int64_long.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public ulong Getulong(string attributeName)
        {
            if (_8Uint64_ulong.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public float Getfloat(string attributeName)
        {
            if (_9Float32_float.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        public double Getdouble(string attributeName)
        {
            if (_10Float64_double.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return default;
        }

        #endregion
        public bool Remove(string attributeName, DataStreamTypeEnum type)
        {
            switch (type)
            {
                case DataStreamTypeEnum.String:
                    return _0string.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Int8:
                    return _1Int8Arr_sbyte.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Uint8:
                    return _2Uint8Arr_byte.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Int16:
                    return _3Int16_short.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Uint16:
                    return _4uint16_ushort.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Int32:
                    return _5eint32_int.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Uint32:
                    return _6uint32_uint.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Int64:
                    return _7int64_long.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Uint64:
                    return _8Uint64_ulong.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Float32:
                    return _9Float32_float.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Float64:
                    return _10Float64_double.RemovePost(attributeName, type, _AttributePosition, ref _isChange);
                    break;
                default:
                    throw new NotSupportedException($"当前类型【{type}】暂时不支持");
                    break;
            }
            return true;
        }


        /// <summary>
        /// 从数据流包中解码数据.
        /// </summary>
        /// <param name="packageData"></param>
        /// <exception cref="NotSupportedException"></exception>
        public void LoadData(StreamPackageData packageData)
        {
            DataStreamTypValue<string[]> temp0string = null;
            DataStreamTypValue<sbyte[]> temp1Int8Arr_sbyte = null;
            DataStreamTypValue<byte[]> temp2Uint8Arr_byte = null;
            DataStreamTypValue<short[]> temp3Int16_short = null;
            DataStreamTypValue<ushort[]> temp4uint16_ushort = null;
            DataStreamTypValue<int[]> temp5int32_int = null;
            DataStreamTypValue<uint[]> temp6uint32_uint = null;
            DataStreamTypValue<long[]> temp7int64_long = null;
            DataStreamTypValue<ulong[]> temp8Uint64_ulong = null;
            DataStreamTypValue<float[]> temp9Float32_float = null;
            DataStreamTypValue<double[]> temp10Float64_double = null;

            foreach (var rowType in packageData.RowType)
            {
                if (Enum.IsDefined(typeof(DataStreamTypeEnum), rowType))
                {
                    throw new NotSupportedException($"当前类型【{rowType}】不在支持范围内");
                }

                var getTypeMenu = (DataStreamTypeEnum)rowType;

                int index = 0;

                var currDatabyte = packageData.DataBody[index];

                switch (getTypeMenu)
                {
                    case DataStreamTypeEnum.StringArr:
                        temp0string = new DataStreamTypValue<string[]>(getTypeMenu, currDatabyte.GetString());
                        break;
                    case DataStreamTypeEnum.Int8Arr:
                        temp1Int8Arr_sbyte = new DataStreamTypValue<sbyte[]>(getTypeMenu, currDatabyte.Getsbyte());
                        break;
                    case DataStreamTypeEnum.Uint8Arr:
                        temp2Uint8Arr_byte = new DataStreamTypValue<byte[]>(getTypeMenu, currDatabyte.Getbyte());
                        break;
                    case DataStreamTypeEnum.Int16Arr:
                        temp3Int16_short = new DataStreamTypValue<short[]>(getTypeMenu, currDatabyte.Getshort());
                        break;
                    case DataStreamTypeEnum.Uint16Arr:
                        temp4uint16_ushort = new DataStreamTypValue<ushort[]>(getTypeMenu, currDatabyte.Getushort());
                        break;
                    case DataStreamTypeEnum.Int32Arr:
                        temp5int32_int = new DataStreamTypValue<int[]>(getTypeMenu, currDatabyte.Getint());
                        break;
                    case DataStreamTypeEnum.Uint32Arr:
                        temp6uint32_uint = new DataStreamTypValue<uint[]>(getTypeMenu, currDatabyte.Getuint());
                        break;
                    case DataStreamTypeEnum.Int64Arr:
                        temp7int64_long = new DataStreamTypValue<long[]>(getTypeMenu, currDatabyte.Getlong());
                        break;
                    case DataStreamTypeEnum.Uint64Arr:
                        temp8Uint64_ulong = new DataStreamTypValue<ulong[]>(getTypeMenu, currDatabyte.Getulong());
                        break;
                    case DataStreamTypeEnum.Float32Arr:
                        temp9Float32_float = new DataStreamTypValue<float[]>(getTypeMenu, currDatabyte.Getfloat());
                        break;
                    case DataStreamTypeEnum.Float64Arr:
                        temp10Float64_double = new DataStreamTypValue<double[]>(getTypeMenu, currDatabyte.Getdouble());
                        break;
                    default:
                        throw new NotSupportedException($"当前类型【{getTypeMenu}】暂时不支持");
                        break;
                }

                index++;
            }

            temp0string.GetPostData<string>(_0string, _AttributePosition);

            temp1Int8Arr_sbyte.GetPostData<sbyte>(_1Int8Arr_sbyte, _AttributePosition);

            temp2Uint8Arr_byte.GetPostData<byte>(_2Uint8Arr_byte, _AttributePosition);

            temp3Int16_short.GetPostData<short>(_3Int16_short, _AttributePosition);

            temp4uint16_ushort.GetPostData<ushort>(_4uint16_ushort, _AttributePosition);

            temp5int32_int.GetPostData<int>(_5eint32_int, _AttributePosition);

            temp6uint32_uint.GetPostData<uint>(_6uint32_uint, _AttributePosition);

            temp7int64_long.GetPostData<long>(_7int64_long, _AttributePosition);

            temp8Uint64_ulong.GetPostData<ulong>(_8Uint64_ulong, _AttributePosition);

            temp9Float32_float.GetPostData<float>(_9Float32_float, _AttributePosition);

            temp10Float64_double.GetPostData<double>(_10Float64_double, _AttributePosition);

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

            var rowType = _AttributePosition.Keys.OrderBy(a => a);

            List<byte[]> databody = new List<byte[]>();

            foreach (var type in rowType)
            {
                var itemPos = _AttributePosition[type];
                var sortAttribute = itemPos.OrderBy(a => a.Value).Select(a => a.Key).ToList();

                switch (type)
                {
                    case DataStreamTypeEnum.StringArr:
                        databody.Add(sortAttribute.GetArray(_0string).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Int8Arr:
                        databody.Add(sortAttribute.GetArray(_1Int8Arr_sbyte).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Uint8Arr:
                        databody.Add(sortAttribute.GetArray(_2Uint8Arr_byte).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Int16Arr:
                        databody.Add(sortAttribute.GetArray(_3Int16_short).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Uint16Arr:
                        databody.Add(sortAttribute.GetArray(_4uint16_ushort).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Int32Arr:
                        databody.Add(sortAttribute.GetArray(_5eint32_int).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Uint32Arr:
                        databody.Add(sortAttribute.GetArray(_6uint32_uint).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Int64Arr:
                        databody.Add(sortAttribute.GetArray(_7int64_long).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Uint64Arr:
                        databody.Add(sortAttribute.GetArray(_8Uint64_ulong).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Float32Arr:
                        databody.Add(sortAttribute.GetArray(_9Float32_float).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Float64Arr:
                        databody.Add(sortAttribute.GetArray(_10Float64_double).ToIceByte());
                        break;
                    default:
                        throw new NotSupportedException($"当前类型【{type}】暂时不支持");
                        break;
                }
            }

            var rowTypeArr = rowType.Select(a => (byte)a).ToArray();
            _packageData = new StreamPackageData(rowTypeArr, databody.ToArray());

            return _packageData;
        }

    }
}
