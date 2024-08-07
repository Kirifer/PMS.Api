using System.Text.Json.Serialization;

namespace Pms.Shared.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ErrorCode
    {
        GenericError = -99,
        RequestFailed = -100,
        UnprocessableField = -101,
        RequestForbidden = -102,

        DuplicateRecord = -200,
        NoRecordFound = -300,

        ValidationError = -400,
        DatabaseError = -500,
    }
}
