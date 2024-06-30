using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Cache.Contracts;

/// <summary>
/// 主要用于下拉选项缓存功能
/// </summary>
public interface ISelectTypeCache<K, V>
{
    public IList<KeyValue<K, V>> GetCache(int start = 0, int end = 0);
}
