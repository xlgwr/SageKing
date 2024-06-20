using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Attributes;

/// <summary>
/// 验证类型特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Enum)]
public sealed class ValidationTypeAttribute : Attribute
{
}