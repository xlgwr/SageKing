﻿namespace SageKing.Database;

/// <summary>
/// 日志表特性
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class LogTableAttribute : Attribute
{
}