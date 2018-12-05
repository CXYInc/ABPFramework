using Abp.Domain.Entities;

namespace CXY.CJS.EntityFrameworkCore
{
    public interface ICJSRepositoryBase<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
    }

    public interface ICJSRepositoryBase<TEntity> where TEntity : class, IEntity<int>
    {

    }
}
