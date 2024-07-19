using HangFire.Domain.Entities;
using HangFire.Domain.ViewModels;

namespace HangFire.Domain.Interfaces.Services
{
    public interface IServiceBase<TEntity>
        where TEntity : EntityBase
    {
        TEntity Add(TEntity entity);

        bool Delete(TEntity entity);

        bool DeleteById(int entityId);

        IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate);

        IEnumerable<TEntity> GetAll();

        TEntity? GetById(int entityId);

        TEntity Update(TEntity entity);
    }
}
