using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.Service
{
    /// <summary>
    /// 雪花ID
    /// </summary>
    public class SnowIdGenerator : DefaultIdGenerator
    {
        public SnowIdGenerator(IOptions<SageKingDatabaseOptions> options) : base(options.Value.SnowId) { }
    }
}
