using Abp.Dependency;
using Abp.Domain.Entities;
using System.Linq;

namespace CXY.CJS.Repository
{
    public interface ICJSRepositoryBase<TEntity, TPrimaryKey>: ITransientDependency where TEntity : class, IEntity<TPrimaryKey>
    {
        IQueryable<TEntity> GetAll();
    }

    public interface ICJSRepositoryBase<TEntity> : ITransientDependency where TEntity : class, IEntity<int>
    {
        IQueryable<TEntity> GetAll();
    }
}