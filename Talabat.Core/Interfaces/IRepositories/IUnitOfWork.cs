using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIs.Interfaces.IRepositories;

public interface IUnitOfWork : IDisposable 
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;


    Task<int> Complete();
}