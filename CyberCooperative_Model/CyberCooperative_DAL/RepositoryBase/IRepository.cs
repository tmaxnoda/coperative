using System.Linq;

namespace CyberCooperative_DAL.RepositoryBase
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void commit();
        void delete(TEntity entity);
        void delete(object id);
        IQueryable<TEntity> getAll();
        IQueryable<TEntity> getAll(object myObject);
        TEntity getById(object id);
        TEntity getFullObjects(object id);
        void insert(TEntity entity);
        void update(TEntity entity);
    }
}