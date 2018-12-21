using AutoMapper;
using CXY.CJS.Core.Extensions;
using System;

namespace CXY.CJS.Core.AutoMapper
{
    /// <summary>
    /// AutoMapper 枚举转为描述
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TSourceMember"></typeparam>
    public class EnumValueResolver<T, TSourceMember> : IMemberValueResolver<object, object, TSourceMember, string> where T : Enum
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="sourceMember"></param>
        /// <param name="member"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string Resolve(object source, object destination, TSourceMember sourceMember, string member, ResolutionContext context)
        {

            var tuple = sourceMember.ToEnum<T>();

            if (!tuple.Item2) return "";

            return tuple.Item1.GetDescription();
        }
    }
}
