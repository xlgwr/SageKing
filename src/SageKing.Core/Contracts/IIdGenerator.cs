using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts
{
    public interface IIdGenerator
    {
        Action<OverCostActionArg> GenIdActionAsync { get; set; }

        long NewLong();

        long NextId();
    }
}
