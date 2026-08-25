using System.Net;

namespace Netflix.Application.Common.Errors
{
    public class DuplicateIdentifierException: Exception
    {
        public new readonly string Message;
        public readonly int code = (int)HttpStatusCode.Conflict;

        public DuplicateIdentifierException(string message)
        {
            this.Message = message;
        }
    }
}
