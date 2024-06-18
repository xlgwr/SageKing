using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Options
{
    public class IdGeneratorOptions
    {
        public virtual short Method { get; set; } = 1;


        public virtual DateTime BaseTime { get; set; } = new DateTime(2020, 2, 20, 2, 20, 2, 20, DateTimeKind.Utc);


        public virtual ushort WorkerId { get; set; }

        public virtual byte WorkerIdBitLength { get; set; } = 6;


        public virtual byte SeqBitLength { get; set; } = 6;


        public virtual int MaxSeqNumber { get; set; }

        public virtual ushort MinSeqNumber { get; set; } = 5;


        public virtual int TopOverCostCount { get; set; } = 2000;


        public virtual uint DataCenterId { get; set; }

        public virtual byte DataCenterIdBitLength { get; set; }

        public virtual byte TimestampType { get; set; }

        public IdGeneratorOptions()
        {
        }

        public IdGeneratorOptions(ushort workerId)
        {
            WorkerId = workerId;
        }
    }

    /// <summary>
    /// 雪花Id配置选项
    /// </summary>
    public sealed class SnowIdOptions : IdGeneratorOptions
    {
        /// <summary>
        /// 缓存前缀
        /// </summary>
        public string WorkerPrefix { get; set; }
    }
}
