using System.Text.Json.Serialization;

namespace Pms.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DepartmentType
    {
        None = 0,
        Development,
        HumanResources,
        Engagement,
        Finance,
        Sales,
        Creative,
        Marketing,
        Management,
        TechnicalSupport,
    }
}
