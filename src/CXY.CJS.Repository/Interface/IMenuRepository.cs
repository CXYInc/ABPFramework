using Abp.Domain.Repositories;
using CXY.CJS.Model;

namespace CXY.CJS.Repository
{
    public interface IMenuRepository : ICJSRepositoryBase<Menu, string>, IQueryPageRepository<Menu>
    {
    }
}