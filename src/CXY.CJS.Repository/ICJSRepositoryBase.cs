using Abp.Dependency;
using Abp.Domain.Entities;

namespace CXY.CJS.Repository
{
    public interface ICJSRepositoryBase<TEntity, TPrimaryKey>: ITransientDependency where TEntity : class, IEntity<TPrimaryKey>
    {
    }

    public interface ICJSRepositoryBase<TEntity> : ITransientDependency where TEntity : class, IEntity<int>
    {

    }
}