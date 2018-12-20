using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace CXY.CJS.Web.Core.Filter.Swagger
{
    public class FormFileOperationFilter : IOperationFilter
    {
        private struct ContainerParameterData
        {
            public readonly ParameterDescriptor Parameter;
            public readonly PropertyInfo Property;

            public string FullName => $"{Parameter.Name}.{Property.Name}";
            public string Name => Property.Name;

            public ContainerParameterData(ParameterDescriptor parameter, PropertyInfo property)
            {
                Parameter = parameter;
                Property = property;
            }
        }

        private const string formDataMimeType = "multipart/form-data";
        private static readonly ImmutableArray<string> iFormFilePropertyNames = typeof(IFormFile).GetTypeInfo().DeclaredProperties.Select(p => p.Name).ToImmutableArray();


        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (!context.ApiDescription.RelativePath.Contains("UploadExcel")) return;

            if (operation.Parameters == null) return;

            // if you don't use [Consumes("multipart/form-data")] on your operations, you don't need this
            if (!operation.Consumes.Contains("multipart/form-data")) return;

            var apiParameterDescriptions = context.ApiDescription.ParameterDescriptions.Where(x => x.ModelMetadata.ModelType == typeof(IFormFile)
            || x.ModelMetadata.ElementType == typeof(IFormFile));

            var paramsToRemove = new List<IParameter>();

            foreach (var apiParameterDescription in apiParameterDescriptions)
            {
                var parameterFilter = apiParameterDescription.Name;
                paramsToRemove.AddRange(operation.Parameters.Where(x => x.Name.Equals(parameterFilter, StringComparison.InvariantCultureIgnoreCase)));
            }

            paramsToRemove.ForEach(x => operation.Parameters.Remove(x));

            foreach (var paramToRemove in paramsToRemove)
            {
                var name = paramToRemove.GetType().GetProperties().FirstOrDefault(x => x.Name == "Type").GetValue(paramToRemove)?.ToString();
                if (name == "array")
                {
                    operation.Parameters.Add(new NonBodyParameter
                    {
                        Description = paramToRemove.Description,
                        Name = paramToRemove.Name,
                        In = "formData",
                        Required = paramToRemove.Required,
                        Type = "array",
                        Items = new PartialSchema { Type = "file", Format = "binary" }
                    });
                }
                else
                {
                    operation.Parameters.Add(new NonBodyParameter
                    {
                        Description = paramToRemove.Description,
                        Name = paramToRemove.Name,
                        In = "formData",
                        Required = paramToRemove.Required,
                        Type = "file"
                    });
                }
            }
        }


        //public void Apply(Operation operation, OperationFilterContext context)
        //{
        //    var parameters = operation.Parameters;
        //    if (!context.ApiDescription.RelativePath.Contains("UploadExcel")) return;

        //    if (parameters == null)
        //        return;

        //    var @params = context.ApiDescription.ActionDescriptor.Parameters;

        //    //if (parameters.Count == @params.Count)
        //    //    return;

        //    var formFileParams =
        //        (from parameter in @params
        //         where parameter.ParameterType.IsAssignableFrom(typeof(IFormFile))
        //         select parameter).ToArray();

        //    var iFormFileType = typeof(IFormFile[]).GetTypeInfo();

        //    var containerParams1 =
        //        @params.Select(p => new KeyValuePair<ParameterDescriptor, PropertyInfo[]>(
        //            p, p.ParameterType.GetProperties()));

        //    var containerParams = containerParams1
        //        .Where(pp => pp.Value.Any(p => iFormFileType.IsAssignableFrom(p.ReflectedType)))
        //        .SelectMany(p => p.Value.Select(pp => new ContainerParameterData(p.Key, pp)))
        //        .ToImmutableArray();

        //    if (!(formFileParams.Any() || containerParams.Any()))
        //        return;

        //    var consumes = operation.Consumes;
        //    consumes.Clear();
        //    consumes.Add(formDataMimeType);

        //    if (!containerParams.Any())
        //    {
        //        var nonIFormFileProperties =
        //            parameters.Where(p => !(iFormFilePropertyNames.Contains(p.Name)
        //                && string.Compare(p.In, "query", StringComparison.OrdinalIgnoreCase) == 0))
        //                .ToImmutableArray();

        //        parameters.Clear();
        //        foreach (var parameter in nonIFormFileProperties) parameters.Add(parameter);

        //        foreach (var parameter in formFileParams)
        //        {
        //            parameters.Add(new NonBodyParameter
        //            {
        //                Name = parameter.Name,
        //                // Required = true,
        //                Type = "file"
        //            });
        //        }
        //    }
        //    else
        //    {
        //        var paramsToRemove = new List<IParameter>();
        //        foreach (var parameter in containerParams)
        //        {
        //            var parameterFilter = parameter.Property.Name + ".";
        //            paramsToRemove.AddRange(from p in parameters
        //                                    where p.Name.StartsWith(parameterFilter)
        //                                    select p);
        //        }
        //        paramsToRemove.ForEach(x => parameters.Remove(x));

        //        foreach (var parameter in containerParams)
        //        {
        //            if (iFormFileType.IsAssignableFrom(parameter.Property.ReflectedType))
        //            {
        //                var originalParameter = parameters.FirstOrDefault(param => param.Name == parameter.Name);
        //                parameters.Remove(originalParameter);

        //                parameters.Add(new NonBodyParameter
        //                {
        //                    Name = parameter.Name,
        //                    //Required = (bool)originalParameter?.Required,
        //                    Type = "file",
        //                    In = "formData"
        //                });
        //            }
        //        }
        //    }
        //}
    }
}