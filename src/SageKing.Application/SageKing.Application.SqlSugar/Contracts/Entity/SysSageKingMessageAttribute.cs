
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Application.SqlSugar.Contracts.Entity;

/// <summary>
/// SageKing消息结构属性表
/// </summary>
[SysTable]
[SugarTable(null, "SageKing消息结构属性表")]
[SugarIndex("index_{table}_Unique_MessageId_Name", new[] { nameof(MessageId), nameof(Name) }, new[] { OrderByType.Desc, OrderByType.Asc }, isUnique: true)]
public partial class SysSageKingMessageAttribute : EntityBase
{

    [Required]
    [SugarColumn(ColumnDescription = "SageKing消息结构Id")]
    public virtual long MessageId { get; set; }

    /// <summary>
    /// 属性名称
    /// </summary>
    [Required, MaxLength(64)]
    [SugarColumn(ColumnDescription = "属性名称", Length = 64)]
    public virtual string Name { get; set; }

    [Required]
    [SugarColumn(ColumnDescription = "类型")]
    public DataStreamTypeEnum Type { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnDescription = "状态")]
    public StatusEnum Status { get; set; } = StatusEnum.Enable;


    [SugarColumn(ColumnDescription = "描述")]
    public virtual string? Description { get; set; }

}
