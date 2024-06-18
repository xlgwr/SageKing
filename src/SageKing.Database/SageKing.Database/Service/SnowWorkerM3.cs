using SageKing.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.Service
{
    public class SnowWorkerM3 : SnowWorkerM1
    {
        protected readonly uint DataCenterId;

        protected readonly byte DataCenterIdBitLength;

        protected readonly byte TimestampType;

        public SnowWorkerM3(IdGeneratorOptions options)
            : base(options)
        {
            TimestampType = options.TimestampType;
            DataCenterId = options.DataCenterId;
            DataCenterIdBitLength = options.DataCenterIdBitLength;
            if (TimestampType == 1)
            {
                TopOverCostCount = 0;
            }

            _TimestampShift = (byte)(DataCenterIdBitLength + WorkerIdBitLength + SeqBitLength);
        }

        protected override long CalcId(in long useTimeTick)
        {
            long result = (useTimeTick << (int)_TimestampShift) + (long)((ulong)DataCenterId << (int)DataCenterIdBitLength) + (long)((ulong)WorkerId << (int)SeqBitLength) + _CurrentSeqNumber;
            _CurrentSeqNumber++;
            return result;
        }

        protected override long CalcTurnBackId(in long useTimeTick)
        {
            long result = (useTimeTick << (int)_TimestampShift) + (long)((ulong)DataCenterId << (int)DataCenterIdBitLength) + (long)((ulong)WorkerId << (int)SeqBitLength) + _TurnBackIndex;
            _TurnBackTimeTick--;
            return result;
        }

        protected override long GetCurrentTimeTick()
        {
            if (TimestampType != 0)
            {
                return (long)(DateTime.UtcNow - BaseTime).TotalSeconds;
            }

            return (long)(DateTime.UtcNow - BaseTime).TotalMilliseconds;
        }
    }
}
