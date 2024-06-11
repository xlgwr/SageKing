using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts
{
    public interface IPushPackage<Tpackage>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="msg"></param>
        /// <param name="connectionId">为空时，推所有</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public bool PushStreamPackageListAsync(IEnumerable<Tpackage> param, string msg, string connectionId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
