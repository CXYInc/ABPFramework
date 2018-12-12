using System.Linq;
using System.Threading.Tasks;
using CXY.CJS.Repository.MixModel;

namespace CXY.CJS.Repository
{
    public interface IWebSiteFullRepository:IQueryPageRepository<WebSiteFull>
    {
        IQueryable<WebSiteFull> GetAll();

        Task<WebSiteFull> GetAsync(string id);

        Task InsertAsync(WebSiteFull i);
    }
}