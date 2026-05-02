using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Common.Errors
{
    public class AuthenticationException: Exception
    {
        public readonly int code = (int)HttpStatusCode.Unauthorized;

        public AuthenticationException(string message) : base(message) { }
    }
}
