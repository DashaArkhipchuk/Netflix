using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Common.Errors
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string entityName, string property)
            : base($"{property} for this {entityName} already exists")
        {
        }
    }
}
