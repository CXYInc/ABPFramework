using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CXY.CJS.Model;

namespace CXY.CJS.Repository
{
    public interface IRoleMenuRepository : ICJSRepositoryBase<RoleMenu, string>, IQueryPageRepository<RoleMenu>
    {
        
    }
}
