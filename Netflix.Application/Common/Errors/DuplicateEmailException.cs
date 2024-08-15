using System.Net;

namespace Netflix.Application.Common.Errors
{
    public class DuplicateEmailException : Exception
    {
        public readonly string Message = "Client with given email already exists";
        public readonly int code = (int)HttpStatusCode.Conflict;
    }
}
