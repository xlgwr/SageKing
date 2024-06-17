using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.SqlSugar.Contracts;

/// <summary>
/// 自定义实体过滤器接口
/// </summary>
public interface IEntityFilter
{
    /// <summary>
    /// 实体过滤器
    /// </summary>
    /// <returns></returns>
    IEnumerable<TableFilterItem<object>> AddEntityFilter();
}

///// <summary>
///// 自定义业务实体过滤器示例
///// </summary>
//public class TestEntityFilter : IEntityFilter
//{
//    public IEnumerable<TableFilterItem<object>> AddEntityFilter()
//    {
//        // 构造自定义条件的过滤器
//        Expression<Func<SysUser, bool>> dynamicExpression = u => u.Remark.Contains("xxx");
//        var tableFilterItem = new TableFilterItem<object>(typeof(SysUser), dynamicExpression);

//        return new[]
//        {
//            tableFilterItem
//        };
//    }
//}