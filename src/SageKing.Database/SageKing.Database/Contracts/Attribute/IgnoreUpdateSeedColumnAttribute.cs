using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.Contracts;

/// <summary>
/// 忽略更新种子列特性（标记在实体属性）
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public class IgnoreUpdateSeedColumnAttribute : Attribute
{
}
