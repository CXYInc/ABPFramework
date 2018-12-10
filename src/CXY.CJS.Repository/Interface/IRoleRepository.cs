using Abp.Domain.Repositories;
using CXY.CJS.Model;

namespace CXY.CJS.Repository
{
    public interface IRoleRepository : ICJSRepositoryBase<Role, string>, IQueryPageRepository<Role, string>
    {
    }
}