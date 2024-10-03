using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Actor.Common
{
    public record ActorProfileResult
    (
        Domain.Actor actor
    );
}
