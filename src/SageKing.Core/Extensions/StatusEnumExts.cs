using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Extensions;

public static class StatusEnumExts
{
    public static bool ToSwitch(this StatusEnum status)
    {
        switch (status)
        {
            case StatusEnum.Enable:
                return true;
            default:
                return false;
        }
    }
}
