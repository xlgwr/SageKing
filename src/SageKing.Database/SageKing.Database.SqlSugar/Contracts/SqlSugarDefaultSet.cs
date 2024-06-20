using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.SqlSugar.Contracts
{
    public class SqlSugarDefaultSet
    {
        /// <summary>
        /// 默认主数据库标识（默认租户）1100000000001
        /// </summary>
        public string MainConfigId { get; set; } = "1100000000001";

        /// <summary>
        /// 默认日志数据库标识 1100000000002
        /// </summary>
        public string LogConfigId { get; set; } = "1100000000002";

        /// <summary>
        /// 默认表主键 Id
        /// </summary>
        public string PrimaryKey { get; set; } = "Id";

        /// <summary>
        /// 默认租户Id 1200000000001
        /// </summary>
        public long DefaultTenantId { get; set; } = 1200000000001;
    }
}
