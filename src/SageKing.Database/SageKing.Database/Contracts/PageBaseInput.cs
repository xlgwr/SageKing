using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SageKing.Database.Contracts;

/// <summary>
/// 全局分页查询输入参数
/// </summary>
public class PageBaseInput
{
    /// <summary>
    /// 当前页码
    /// </summary>
    public virtual int Page { get; set; } = 1;

    /// <summary>
    /// 页码容量
    /// </summary>
    [Range(0, 1000, ErrorMessage = "页码容量超过最大限制")]
    public virtual int PageSize { get; set; } = 20;

    /// <summary>
    /// 排序字段
    /// </summary>
    public virtual string Field { get; set; }

    /// <summary>
    /// 排序方向
    /// </summary>
    public virtual string Order { get; set; }

    /// <summary>
    /// 降序排序
    /// </summary>
    public virtual string DescStr { get; set; } = "descending";
}
