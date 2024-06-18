using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.Contracts;

/// <summary>
/// 仓储接口
/// </summary>
public interface ISageKingRepository<T>
    where T : class, new()
{
    int Count(Expression<Func<T, bool>> whereExpression);

    bool Delete(Expression<Func<T, bool>> whereExpression);

    bool Delete(T deleteObj);

    bool Delete(List<T> deleteObjs);

    bool DeleteById(dynamic id);

    bool DeleteByIds(dynamic[] ids);

    T GetById(dynamic id);

    List<T> GetList();

    List<T> GetList(Expression<Func<T, bool>> whereExpression);

    T GetSingle(Expression<Func<T, bool>> whereExpression);

    T GetFirst(Expression<Func<T, bool>> whereExpression);

    bool Insert(T insertObj);

    bool InsertOrUpdate(T data);

    bool InsertOrUpdate(List<T> datas);

    bool InsertRange(List<T> insertObjs);

    bool InsertRange(T[] insertObjs);

    int InsertReturnIdentity(T insertObj);

    long InsertReturnBigIdentity(T insertObj);

    long InsertReturnSnowflakeId(T insertObj);

    List<long> InsertReturnSnowflakeId(List<T> insertObjs);

    T InsertReturnEntity(T insertObj);

    bool IsAny(Expression<Func<T, bool>> whereExpression);

    bool Update(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression);

    bool UpdateSetColumnsTrue(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression);

    bool Update(T updateObj);

    bool UpdateRange(List<T> updateObjs);

    bool UpdateRange(T[] updateObjs);

    Task<int> CountAsync(Expression<Func<T, bool>> whereExpression);

    Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression);

    Task<bool> DeleteAsync(T deleteObj);

    Task<bool> DeleteAsync(List<T> deleteObjs);

    Task<bool> DeleteByIdAsync(dynamic id);

    Task<bool> DeleteByIdsAsync(dynamic[] ids);

    Task<T> GetByIdAsync(dynamic id);

    Task<List<T>> GetListAsync();

    Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression);

    Task<T> GetSingleAsync(Expression<Func<T, bool>> whereExpression);

    Task<T> GetFirstAsync(Expression<Func<T, bool>> whereExpression);

    Task<bool> InsertAsync(T insertObj);

    Task<bool> InsertOrUpdateAsync(T data);

    Task<bool> InsertOrUpdateAsync(List<T> datas);

    Task<bool> InsertRangeAsync(List<T> insertObjs);

    Task<bool> InsertRangeAsync(T[] insertObjs);

    Task<int> InsertReturnIdentityAsync(T insertObj);

    Task<long> InsertReturnBigIdentityAsync(T insertObj);

    Task<long> InsertReturnSnowflakeIdAsync(T insertObj);

    Task<List<long>> InsertReturnSnowflakeIdAsync(List<T> insertObjs);

    Task<T> InsertReturnEntityAsync(T insertObj);

    Task<bool> IsAnyAsync(Expression<Func<T, bool>> whereExpression);

    Task<bool> UpdateSetColumnsTrueAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression);

    Task<bool> UpdateAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression);

    Task<bool> UpdateAsync(T updateObj);

    Task<bool> UpdateRangeAsync(List<T> updateObjs);

    Task<bool> UpdateRangeAsync(T[] updateObjs);

    Task<int> CountAsync(Expression<Func<T, bool>> whereExpression, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(T deleteObj, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(List<T> deleteObjs, CancellationToken cancellationToken);

    Task<bool> DeleteByIdAsync(dynamic id, CancellationToken cancellationToken);

    Task<bool> DeleteByIdsAsync(dynamic[] ids, CancellationToken cancellationToken);

    Task<T> GetByIdAsync(dynamic id, CancellationToken cancellationToken);

    Task<List<T>> GetListAsync(CancellationToken cancellationToken);

    Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression, CancellationToken cancellationToken);

    Task<T> GetSingleAsync(Expression<Func<T, bool>> whereExpression, CancellationToken cancellationToken);

    Task<T> GetFirstAsync(Expression<Func<T, bool>> whereExpression, CancellationToken cancellationToken);

    Task<bool> InsertAsync(T insertObj, CancellationToken cancellationToken);

    Task<bool> InsertOrUpdateAsync(T data, CancellationToken cancellationToken);

    Task<bool> InsertOrUpdateAsync(List<T> datas, CancellationToken cancellationToken);

    Task<bool> InsertRangeAsync(List<T> insertObjs, CancellationToken cancellationToken);

    Task<bool> InsertRangeAsync(T[] insertObjs, CancellationToken cancellationToken);

    Task<int> InsertReturnIdentityAsync(T insertObj, CancellationToken cancellationToken);

    Task<long> InsertReturnBigIdentityAsync(T insertObj, CancellationToken cancellationToken);

    Task<long> InsertReturnSnowflakeIdAsync(T insertObj, CancellationToken cancellationToken);

    Task<List<long>> InsertReturnSnowflakeIdAsync(List<T> insertObjs, CancellationToken cancellationToken);

    Task<T> InsertReturnEntityAsync(T insertObj, CancellationToken cancellationToken);

    Task<bool> IsAnyAsync(Expression<Func<T, bool>> whereExpression, CancellationToken cancellationToken);

    Task<bool> UpdateSetColumnsTrueAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression, CancellationToken cancellationToken);

    Task<bool> UpdateAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression, CancellationToken cancellationToken);

    Task<bool> UpdateAsync(T updateObj, CancellationToken cancellationToken);

    Task<bool> UpdateRangeAsync(List<T> updateObjs, CancellationToken cancellationToken);

    Task<bool> UpdateRangeAsync(T[] updateObjs, CancellationToken cancellationToken);
}
