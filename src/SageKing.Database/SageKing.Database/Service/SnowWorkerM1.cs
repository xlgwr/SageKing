using SageKing.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.Service;

public class SnowWorkerM1 : ISnowWorker
{
    protected readonly DateTime BaseTime;

    protected readonly ushort WorkerId;

    protected readonly byte WorkerIdBitLength;

    protected readonly byte SeqBitLength;

    protected readonly int MaxSeqNumber;

    protected readonly ushort MinSeqNumber;

    protected int TopOverCostCount;

    protected byte _TimestampShift;

    protected static object _SyncLock = new object();

    protected ushort _CurrentSeqNumber;

    protected long _LastTimeTick;

    protected long _TurnBackTimeTick;

    protected byte _TurnBackIndex;

    protected bool _IsOverCost;

    protected int _OverCostCountInOneTerm;

    protected int _GenCountInOneTerm;

    protected int _TermIndex;

    public Action<OverCostActionArg> GenAction { get; set; }

    public SnowWorkerM1(IdGeneratorOptions options)
    {
        if (options.BaseTime != DateTime.MinValue)
        {
            BaseTime = options.BaseTime;
        }

        if (options.WorkerIdBitLength == 0)
        {
            WorkerIdBitLength = 6;
        }
        else
        {
            WorkerIdBitLength = options.WorkerIdBitLength;
        }

        WorkerId = options.WorkerId;
        if (options.SeqBitLength == 0)
        {
            SeqBitLength = 6;
        }
        else
        {
            SeqBitLength = options.SeqBitLength;
        }

        if (MaxSeqNumber == 0)
        {
            MaxSeqNumber = (1 << (int)SeqBitLength) - 1;
        }
        else
        {
            MaxSeqNumber = options.MaxSeqNumber;
        }

        MinSeqNumber = options.MinSeqNumber;
        TopOverCostCount = options.TopOverCostCount;
        if (TopOverCostCount == 0)
        {
            TopOverCostCount = 2000;
        }

        _TimestampShift = (byte)(WorkerIdBitLength + SeqBitLength);
        _CurrentSeqNumber = options.MinSeqNumber;
    }

    private void DoGenIdAction(OverCostActionArg arg)
    {
        Task.Run(delegate
        {
            GenAction(arg);
        });
    }

    private void BeginOverCostAction(in long useTimeTick)
    {
    }

    private void EndOverCostAction(in long useTimeTick)
    {
        if (_TermIndex > 10000)
        {
            _TermIndex = 0;
        }
    }

    private void BeginTurnBackAction(in long useTimeTick)
    {
    }

    private void EndTurnBackAction(in long useTimeTick)
    {
    }

    protected virtual long NextOverCostId()
    {
        long useTimeTick = GetCurrentTimeTick();
        if (useTimeTick > _LastTimeTick)
        {
            EndOverCostAction(in useTimeTick);
            _LastTimeTick = useTimeTick;
            _CurrentSeqNumber = MinSeqNumber;
            _IsOverCost = false;
            _OverCostCountInOneTerm = 0;
            _GenCountInOneTerm = 0;
            return CalcId(in _LastTimeTick);
        }

        if (_OverCostCountInOneTerm >= TopOverCostCount)
        {
            EndOverCostAction(in useTimeTick);
            _LastTimeTick = GetNextTimeTick();
            _CurrentSeqNumber = MinSeqNumber;
            _IsOverCost = false;
            _OverCostCountInOneTerm = 0;
            _GenCountInOneTerm = 0;
            return CalcId(in _LastTimeTick);
        }

        if (_CurrentSeqNumber > MaxSeqNumber)
        {
            _LastTimeTick++;
            _CurrentSeqNumber = MinSeqNumber;
            _IsOverCost = true;
            _OverCostCountInOneTerm++;
            _GenCountInOneTerm++;
            return CalcId(in _LastTimeTick);
        }

        _GenCountInOneTerm++;
        return CalcId(in _LastTimeTick);
    }

    protected virtual long NextNormalId()
    {
        long useTimeTick = GetCurrentTimeTick();
        if (useTimeTick < _LastTimeTick)
        {
            if (_TurnBackTimeTick < 1)
            {
                _TurnBackTimeTick = _LastTimeTick - 1;
                _TurnBackIndex++;
                if (_TurnBackIndex > 4)
                {
                    _TurnBackIndex = 1;
                }

                BeginTurnBackAction(in _TurnBackTimeTick);
            }

            return CalcTurnBackId(in _TurnBackTimeTick);
        }

        if (_TurnBackTimeTick > 0)
        {
            EndTurnBackAction(in _TurnBackTimeTick);
            _TurnBackTimeTick = 0L;
        }

        if (useTimeTick > _LastTimeTick)
        {
            _LastTimeTick = useTimeTick;
            _CurrentSeqNumber = MinSeqNumber;
            return CalcId(in _LastTimeTick);
        }

        if (_CurrentSeqNumber > MaxSeqNumber)
        {
            BeginOverCostAction(in useTimeTick);
            _TermIndex++;
            _LastTimeTick++;
            _CurrentSeqNumber = MinSeqNumber;
            _IsOverCost = true;
            _OverCostCountInOneTerm = 1;
            _GenCountInOneTerm = 1;
            return CalcId(in _LastTimeTick);
        }

        return CalcId(in _LastTimeTick);
    }

    protected virtual long CalcId(in long useTimeTick)
    {
        long result = (useTimeTick << (int)_TimestampShift) + (long)((ulong)WorkerId << (int)SeqBitLength) + _CurrentSeqNumber;
        _CurrentSeqNumber++;
        return result;
    }

    protected virtual long CalcTurnBackId(in long useTimeTick)
    {
        long result = (useTimeTick << (int)_TimestampShift) + (long)((ulong)WorkerId << (int)SeqBitLength) + _TurnBackIndex;
        _TurnBackTimeTick--;
        return result;
    }

    protected virtual long GetCurrentTimeTick()
    {
        return (long)(DateTime.UtcNow - BaseTime).TotalMilliseconds;
    }

    protected virtual long GetNextTimeTick()
    {
        long currentTimeTick;
        for (currentTimeTick = GetCurrentTimeTick(); currentTimeTick <= _LastTimeTick; currentTimeTick = GetCurrentTimeTick())
        {
        }

        return currentTimeTick;
    }

    public virtual long NextId()
    {
        lock (_SyncLock)
        {
            return _IsOverCost ? NextOverCostId() : NextNormalId();
        }
    }
}
