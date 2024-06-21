using Microsoft.Extensions.Configuration;
using SageKing.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC
{
    /// <summary>
    /// 客户端类型字典
    /// </summary>
    public class ClientTypeDicOptions : ConcurrentDictionary<int, string>, IOptionsBase
    {
        public string SectionName => "ClientTypeDic";

        public void BindFromConfig(IConfigurationManager configurationManager)
        {
            configurationManager.GetSection(SectionName).Bind(this);
        }

        public string GetDesc(int id)
        {
            if (this.TryGetValue(id, out string value))
            {
                return value;
            }
            return $"未知类型{id}";
        }
    }
}
