﻿using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Animes.Common
{
    public class ContentWithTypeAnime
    {
        public string Type { get; set; } = null!;

        public virtual Film? Film { get; set; }
        public virtual Domain.Series? Series { get; set; }
    }
}
