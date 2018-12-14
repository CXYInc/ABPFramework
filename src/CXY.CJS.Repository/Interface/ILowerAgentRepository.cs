using System.Linq;
using System.Threading.Tasks;
using CXY.CJS.Repository.MixModel;

namespace CXY.CJS.Repository
{
    public interface ILowerAgentRepository : IQueryPageRepository<LowerAgent>
    {
        IQueryable<LowerAgent> GetAll();

        IQueryable<LowerAgent> GetAllNoTracking();

        Task<LowerAgent> GetAsync(string id);
    }
}