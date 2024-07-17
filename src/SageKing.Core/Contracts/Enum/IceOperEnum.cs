using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts
{
    public enum IceOperEnum
    {
        
        None = 0,
        [Description("发送")]
        Send = 1,
        [Description("推送")]
        Push = 2
    }
}
