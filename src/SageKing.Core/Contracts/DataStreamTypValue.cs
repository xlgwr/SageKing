using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts
{
    public record class DataStreamTypValue<T>
    {
        DataStreamTypeEnum _streamType = DataStreamTypeEnum.IceObject;

        public DataStreamTypValue(T value)
        {
            this.Value = value;
            this.ValueType = typeof(T);
        }
        public DataStreamTypValue(DataStreamTypeEnum type, T value)
        {
            _streamType = type;

            this.Value = value;
            this.ValueType = typeof(T);
        }

        public T Value { get; private set; }

        public Type ValueType { get; private set; }

        public DataStreamTypeEnum DataStreamType
        {
            get
            {
                if (_streamType != DataStreamTypeEnum.IceObject)
                {
                    return _streamType;
                }
                return CsharpTypeForICE.GetDataStreamTypeEnum(this.ValueType);
            }
        }
    }
}
