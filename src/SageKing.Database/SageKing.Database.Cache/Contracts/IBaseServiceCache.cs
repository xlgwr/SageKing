using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Cache.Contracts
{
    public interface IBaseServiceCache<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <param name="codeType"></param>
        public void CacheRefresh(string codeType);

        /// <summary>
        /// 获取详情缓存 🔖
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DisplayName("获取详情")]
        public Task<TEntity> GetDetailCache(string codeType);

    }
}
