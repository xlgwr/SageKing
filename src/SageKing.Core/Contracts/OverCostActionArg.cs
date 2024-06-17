using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

public class OverCostActionArg
{
    public virtual int ActionType { get; set; }

    public virtual long TimeTick { get; set; }

    public virtual ushort WorkerId { get; set; }

    public virtual int OverCostCountInOneTerm { get; set; }

    public virtual int GenCountInOneTerm { get; set; }

    public virtual int TermIndex { get; set; }

    public OverCostActionArg(ushort workerId, long timeTick, int actionType = 0, int overCostCountInOneTerm = 0, int genCountWhenOverCost = 0, int index = 0)
    {
        ActionType = actionType;
        TimeTick = timeTick;
        WorkerId = workerId;
        OverCostCountInOneTerm = overCostCountInOneTerm;
        GenCountInOneTerm = genCountWhenOverCost;
        TermIndex = index;
    }
}
