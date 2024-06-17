using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.SqlSugar
{
    /// <summary>
    /// 配置字典
    /// </summary>
    public class SageKingDatabaseSqlSugarOptions : ConcurrentDictionary<string, string>
    {
        public const string SectionName = "SageKingDatabaseSqlSugar";

        public string Get(string id)
        {
            if (this.TryGetValue(id, out var value))
            {
                return value;
            }
            return $"未知类型{id}";
        }
    }
}
