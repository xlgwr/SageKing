using SageKing.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.Service;

public class DefaultIdGenerator : IIdGenerator
{
    private ISnowWorker _SnowWorker { get; set; }

    public Action<OverCostActionArg> GenIdActionAsync
    {
        get
        {
            return _SnowWorker.GenAction;
        }
        set
        {
            _SnowWorker.GenAction = value;
        }
    }

    public DefaultIdGenerator(IdGeneratorOptions options)
    {
        if (options == null)
        {
            throw new ApplicationException("options error.");
        }

        if (options.BaseTime < DateTime.Now.AddYears(-50) || options.BaseTime > DateTime.Now)
        {
            throw new ApplicationException("BaseTime error.");
        }

        int num = ((options.TimestampType == 0) ? 22 : 31);
        if (options.WorkerIdBitLength <= 0)
        {
            throw new ApplicationException("WorkerIdBitLength error.(range:[1, 21])");
        }

        if (options.DataCenterIdBitLength + options.WorkerIdBitLength + options.SeqBitLength > num)
        {
            throw new ApplicationException("error：DataCenterIdBitLength + WorkerIdBitLength + SeqBitLength <= " + num);
        }

        int num2 = (1 << (int)options.WorkerIdBitLength) - 1;
        if (num2 == 0)
        {
            num2 = 63;
        }

        if (options.WorkerId < 0 || options.WorkerId > num2)
        {
            throw new ApplicationException("WorkerId error. (range:[0, " + num2 + "]");
        }

        int num3 = (1 << (int)options.DataCenterIdBitLength) - 1;
        if (options.DataCenterId < 0 || options.DataCenterId > num3)
        {
            throw new ApplicationException("DataCenterId error. (range:[0, " + num3 + "]");
        }

        if (options.SeqBitLength < 2 || options.SeqBitLength > 21)
        {
            throw new ApplicationException("SeqBitLength error. (range:[2, 21])");
        }

        int num4 = (1 << (int)options.SeqBitLength) - 1;
        if (num4 == 0)
        {
            num4 = 63;
        }

        if (options.MaxSeqNumber < 0 || options.MaxSeqNumber > num4)
        {
            throw new ApplicationException("MaxSeqNumber error. (range:[1, " + num4 + "]");
        }

        if (options.MinSeqNumber < 5 || options.MinSeqNumber > num4)
        {
            throw new ApplicationException("MinSeqNumber error. (range:[5, " + num4 + "]");
        }

        if (options.Method == 2)
        {
            _SnowWorker = new SnowWorkerM2(options);
        }
        else if (options.DataCenterIdBitLength == 0 && options.TimestampType == 0)
        {
            _SnowWorker = new SnowWorkerM1(options);
        }
        else
        {
            _SnowWorker = new SnowWorkerM3(options);
        }

        if (options.Method != 2)
        {
            Thread.Sleep(500);
        }
    }

    public long NewLong()
    {
        return _SnowWorker.NextId();
    }

    public long NextId()
    {
        return _SnowWorker.NextId();
    }
}
