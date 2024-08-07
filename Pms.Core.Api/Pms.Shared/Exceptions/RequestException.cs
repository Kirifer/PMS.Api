using Pms.Shared.Enums;

namespace Pms.Shared.Exceptions
{
    public class RequestException : Exception
    {
        public ErrorCode Code { get; private set; }
        public List<string> Messages { get; } = new List<string>();

        public RequestException(params string[] errors)
        {
            Code = ErrorCode.RequestFailed;
            Messages = new List<string>(errors);
        }

        public RequestException(ErrorCode code, params string[] errors)
        {
            Code = code;
            Messages = new List<string>(errors);
        }
    }
}
