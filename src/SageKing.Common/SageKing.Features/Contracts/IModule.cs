namespace SageKing.Features.Contracts;

/// <summary>
/// 请参阅 <see cref="IServiceCollection"/> 之上的精简抽象，以帮助组织特性和依赖项。
/// </summary>
public interface IModule
{
    /// <summary>
    /// 正在填充的服务集合
    /// </summary>
    IServiceCollection Services { get; }

    /// <summary>
    /// 一个字典属性，功能可以将值存储到其中以备将来使用。
    /// </summary>
    IDictionary<object, object> Properties { get; }

    /// <summary>
    /// 如果已配置指定类型的功能，则返回true
    /// </summary>
    bool HasFeature<T>() where T : class, IFeature;

    /// <summary>
    /// 如果已配置指定类型的功能，则返回true。
    /// </summary>
    bool HasFeature(Type featureType);

    /// <summary>
    /// 创建和配置指定类型的功能。
    /// </summary>
    T Configure<T>(Action<T>? configure = default) where T : class, IFeature;

    /// <summary>
    /// 创建和配置指定类型的功能。
    /// </summary>
    T Configure<T>(Func<IModule, T> factory, Action<T>? configure = default) where T : class, IFeature;

    /// <summary>
    /// 使用可选优先级配置 <see cref="IHostedService"/> 以控制向服务容器注册的顺序。
    /// </summary>
    IModule ConfigureHostedService<T>(int priority = 0) where T : class, IHostedService;

    /// <summary>
    /// 使用可选优先级配置 <see cref="IHostedService"/> 以控制向服务容器注册的顺序。
    /// </summary>
    IModule ConfigureHostedService(Type hostedServiceType, int priority = 0);

    /// <summary>
    /// 将应用所有配置的功能，导致填充 <see cref="Services"/> 集合
    /// </summary>
    void Apply();

    /// <summary>
    /// 初始化类，Use init
    /// </summary>
    void Init();
}