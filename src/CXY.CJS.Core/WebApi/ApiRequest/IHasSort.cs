using CXY.CJS.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace CXY.CJS.WebApi
{
    public interface IHasSort
    {
        string SortField { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        SortEnum SortOrder { get; set; }
    }
}