

using StackExchange.Profiling;
using StackExchange.Profiling.Storage;

namespace SageKing.Database.SqlSugar.AspNetCore.Features;

[DependsOn(typeof(SageKingDatabaseSqlSugarFeature))]
public class SageKingSqlSugarAspNetCoreFeature : FeatureBase
{
    public SageKingSqlSugarAspNetCoreFeature(IModule module) : base(module)
    {
    }

    /// <summary>
    /// 是否注入 MiniProfiler,默认注入
    //  app.UseMiniProfiler();
    //  该方法必须在app.UseEndpoints以前
    /// </summary>
    public bool InjectMiniProfiler { get; set; } = true;

    /// <summary>
    /// Represents the options for SageKingSqlSugarAspNetCores feature.
    /// </summary>
    public Action<SageKingSqlSugarAspNetCoreOptions> DatabaseOptions { get; set; } = _ => { };

    public Action<MiniProfilerOptions> MiniProfilerOptions { get; set; } = options =>
    {
        //访问地址路由根目录；默认为：/mini-profiler-resources
        options.RouteBasePath = "/profiler";
        //数据缓存时间
        (options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(60);
        //sql格式化设置
        options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();
        //跟踪连接打开关闭
        options.TrackConnectionOpenClose = true;
        //界面主题颜色方案;默认浅色
        options.ColorScheme = StackExchange.Profiling.ColorScheme.Dark;
        //.net core 3.0以上：对MVC过滤器进行分析
        options.EnableMvcFilterProfiling = true;
        //对视图进行分析
        options.EnableMvcViewProfiling = true;

        //控制访问页面授权，默认所有人都能访问
        //options.ResultsAuthorize;
        //要控制分析哪些请求，默认说有请求都分析
        //options.ShouldProfile;

        //内部异常处理
        //options.OnInternalError = e => MyExceptionLogger(e);
    };

    /// <inheritdoc />
    public override void Apply()
    {
        Services.Configure(DatabaseOptions);

        if (InjectMiniProfiler)
        {
            Services.AddMiniProfiler(MiniProfilerOptions);
        }

    }
}
