namespace SageKing.Features.Abstractions;

/// <summary>
/// 抽象基类，表示一个功能。 
/// </summary>
public abstract class FeatureBase : IFeature
{
    /// <summary>
    /// 构造函数
    /// </summary>
    protected FeatureBase(IModule module)
    {
        Module = module;
    }

    /// <summary>
    /// 此功能是模块的一部分。
    /// </summary>
    public IModule Module { get; }

    /// <summary>
    /// 对可以向其中添加服务的 <see cref="IServiceCollection"/> 的引用
    /// </summary>
    public IServiceCollection Services => Module.Services;

    /// <summary>
    /// 覆盖此方法可以配置功能。
    /// </summary>
    public virtual void Configure()
    {
    }

    /// <summary>
    /// 覆盖此方法以注册由功能提供的任何托管服务。
    /// </summary>
    public virtual void ConfigureHostedServices()
    {
    }

    /// <summary>
    /// 覆盖此项以使用 <see cref="Services"/>注册服务
    /// </summary>
    public virtual void Apply()
    {
    }

    public virtual void Init()
    {
    }
    /// <summary>
    /// 使用可选优先级配置指定的托管服务，以控制其在服务容器中注册的顺序。
    /// </summary>
    /// <param name="priority">优先级</param>
    /// <typeparam name="T">要配置的托管服务的类型。</typeparam>
    protected void ConfigureHostedService<T>(int priority = 0) where T : class, IHostedService
    {
        Module.ConfigureHostedService<T>(priority);
    }
}