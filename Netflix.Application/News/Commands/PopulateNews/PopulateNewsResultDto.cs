using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.News.Commands.PopulateNews
{
    public record PopulateNewsResultDto
    (
        int Fetched, 
        int Inserted, 
        int SkippedDuplicates, 
        int SkippedInvalid
    );
}
