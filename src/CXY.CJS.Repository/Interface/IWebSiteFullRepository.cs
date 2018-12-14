using System.Linq;
using System.Threading.Tasks;
using CXY.CJS.Repository.MixModel;

namespace CXY.CJS.Repository
{
    public interface IWebSiteFullRepository:IQueryPageRepository<WebSiteFull>
    {
        IQueryable<WebSiteFull> GetAll();

        IQueryable<WebSiteFull> GetAllNoTracking();

        Task<WebSiteFull> GetAsync(string id);

        Task InsertAsync(WebSiteFull i);

        Task<WebSiteFull> UpdateAsync(WebSiteFull i);
    }
}