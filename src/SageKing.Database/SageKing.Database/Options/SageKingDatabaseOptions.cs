using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database
{
    /// <summary>
    /// 数据库连接配置字典
    /// </summary>
    public class SageKingDatabaseOptions
    {
        public const string SectionName = "SageKingDatabase";

        /// <summary>
        /// 雪花ID配置
        /// </summary>
        public SnowIdOptions SnowId { get; set; }

        /// <summary>
        /// 数据库相关
        /// </summary>
        public ConcurrentDictionary<string, string> Connections = new ConcurrentDictionary<string, string>();

        public string GetConnection(string id)
        {
            if (Connections.TryGetValue(id, out var value))
            {
                return value;
            }
            return $"未知类型{id}";
        }
    }
}
