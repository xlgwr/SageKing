using JetBrains.Annotations;

namespace SageKing.Features.Contracts;

/// <summary>
/// 表示一个功能。
/// </summary>
[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors | ImplicitUseTargetFlags.Members)]
public interface IFeature
{
    /// <summary>
    /// 获取此功能所属的模块。
    /// </summary>
    IModule Module { get; }

    /// <summary>
    /// 配置功能。
    /// </summary>
    void Configure();

    /// <summary>
    /// 配置托管服务。
    /// </summary>
    void ConfigureHostedServices();

    /// <summary>
    /// 通过将服务添加到服务集合来应用该功能。
    /// </summary>
    void Apply();

}