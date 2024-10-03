using System.Net;

namespace Netflix.Application.Common.Errors
{
    public class DuplicateEmailException: Exception
    {
        public new readonly string Message;
        public readonly int code = (int)HttpStatusCode.Conflict;

        public DuplicateEmailException(string message)
        {
            this.Message = message;
        }
    }
}
