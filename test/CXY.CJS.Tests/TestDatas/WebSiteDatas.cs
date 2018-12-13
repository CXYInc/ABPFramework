using CXY.CJS.Model;

namespace CXY.CJS.Tests.TestDatas
{
    public class WebSiteDatas
    {
        public static readonly WebSite SuperWebSite = new WebSite
        {
            Id = "999999",
            WebSiteKey = "CS9999",
            WebSiteName = "超级总站",
            WebSiteMater = "999999201110290000000000000001"
        };

        public static readonly WebSite DedeletedWebSite = new WebSite
        {
            Id = "999991",
            WebSiteKey = "CS9991",
            WebSiteName = "废弃站点",
            WebSiteMater = "999999201110290000000000000001",
            IsDeleted=true,
        };

    }
}