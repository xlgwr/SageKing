using SageKing.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.Contracts;

/// <summary>
/// 忽略更新种子特性（标记在种子类）
/// </summary>
[SuppressSniffer]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class IgnoreUpdateSeedAttribute : Attribute
{
}