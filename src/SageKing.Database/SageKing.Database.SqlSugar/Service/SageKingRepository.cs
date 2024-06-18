using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.SqlSugar.Service
{
    /// <summary>
    /// 实现接口 分表 ISageKingSqlSplitRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SageKingSqlSplitRepository<T> : SqlSugarRepository<T>, ISageKingSqlSplitRepository<T>
        where T : class, new()
    {
        public SageKingSqlSplitRepository(ITenant iTenant) : base(iTenant)
        {
        }
    }

    /// <summary>
    /// 实现接口 ISqlSplitRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SageKingRepository<T> : SqlSugarRepository<T>, ISageKingRepository<T>
        where T : class, new()
    {
        public SageKingRepository(ITenant iTenant) : base(iTenant)
        {
        }
    }
}
