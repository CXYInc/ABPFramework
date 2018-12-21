using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace CXY.CJS.Core.Extensions
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 获取特性 (DisplayAttribute) 的名称；如果未使用该特性，则返回枚举的名称。
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            DisplayAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

            return attrs.Length > 0 ? attrs[0].Name : enumValue.ToString();
        }

        /// <summary>
        /// 获取特性 (DescriptionAttribute) 的说明；如果未使用该特性，则返回枚举的名称。
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDisplayDescription(this Enum enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            DisplayAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

            return attrs.Length > 0 ? attrs[0].Description : enumValue.ToString();
        }

        /// <summary>
        /// 获取特性 (DescriptionAttribute) 的说明；如果未使用该特性，则返回枚举的名称。
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            DescriptionAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            return attrs.Length > 0 ? attrs[0].Description : enumValue.ToString();
        }

        /// <summary>
        /// 直接获取特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static T GetAttributeOfType<T>(this Enum enumValue) where T : Attribute
        {
            Type type = enumValue.GetType();
            MemberInfo[] memInfo = type.GetMember(enumValue.ToString());
            object[] attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        /// <summary>
        /// object to Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns>元组数据 item1 Enum,item2 bool</returns>
        public static Tuple<T, bool> ToEnum<T>(this object obj) where T : Enum
        {
            var result = default(T);
            bool success = Enum.IsDefined(typeof(T), obj);
            if (success)
            {
                result = (T)Enum.ToObject(typeof(T), obj);
            }
            return new Tuple<T, bool>(result, success);
        }

        /// <summary>
        /// 获取指定枚举全部描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<string> GetDescriptions<T>() where T : Enum
        {
            Type type = typeof(T);

            if (!type.IsEnum) return null;

            var result = new List<string>();

            foreach (var enumValue in type.GetEnumValues())
            {
                var attributeName = Enum.GetName(type, enumValue);

                var memInfo = type.GetMember(enumValue.ToString()).FirstOrDefault();
                var attribute = memInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();

                if (attribute != null)
                    attributeName = ((DescriptionAttribute)attribute).Description;

                result.Add(attributeName);
            }

            return result;
        }

        /// <summary>
        /// 根据描述获取枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static T GetEnumByDesc<T>(this string desc) where T : Enum
        {
            Type type = typeof(T);

            if (!type.IsEnum) return default;

            foreach (var enumValue in type.GetEnumValues())
            {
                var attributeName = Enum.GetName(type, enumValue);

                var memInfo = type.GetMember(enumValue.ToString()).FirstOrDefault();
                var attribute = memInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();

                if (attribute != null)
                    attributeName = ((DescriptionAttribute)attribute).Description;

                if (attributeName.Equals(desc))

                    return (T)enumValue;
            }

            return default;
        }
    }
}
