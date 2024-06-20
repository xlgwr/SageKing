using SageKing.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.Contracts;

/// <summary>
/// 增量表特性
/// </summary>
[SuppressSniffer]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class IncreTableAttribute : Attribute
{
}