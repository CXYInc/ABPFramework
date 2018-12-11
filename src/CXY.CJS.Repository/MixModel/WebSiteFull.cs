using System.Collections.Generic;
using System.Linq;
using CXY.CJS.Model;
using Newtonsoft.Json.Linq;

namespace CXY.CJS.Repository.MixModel
{
    public class WebSiteFull
    {
        public WebSite WebSite { get; set; }
        public WebSiteConfig WebSiteConfig { get; set; }
        public WebSitePayConfig WebSitePayConfig { get; set; }



        public static IEnumerable<TResult> MapToList<TResult>(IEnumerable<WebSiteFull> datas)
        {
            return datas.Select(i =>
            {
                return MapTo<TResult>(i);
            });
        }

        public static TResult MapTo<TResult>(WebSiteFull i)
        {
            var temp = JObject.FromObject(i.WebSite);
            if (i.WebSiteConfig != null)
            {
                temp.Merge(JObject.FromObject(i.WebSiteConfig));
            }
            if (i.WebSitePayConfig != null)
            {
                temp.Merge(JObject.FromObject(i.WebSitePayConfig));
            }
            return temp.ToObject<TResult>();
        }
    }
}