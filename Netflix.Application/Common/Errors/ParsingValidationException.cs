using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Common.Errors
{
    public class ParsingValidationException : Exception
    {
        public readonly int code = (int)HttpStatusCode.BadRequest;

        public ParsingValidationException(string str, string m) : base($"Could not parse string \"{str}\". {m}" ) { }
        public ParsingValidationException(string str) : base($"Could not parse string \"{str}\"" ) { }
    }
}
