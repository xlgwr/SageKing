﻿using System;
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

    }
}
