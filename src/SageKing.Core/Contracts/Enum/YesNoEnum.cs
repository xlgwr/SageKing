using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

/// <summary>
/// 是否枚举
/// </summary>
[Description("是否枚举")]
public enum YesNoEnum
{
    /// <summary>
    /// 是
    /// </summary>
    [Description("是")]
    Y = 1,

    /// <summary>
    /// 否
    /// </summary>
    [Description("否")]
    N = 2
}
