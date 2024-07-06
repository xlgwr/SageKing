using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Application.AspNetCore.SqlSugar.Contracts;

/// <summary>
/// 系统菜单类型枚举
/// </summary> 
[Description("系统菜单类型枚举")]
public enum MenuTypeEnum
{
    /// <summary>
    /// 目录
    /// </summary>
    [Display(Name ="目录")]
    Dir = 1,

    /// <summary>
    /// 菜单
    /// </summary>
    [Display(Name = "菜单")]
    Menu = 2,

    /// <summary>
    /// 按钮
    /// </summary>
    [Display(Name = "按钮")]
    Btn = 3
}