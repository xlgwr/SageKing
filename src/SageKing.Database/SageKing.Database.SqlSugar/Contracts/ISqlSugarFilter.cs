using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.SqlSugar.Contracts
{
    public interface ISqlSugarFilter
    {
        /// <summary>
        /// 删除用户机构缓存
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dbConfigId"></param>
        public void DeleteUserOrgCache(long userId, string dbConfigId);

        /// <summary>
        /// 配置用户机构集合过滤器
        /// </summary>
        /// <param name="db"></param>
        public void SetOrgEntityFilter(SqlSugarScopeProvider db);


        /// <summary>
        /// 配置用户仅本人数据过滤器
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public int SetDataScopeFilter(SqlSugarScopeProvider db);

        /// <summary>
        /// 配置自定义过滤器
        /// </summary>
        /// <param name="db"></param>
        public void SetCustomEntityFilter(SqlSugarScopeProvider db);
    }
}
