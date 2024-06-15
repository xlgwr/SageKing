using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Contracts
{

    public record class SageKingMessage : ISageKingMessage
    {
        bool _isChange = false;

        StreamPackageData _packageData;

        /// <summary>
        /// 属性值
        /// </summary>
        ConcurrentDictionary<string, (object value, DataStreamTypeEnum type)> _AttributeValue;

        /// <summary>
        /// 属性位置
        /// 用于解包定位
        /// DataStreamTypeEnum 使有带有MapCsharp枚举
        /// </summary>
        ConcurrentDictionary<DataStreamTypeEnum, ConcurrentDictionary<string, int>> _AttributePosition;

        public SageKingMessage()
        {
            _isChange = false;
            _AttributeValue = new ConcurrentDictionary<string, object>();
            _AttributePosition = new ConcurrentDictionary<DataStreamTypeEnum, ConcurrentDictionary<string, int>>();
        }

        public bool AddOrUpdate(string attributeName, (object value, DataStreamTypeEnum type) value)
        {
            if (!_AttributeValue.ContainsKey(attributeName))
            {
                if (_AttributePosition.TryGetValue(value.type, out var pos))
                {
                    int count = pos.Count;
                    pos[attributeName] = count;
                }
                else
                {
                    _AttributePosition[value.type] = new ConcurrentDictionary<string, int>()
                    {
                        { attributeName,0}
                    };
                }
            }

            _AttributeValue[attributeName] = value;
            _isChange = true;
        }

        public (object value, DataStreamTypeEnum type) Get(string attributeName)
        {
            if (_AttributeValue.TryGetValue(attributeName, out var value))
            {
                return value;
            }
            return null;
        }

        public bool Remove<T>(string attributeName)
        {
            if (_AttributeValue.TryRemove(attributeName, out var value){

                if (_AttributePosition.TryGetValue(value.type, out var type))
                {
                    type.TryRemove(attributeName, out _);
                }
                _isChange = true;
            };
        }

        public void LoadData(StreamPackageData packageData)
        {


            //清理
            _AttributeValue.Clear();
            _packageData = packageData;
            _isChange = false;
        }


        public StreamPackageData ToData()
        {
            if (!_isChange)
            {
                return _packageData;
            }

            //todo
        }
    }
}
