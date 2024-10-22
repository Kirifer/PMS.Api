using System.Text.Json.Serialization;

namespace Pms.Shared.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DbErrorCode
    {
        ValidationFailed = -100,
        NotImplemented = -200,
        NotFound = -300
    }
}
