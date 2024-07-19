using HangFire.Domain.Entities;
using HangFire.Domain.Interfaces.Repositories;
using HangFire.Domain.Interfaces.Services;
using HangFire.Domain.ViewModels;
using AutoMapper;

namespace HangFire.Services.Services
{
    public class ServiceBase<TEntity, TRepo> : IServiceBase<TEntity>
        where TEntity : EntityBase
        where TRepo : IRepositoryBase<TEntity>
    {
        protected IRepositoryBase<TEntity> Repo;
        private readonly IMapper _mapper;

        public ServiceBase(IRepositoryBase<TEntity> repo, IMapper mapper)
        {
            Repo = repo;
            _mapper = mapper;
        }

        public TEntity Add(TEntity entity)
        {
            try
            {
                return Repo.Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                return Repo.Delete(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteById(int entityId)
        {
            try
            {
                var entity = GetById(entityId);

                if (entity == null)
                {
                    return false;
                }

                return Delete(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate)
        {
            try
            {
                return Repo.Filter(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return Repo.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TEntity? GetById(int viewModelId)
        {
            try
            {
                return Repo.GetById(viewModelId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TEntity Update(TEntity entity)
        {
            try
            {
                return Repo.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
