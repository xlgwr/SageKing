﻿namespace SageKing.Database.SqlSugar.Service;

/// <summary>
/// 实体操作基服务
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class BaseService<TEntity> : IBaseService<TEntity>
    where TEntity : class, new()
{
    private readonly SageKingRepository<TEntity> _rep;

    public BaseService(SageKingRepository<TEntity> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [DisplayName("获取详情")]
    public virtual async Task<TEntity> GetDetail(long id)
    {
        return await _rep.GetByIdAsync(id);
    }

    /// <summary>
    /// 获取集合
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取集合")]
    public virtual async Task<List<TEntity>> GetList()
    {
        return await _rep.GetListAsync();
    }

    /// <summary>
    /// 获取集合
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取集合")]
    public virtual async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> wherefunc)
    {
        return await _rep.AsQueryable().Where(wherefunc).ToListAsync();
    }

    /// <summary>
    /// 获取集合
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取集合")]
    public virtual async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> wherefunc, Expression<Func<TEntity, object>> orderByColumns, bool orderByType)
    {
        return await _rep.AsQueryable().Where(wherefunc).OrderBy(orderByColumns, orderByType ? OrderByType.Asc : OrderByType.Desc).ToListAsync();
    }

    /// <summary>
    /// 获取实体分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取实体分页")]
    public virtual async Task<PageBase<TEntity>> GetPage(PageBaseInput input)
    {
        return await _rep.AsQueryable().ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取实体分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取实体分页")]
    public virtual async Task<PageBase<TEntity>> GetPage(PageBaseInput input, Expression<Func<TEntity, object>> orderByColumns, bool orderByType)
    {
        return await _rep.AsQueryable().OrderBy(orderByColumns, orderByType ? OrderByType.Asc : OrderByType.Desc).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [DisplayName("增加")]
    public virtual async Task<bool> Add(TEntity entity)
    {
        return await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [DisplayName("更新")]
    public virtual async Task<int> Update(TEntity entity)
    {
        return await _rep.AsUpdateable(entity).IgnoreColumns(true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [DisplayName("更新")]
    public virtual async Task<int> Update(TEntity entity, Expression<Func<TEntity, object>> updateColumns)
    {
        return await _rep.AsUpdateable(entity).IgnoreColumns(true).UpdateColumns(updateColumns).ExecuteCommandAsync();
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [DisplayName("更新")]
    public virtual async Task<int> Update(Expression<Func<TEntity, bool>> wherefunc, Expression<Func<TEntity, TEntity>> updateColumns)
    {
        return await _rep.AsUpdateable().SetColumns(updateColumns).IgnoreColumns(true).Where(wherefunc).ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [DisplayName("删除")]
    public virtual async Task<bool> Delete(long id)
    {
        return await _rep.DeleteByIdAsync(id);
    }


}