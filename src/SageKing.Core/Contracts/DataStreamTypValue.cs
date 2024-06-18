using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts
{
    public record class DataStreamTypValue<T>
    {
        DataStreamTypeEnum _streamType = DataStreamTypeEnum.None;

        public DataStreamTypValue(T value)
        {
            this.Value = value;
            this.ValueType = typeof(T);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">用Arr相关</param>
        /// <param name="value"></param>
        public DataStreamTypValue(DataStreamTypeEnum type, T value)
        {
            _streamType = type;
            this.Value = value;
        }

        public T Value { get; private set; }

        public Type ValueType { get; private set; }

        public DataStreamTypeEnum DataStreamType
        {
            get
            {
                if (_streamType != DataStreamTypeEnum.None)
                {
                    return _streamType;
                }
                if (this.ValueType == null)
                {
                    this.ValueType = typeof(T);
                }
                _streamType = CsharpTypeForICE.GetDataStreamTypeEnum(this.ValueType);

                return _streamType;
            }
        }
    }
}
