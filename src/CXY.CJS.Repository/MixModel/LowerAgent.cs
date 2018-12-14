using System.Collections.Generic;
using System.Linq;
using CXY.CJS.Model;
using Newtonsoft.Json.Linq;

namespace CXY.CJS.Repository.MixModel
{
    public class LowerAgent
    {
        public  User User { get; set; }
        public UserSysSetting UserSysSetting { get; set; }
        public UserScore UserScore { get; set; }
        public UserWallet UserWallet { get; set; }
        public UserMarkupSetting UserMarkupSetting { get; set; }

        public static LowerAgent MapFrom<TSource>(TSource source)
        {
            var temp = JObject.FromObject(source);
            return new LowerAgent
            {
                User = temp.ToObject<User>(),
                UserSysSetting = temp.ToObject<UserSysSetting>(),
                UserScore = temp.ToObject<UserScore>(),
                UserWallet = temp.ToObject<UserWallet>(),
                UserMarkupSetting = temp.ToObject<UserMarkupSetting>(),
            };
        }

        public static IEnumerable<TResult> MapToList<TResult>(IEnumerable<LowerAgent> datas)
        {
            return datas.Select(i =>
            {
                return MapTo<TResult>(i);
            });
        }

        public static TResult MapTo<TResult>(LowerAgent i)
        {
            var temp = JObject.FromObject(i.User);
            if (i.UserSysSetting != null)
            {
                temp.Merge(JObject.FromObject(i.UserSysSetting));
            }
            if (i.UserScore != null)
            {
                temp.Merge(JObject.FromObject(i.UserScore));
            }
            if (i.UserWallet != null)
            {
                temp.Merge(JObject.FromObject(i.UserWallet));
            }
            if (i.UserMarkupSetting != null)
            {
                temp.Merge(JObject.FromObject(i.UserMarkupSetting));
            }
            return temp.ToObject<TResult>();
        }
    }
}