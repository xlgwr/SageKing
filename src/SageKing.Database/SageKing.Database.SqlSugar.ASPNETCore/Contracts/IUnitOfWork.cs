namespace SageKing.Database.SqlSugar.AspNetCore.Contracts;

public interface IUnitOfWork
{
    /// <summary>
    /// 开启工作单元处理
    /// </summary>
    /// <param name="context"></param>
    /// <param name="unitOfWork"></param>
   public void BeginTransaction(FilterContext context, UnitOfWorkAttribute unitOfWork);

    /// <summary>
    /// 提交工作单元处理
    /// </summary>
    /// <param name="resultContext"></param>
    /// <param name="unitOfWork"></param>
   public void CommitTransaction(FilterContext resultContext, UnitOfWorkAttribute unitOfWork);

    /// <summary>
    /// 回滚工作单元处理
    /// </summary>
    /// <param name="resultContext"></param>
    /// <param name="unitOfWork"></param>
   public void RollbackTransaction(FilterContext resultContext, UnitOfWorkAttribute unitOfWork);

    /// <summary>
    /// 执行完毕（无论成功失败）
    /// </summary>
    /// <param name="context"></param>
    /// <param name="resultContext"></param>
  public  void OnCompleted(FilterContext context, FilterContext resultContext);
}
