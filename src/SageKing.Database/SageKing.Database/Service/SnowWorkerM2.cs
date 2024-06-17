using SageKing.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.Service
{
    public class SnowWorkerM2 : SnowWorkerM1
    {
        public SnowWorkerM2(IdGeneratorOptions options)
            : base(options)
        {
        }

        public override long NextId()
        {
            lock (SnowWorkerM1._SyncLock)
            {
                long num = GetCurrentTimeTick();
                if (_LastTimeTick == num)
                {
                    if (_CurrentSeqNumber++ > MaxSeqNumber)
                    {
                        _CurrentSeqNumber = MinSeqNumber;
                        num = GetNextTimeTick();
                    }
                }
                else
                {
                    _CurrentSeqNumber = MinSeqNumber;
                }

                if (num < _LastTimeTick)
                {
                    throw new Exception($"Time error for {_LastTimeTick - num} milliseconds");
                }

                _LastTimeTick = num;
                return (num << (int)_TimestampShift) + (long)((ulong)WorkerId << (int)SeqBitLength) + _CurrentSeqNumber;
            }
        }
    }
}
