using Pms.Shared.Enums;

namespace Pms.Shared.Exceptions
{
    public class UnprocessableException : Exception
    {
        public ErrorCode Code { get; private set; }
        public List<string> Messages { get; } = new List<string>();

        public UnprocessableException(params string[] errors)
        {
            Code = ErrorCode.UnprocessableField;
            Messages = new List<string>(errors);
        }

        public UnprocessableException(ErrorCode code, params string[] errors)
        {
            Code = code;
            Messages = new List<string>(errors);
        }
    }
}
