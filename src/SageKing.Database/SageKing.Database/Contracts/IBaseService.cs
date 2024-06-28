using System.Linq.Expressions;

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
    /// 获取集合 🔖
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取集合")]
    public Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> whereFunc);

    /// <summary>
    /// 获取集合
    /// </summary>
    /// <param name="whereFunc"></param>
    /// <param name="orderby"></param>
    /// <param name="orderByType"></param>
    /// <returns></returns>
    [DisplayName("获取集合")]
    public Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> whereFunc, Expression<Func<TEntity, object>> orderby, bool orderByType = true);

    /// <summary>
    /// 分页获取集合 🔖
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取获取集合")]
    public Task<PageBase<TEntity>> GetPage(PageBaseInput input);

    /// <summary>
    /// 分页获取集合 🔖
    /// </summary>
    /// <returns></returns>
    /// <param name="input"></param>
    /// <param name="orderby"></param>
    /// <param name="orderByType">true:asc,false:desc</param>
    [DisplayName("获取获取集合")]
    /// <returns></returns>
    public Task<PageBase<TEntity>> GetPage(PageBaseInput input, Expression<Func<TEntity, object>> orderby, bool orderByType = true);

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
    /// 更新 🔖
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [DisplayName("更新")]
    public Task<int> Update(TEntity entity, Expression<Func<TEntity, object>> updateColumns);

    /// <summary>
    /// 更新 🔖
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [DisplayName("更新")]
    public Task<int> Update(Expression<Func<TEntity, bool>> wherefunc, Expression<Func<TEntity, TEntity>> updateColumns);

    /// <summary>
    /// 删除 🔖
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [DisplayName("删除")]
    public Task<bool> Delete(long id);
}