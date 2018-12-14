using System;

namespace CXY.CJS.Core.Config
{
    /// <summary>
    /// 配置模型标志
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ConfigModelAttribute : Attribute
    {
    //    /// <summary>
    //    /// 配置节点的名词，默认为类名称
    //    /// </summary>
    //    public string SectionName { get; }

    //    /// <summary>
    //    /// 是否允许重载
    //    /// </summary>
    //    public bool IsAllowReload { get; }

    //    /// <summary>
    //    /// 配置模型标志
    //    /// </summary>
    //    /// <param name="sectionName">不配置，默认为类名称</param>
    //    /// <param name="isAllowReload">是否允许重载</param>
    //    public ConfigModelAttribute(string sectionName = "", bool isAllowReload = false)
    //    {
    //        SectionName = sectionName;
    //        IsAllowReload = isAllowReload;
    //    }

    }
}