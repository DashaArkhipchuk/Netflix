using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Common.Errors
{
    public class RestrictedAccessException : Exception
    {
        public readonly int code = (int)HttpStatusCode.BadRequest;

        public RestrictedAccessException(string m) : base($"Restricted access to modification of the entity. {m}") { }
    }
}
