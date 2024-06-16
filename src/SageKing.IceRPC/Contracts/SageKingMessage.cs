using SageKing.Core.Extensions;
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
        private bool _isChange = false;
        private StreamPackageData _packageData;

        #region 数据 AttributeValue

        /// <summary>
        /// 属性值 
        /// </summary>
        private ConcurrentDictionary<string, string> _string0;
        private ConcurrentDictionary<string, sbyte> _int8Arr_sbyte1;
        private ConcurrentDictionary<string, byte> _uint8Arr_byte2;
        private ConcurrentDictionary<string, short> _int16_short3;
        private ConcurrentDictionary<string, ushort> _uint16_ushort4;
        private ConcurrentDictionary<string, int> _int32_int5;
        private ConcurrentDictionary<string, uint> _uint32_uint6;
        private ConcurrentDictionary<string, long> _int64_long7;
        private ConcurrentDictionary<string, ulong> _uint64_ulong8;
        private ConcurrentDictionary<string, float> _float32_float9;
        private ConcurrentDictionary<string, double> _float64_double10;
        #endregion

        /// <summary>
        /// 属性位置
        /// 用于解包定位
        /// </summary>
        private readonly ConcurrentDictionary<DataStreamTypeEnum, ConcurrentDictionary<string, int>> _attributePosition;

        public SageKingMessage()
        {
            _isChange = false;
            _attributePosition = new();
        }

        public string Id { get; set; }

        public string Varsion { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public string Description { get; set; }

        #region AddOrUpdate

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<string> value)
        {
            if (_string0 == null)
            {
                _string0 = new();
            }
            _isChange = true;
            return _string0.AddOrUpdatePost(attributeName, value, _attributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<sbyte> value)
        {
            if (_int8Arr_sbyte1 == null)
            {
                _int8Arr_sbyte1 = new();
            }
            _isChange = true;
            return _int8Arr_sbyte1.AddOrUpdatePost(attributeName, value, _attributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<byte> value)
        {
            if (_uint8Arr_byte2 == null)
            {
                _uint8Arr_byte2 = new();
            }
            _isChange = true;
            return _uint8Arr_byte2.AddOrUpdatePost(attributeName, value, _attributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<short> value)
        {
            if (_int16_short3 == null)
            {
                _int16_short3 = new();
            }
            _isChange = true;
            return _int16_short3.AddOrUpdatePost(attributeName, value, _attributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<int> value)
        {
            if (_int32_int5 == null)
            {
                _int32_int5 = new();
            }
            _isChange = true;
            return _int32_int5.AddOrUpdatePost(attributeName, value, _attributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<uint> value)
        {
            if (_uint32_uint6 == null)
            {
                _uint32_uint6 = new();
            }
            _isChange = true;
            return _uint32_uint6.AddOrUpdatePost(attributeName, value, _attributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<long> value)
        {
            if (_int64_long7 == null)
            {
                _int64_long7 = new();
            }
            _isChange = true;
            return _int64_long7.AddOrUpdatePost(attributeName, value, _attributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<ulong> value)
        {
            if (_uint64_ulong8 == null)
            {
                _uint64_ulong8 = new();
            }
            _isChange = true;
            return _uint64_ulong8.AddOrUpdatePost(attributeName, value, _attributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<float> value)
        {
            if (_float32_float9 == null)
            {
                _float32_float9 = new();
            }
            _isChange = true;
            return _float32_float9.AddOrUpdatePost(attributeName, value, _attributePosition);
        }

        public bool AddOrUpdate(string attributeName, DataStreamTypValue<double> value)
        {
            if (_float64_double10 == null)
            {
                _float64_double10 = new();
            }
            _isChange = true;
            return _float64_double10.AddOrUpdatePost(attributeName, value, _attributePosition);
        }

        #endregion

        #region Get
        public string Get(string attributeName)
        {
            return _string0.GetDefault(attributeName, string.Empty);
        }

        public sbyte Getsbyte(string attributeName)
        {
            return _int8Arr_sbyte1.GetDefault(attributeName);
        }

        public byte Getbyte(string attributeName)
        {
            return _uint8Arr_byte2.GetDefault(attributeName);
        }

        public short Getshort(string attributeName)
        {
            return _int16_short3.GetDefault(attributeName);
        }

        public ushort Getushort(string attributeName)
        {
            return _uint16_ushort4.GetDefault(attributeName);
        }

        public int Getint(string attributeName)
        {
            return _int32_int5.GetDefault(attributeName);
        }

        public uint Getuint(string attributeName)
        {
            return _uint32_uint6.GetDefault(attributeName);
        }

        public long Getlong(string attributeName)
        {
            return _int64_long7.GetDefault(attributeName);
        }

        public ulong Getulong(string attributeName)
        {
            return _uint64_ulong8.GetDefault(attributeName);
        }

        public float Getfloat(string attributeName)
        {
            return _float32_float9.GetDefault(attributeName);
        }

        public double Getdouble(string attributeName)
        {
            return _float64_double10.GetDefault(attributeName);
        }

        #endregion

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
                if (!Enum.IsDefined(typeof(DataStreamTypeEnum), rowType))
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
                        _string0 = new();
                        temp0string.GetPostData<string>(_string0, _attributePosition);
                        break;
                    case DataStreamTypeEnum.Int8Arr:
                        temp1Int8Arr_sbyte = new DataStreamTypValue<sbyte[]>(getTypeMenu, currDatabyte.Getsbyte());
                        _int8Arr_sbyte1 = new();
                        temp1Int8Arr_sbyte.GetPostData<sbyte>(_int8Arr_sbyte1, _attributePosition);
                        break;
                    case DataStreamTypeEnum.Uint8Arr:
                        temp2Uint8Arr_byte = new DataStreamTypValue<byte[]>(getTypeMenu, currDatabyte.Getbyte());
                        _uint8Arr_byte2 = new();
                        temp2Uint8Arr_byte.GetPostData<byte>(_uint8Arr_byte2, _attributePosition);
                        break;
                    case DataStreamTypeEnum.Int16Arr:
                        temp3Int16_short = new DataStreamTypValue<short[]>(getTypeMenu, currDatabyte.Getshort());
                        _int16_short3 = new();
                        temp3Int16_short.GetPostData<short>(_int16_short3, _attributePosition);
                        break;
                    case DataStreamTypeEnum.Uint16Arr:
                        temp4uint16_ushort = new DataStreamTypValue<ushort[]>(getTypeMenu, currDatabyte.Getushort());
                        _uint16_ushort4 = new();
                        temp4uint16_ushort.GetPostData<ushort>(_uint16_ushort4, _attributePosition);
                        break;
                    case DataStreamTypeEnum.Int32Arr:
                        temp5int32_int = new DataStreamTypValue<int[]>(getTypeMenu, currDatabyte.Getint());
                        _int32_int5 = new();
                        temp5int32_int.GetPostData<int>(_int32_int5, _attributePosition);
                        break;
                    case DataStreamTypeEnum.Uint32Arr:
                        temp6uint32_uint = new DataStreamTypValue<uint[]>(getTypeMenu, currDatabyte.Getuint());
                        _uint32_uint6 = new();
                        temp6uint32_uint.GetPostData<uint>(_uint32_uint6, _attributePosition);
                        break;
                    case DataStreamTypeEnum.Int64Arr:
                        temp7int64_long = new DataStreamTypValue<long[]>(getTypeMenu, currDatabyte.Getlong());
                        _int64_long7 = new();
                        temp7int64_long.GetPostData<long>(_int64_long7, _attributePosition);
                        break;
                    case DataStreamTypeEnum.Uint64Arr:
                        temp8Uint64_ulong = new DataStreamTypValue<ulong[]>(getTypeMenu, currDatabyte.Getulong());
                        _uint64_ulong8 = new();
                        temp8Uint64_ulong.GetPostData<ulong>(_uint64_ulong8, _attributePosition);
                        break;
                    case DataStreamTypeEnum.Float32Arr:
                        temp9Float32_float = new DataStreamTypValue<float[]>(getTypeMenu, currDatabyte.Getfloat());
                        _float32_float9 = new();
                        temp9Float32_float.GetPostData<float>(_float32_float9, _attributePosition);
                        break;
                    case DataStreamTypeEnum.Float64Arr:
                        temp10Float64_double = new DataStreamTypValue<double[]>(getTypeMenu, currDatabyte.Getdouble());
                        _float64_double10 = new();
                        temp10Float64_double.GetPostData<double>(_float64_double10, _attributePosition);
                        break;
                    default:
                        throw new NotSupportedException($"当前类型【{getTypeMenu}】暂时不支持");
                        break;
                }

                index++;
            }

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

            var rowType = _attributePosition.Keys.OrderBy(a => a);

            List<byte[]> databody = new List<byte[]>();

            foreach (var type in rowType)
            {
                var itemPos = _attributePosition[type];
                var sortAttribute = itemPos.OrderBy(a => a.Value).Select(a => a.Key).ToList();

                switch (type)
                {
                    case DataStreamTypeEnum.StringArr:
                        databody.Add(sortAttribute.GetArray(_string0).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Int8Arr:
                        databody.Add(sortAttribute.GetArray(_int8Arr_sbyte1).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Uint8Arr:
                        databody.Add(sortAttribute.GetArray(_uint8Arr_byte2).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Int16Arr:
                        databody.Add(sortAttribute.GetArray(_int16_short3).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Uint16Arr:
                        databody.Add(sortAttribute.GetArray(_uint16_ushort4).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Int32Arr:
                        databody.Add(sortAttribute.GetArray(_int32_int5).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Uint32Arr:
                        databody.Add(sortAttribute.GetArray(_uint32_uint6).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Int64Arr:
                        databody.Add(sortAttribute.GetArray(_int64_long7).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Uint64Arr:
                        databody.Add(sortAttribute.GetArray(_uint64_ulong8).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Float32Arr:
                        databody.Add(sortAttribute.GetArray(_float32_float9).ToIceByte());
                        break;
                    case DataStreamTypeEnum.Float64Arr:
                        databody.Add(sortAttribute.GetArray(_float64_double10).ToIceByte());
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

        public bool InitAttribytePos(Dictionary<DataStreamTypeEnum, Dictionary<string, int>> posDic)
        {
            return posDic.InitConcurrentDictionary(_attributePosition);
        }
        public Dictionary<DataStreamTypeEnum, Dictionary<string, int>> GetPosData()
        {
            return _attributePosition.GetDictionary();
        }
        public bool Remove(string attributeName, DataStreamTypeEnum type)
        {
            switch (type)
            {
                case DataStreamTypeEnum.StringArr:
                    return _string0.RemovePost(attributeName, type, _attributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Int8Arr:
                    return _int8Arr_sbyte1.RemovePost(attributeName, type, _attributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Uint8Arr:
                    return _uint8Arr_byte2.RemovePost(attributeName, type, _attributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Int16Arr:
                    return _int16_short3.RemovePost(attributeName, type, _attributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Uint16Arr:
                    return _uint16_ushort4.RemovePost(attributeName, type, _attributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Int32Arr:
                    return _int32_int5.RemovePost(attributeName, type, _attributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Uint32Arr:
                    return _uint32_uint6.RemovePost(attributeName, type, _attributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Int64Arr:
                    return _int64_long7.RemovePost(attributeName, type, _attributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Uint64Arr:
                    return _uint64_ulong8.RemovePost(attributeName, type, _attributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Float32Arr:
                    return _float32_float9.RemovePost(attributeName, type, _attributePosition, ref _isChange);
                    break;
                case DataStreamTypeEnum.Float64Arr:
                    return _float64_double10.RemovePost(attributeName, type, _attributePosition, ref _isChange);
                    break;
                default:
                    throw new NotSupportedException($"当前类型【{type}】暂时不支持");
                    break;
            }
            return true;
        }

        public void ClearData()
        {
            foreach (var type in _attributePosition.Keys)
            {
                switch (type)
                {
                    case DataStreamTypeEnum.StringArr:
                        _string0.Clear();
                        break;
                    case DataStreamTypeEnum.Int8Arr:
                        _int8Arr_sbyte1.Clear();
                        break;
                    case DataStreamTypeEnum.Uint8Arr:
                        _uint8Arr_byte2.Clear();
                        break;
                    case DataStreamTypeEnum.Int16Arr:
                        _int16_short3.Clear();
                        break;
                    case DataStreamTypeEnum.Uint16Arr:
                        _uint16_ushort4.Clear();
                        break;
                    case DataStreamTypeEnum.Int32Arr:
                        _int32_int5.Clear();
                        break;
                    case DataStreamTypeEnum.Uint32Arr:
                        _uint32_uint6.Clear();
                        break;
                    case DataStreamTypeEnum.Int64Arr:
                        _int64_long7.Clear();
                        break;
                    case DataStreamTypeEnum.Uint64Arr:
                        _uint64_ulong8.Clear();
                        break;
                    case DataStreamTypeEnum.Float32Arr:
                        _float32_float9.Clear();
                        break;
                    case DataStreamTypeEnum.Float64Arr:
                        _float64_double10.Clear();
                        break;
                    default:
                        throw new NotSupportedException($"当前类型【{type}】暂时不支持");
                        break;
                }
            }
        }

    }
}
