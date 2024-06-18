namespace SageKing.Database.Contracts;

/// <summary>
/// 框架实体基类Id
/// </summary>
public interface IEntityBaseId
{
    public long Id { get; }
}

public interface IEntityBase : IEntityBaseId, IDeletedFilter
{
    public DateTime CreateTime { get; }

    public DateTime? UpdateTime { get; }

    /// <summary>
    /// 创建者Id
    /// </summary>
    public long? CreateUserId { get; }

    /// <summary>
    /// 创建者姓名
    /// </summary>
    public string? CreateUserName { get; }

    /// <summary>
    /// 修改者Id
    /// </summary>
    public long? UpdateUserId { get; }

    /// <summary>
    /// 修改者姓名
    /// </summary>
    public string? UpdateUserName { get; }

    /// <summary>
    /// 软删除
    /// </summary>
    public bool IsDelete { get; }
}

/// <summary>
/// 业务数据实体基类（数据权限）
/// </summary>
public interface IEntityBaseData : IEntityBase, IOrgIdFilter
{
    /// <summary>
    /// 创建者部门Id
    /// </summary>
    public long? CreateOrgId { get; }

    /// <summary>
    /// 创建者部门名称
    /// </summary>
    public string? CreateOrgName { get; }
}

/// <summary>
/// 租户实体基类
/// </summary>
public interface IEntityTenant : IEntityBase, ITenantIdFilter
{
    /// <summary>
    /// 租户Id
    /// </summary>
    public long? TenantId { get; }
}

/// <summary>
/// 租户实体基类Id
/// </summary>
public interface IEntityTenantId : IEntityBaseId, ITenantIdFilter
{
    /// <summary>
    /// 租户Id
    /// </summary>
    public long? TenantId { get; }
}

/// <summary>
/// 租户实体基类 + 业务数据（数据权限）
/// </summary>
public interface IEntityTenantBaseData : IEntityBaseData, ITenantIdFilter
{
    /// <summary>
    /// 租户Id
    /// </summary>
    public long? TenantId { get; }
}