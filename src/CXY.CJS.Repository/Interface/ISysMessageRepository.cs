using System.Threading.Tasks;
using CXY.CJS.Model;

namespace CXY.CJS.Repository
{
    public interface ISysMessageRepository : ICJSRepositoryBase<SysMessage, string>
    {
        /// <summary>
        /// 设置为已读
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task SetRead(string id);
    }
}