
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Application.AspNetCore.SqlSugar.Contracts.Entity;

/// <summary>
/// SageKing消息结构表
/// </summary>
[SysTable]
[SugarTable(null, "SageKing消息结构表")]
[SugarIndex("index_{table}_Name", new[] { nameof(Name), nameof(Version) }, new[] { OrderByType.Asc, OrderByType.Asc }, isUnique: true)]
public partial class SysSageKingMessage : EntityTenantBaseData
{

    /// <summary>
    /// 名称
    /// </summary>
    [Required, MaxLength(64)]
    [SugarColumn(ColumnDescription = "名称", Length = 64)]
    public virtual string Name { get; set; }

    [Required]
    [SugarColumn(ColumnDescription = "版本")]
    public virtual long Version { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    [Required]
    [SugarColumn(ColumnDescription = "类型")]
    public virtual int Type { get; set; }

    [SugarColumn(ColumnDescription = "描述")]
    public string? Description { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnDescription = "状态")]
    public bool Status { get; set; } = false;

    /// <summary>
    /// 属性集合
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(SysSageKingMessageAttribute.MessageId))]
    public List<SysSageKingMessageAttribute> Children { get; set; }
}
