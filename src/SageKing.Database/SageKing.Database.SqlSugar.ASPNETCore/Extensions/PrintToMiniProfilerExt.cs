using Microsoft.AspNetCore.Http;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.SqlSugar.AspNetCore.Extensions;

public static class PrintToMiniProfilerExt
{
    public static void PrintToMiniProfiler(this string category, string state, string message = null, bool isError = false)
    {
        // 打印消息
        var titleCaseCategory = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(category);
        var customTiming = MiniProfiler.Current?.CustomTiming(category, string.IsNullOrWhiteSpace(message) ? $"{titleCaseCategory} {state}" : message, state);
        if (customTiming == null) return;

        // 判断是否是警告消息
        if (isError) customTiming.Errored = true;
    } 

    /// <summary>
    /// 打印验证信息到 MiniProfiler
    /// </summary>
    /// <param name="category">分类</param>
    /// <param name="state">状态</param>
    /// <param name="message">消息</param>
    /// <param name="isError">是否为警告消息</param>
    public static void PrintToMiniProfiler(this HttpContext httpContext, string category, string state, string message = null, bool isError = false)
    {
        if (!CanBeMiniProfiler(httpContext)) return;

        // 打印消息
        var titleCaseCategory = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(category);
        var customTiming = MiniProfiler.Current?.CustomTiming(category, string.IsNullOrWhiteSpace(message) ? $"{titleCaseCategory} {state}" : message, state);
        if (customTiming == null) return;

        // 判断是否是警告消息
        if (isError) customTiming.Errored = true;
    }

    /// <summary>
    /// 判断是否启用 MiniProfiler
    /// </summary>
    /// <returns></returns>
    public static bool CanBeMiniProfiler(this HttpContext httpContext)
    {
        // 减少不必要的监听
        if (httpContext == null
            || !(httpContext.Request.Headers.TryGetValue("request-from", out var value) && value == "swagger")) return false;

        return true;
    }
}
