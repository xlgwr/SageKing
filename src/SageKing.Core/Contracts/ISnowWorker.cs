using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts
{
    public interface ISnowWorker
    {
        Action<OverCostActionArg> GenAction { get; set; }

        long NextId();
    }
}
