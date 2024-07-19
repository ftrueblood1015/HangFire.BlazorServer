using HangFire.Domain.Entities;
using HangFire.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HangFire.Infrastructure.Repositories
{
    public class RepositoryBase<T, TContext> : IRepositoryBase<T>
        where T : EntityBase
        where TContext : DbContext
    {
        protected DbContext Context { get; }

        public RepositoryBase(DbContext context)
        {
            Context = context;
        }

        public T Add(T entity)
        {
            Context.Add(entity);
            Context.SaveChanges();

            return entity;
        }

        public bool Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            var results = Context.SaveChanges();

            return results > 0;
        }

        public bool DeleteById(int entityId)
        {
            var entity = GetById(entityId);

            Context.Set<T>().Remove(entity!);
            var results = Context.SaveChanges();

            return results > 0;
        }

        public IEnumerable<T> Filter(Func<T, bool> predicate)
        {
            return Context.Set<T>().AsNoTracking().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public T? GetById(int id)
        {
            return Context.Set<T>().SingleOrDefault(x => x.Id == id);
        }

        public T Update(T entity)
        {
            Context.Set<T>().Update(entity);
            Context.SaveChanges();

            return GetById(entity.Id)!;
        }
    }
}
