namespace SageKing.Database.Contracts;

/// <summary>
/// 实体操作基服务接口
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IBaseService<TEntity> where TEntity : class, new()
{

    /// <summary>
    /// 获取详情 🔖
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [DisplayName("获取详情")]
    public Task<TEntity> GetDetail(long id);

    /// <summary>
    /// 获取集合 🔖
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取集合")]
    public Task<List<TEntity>> GetList();

    /// <summary>
    /// 增加 🔖
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [DisplayName("增加")]
    public Task<bool> Add(TEntity entity);

    /// <summary>
    /// 更新 🔖
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [DisplayName("更新")]
    public Task<int> Update(TEntity entity);

    /// <summary>
    /// 删除 🔖
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [DisplayName("删除")]
    public Task<bool> Delete(long id);
}