using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CXY.CJS.Web.Core.Extensions
{
    /// <summary>
    /// Add enum value descriptions to Swagger
    /// </summary>
    public class AddSwaggerEnumDescriptions : IDocumentFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {

            //SwaggerDocsConfig.DefaultRootUrlResolver
            // add enum descriptions to result models
            foreach (var schemaDictionaryItem in swaggerDoc.Definitions)
            {
                var schema = schemaDictionaryItem.Value;
                foreach (var propertyDictionaryItem in schema.Properties)
                {
                    var property = propertyDictionaryItem.Value;
                    var propertyEnums = property.Enum;
                    if (propertyEnums != null && propertyEnums.Count > 0)
                    {
                        property.Description += DescribeEnum(propertyEnums);
                    }
                }
            }

            if (swaggerDoc.Paths.Count <= 0) return;

            // add enum descriptions to input parameters
            foreach (var pathItem in swaggerDoc.Paths.Values)
            {
                DescribeEnumParameters(pathItem.Parameters);

                // head, patch, options, delete left out
                var possibleParameterisedOperations = new List<Operation> { pathItem.Get, pathItem.Post, pathItem.Put };
                possibleParameterisedOperations.FindAll(x => x != null).ForEach(x => DescribeEnumParameters(x.Parameters));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        private static void DescribeEnumParameters(IList<IParameter> parameters)
        {
            if (parameters == null) return;

            foreach (var param in parameters)
            {
                if (param.Extensions.ContainsKey("enum") && param.Extensions["enum"] is IList<object> paramEnums &&
                    paramEnums.Count > 0)
                {
                    param.Description += DescribeEnum(paramEnums);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enums"></param>
        /// <returns></returns>
        private static string DescribeEnum(IEnumerable<object> enums)
        {
            var enumDescriptions = new List<string>();
            Type type = null;
            foreach (var enumOption in enums)
            {
                if (type == null) type = enumOption.GetType();

                var attributeName = Enum.GetName(type, enumOption);

                var memInfo = type.GetMember(enumOption.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length != 0)
                    attributeName = ((DescriptionAttribute)attributes[0]).Description;

                enumDescriptions.Add($"{Convert.ChangeType(enumOption, type.GetEnumUnderlyingType())} = {attributeName}");
            }

            return $"{Environment.NewLine}{string.Join(Environment.NewLine, enumDescriptions)}";
        }
    }
}
