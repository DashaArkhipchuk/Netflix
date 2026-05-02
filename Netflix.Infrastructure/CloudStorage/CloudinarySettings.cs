using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Infrastructure.CloudStorage
{
    internal class CloudinarySettings
    {
        public const string SectionName = "CloudinarySettings";
        public string CloudName { get; init; } = null!;
        public string APIKey { get; init; } = null!;
        public string APISecret { get; init; } = null!;
        public int Timeout { get; init; }
        public int ChunkSize { get; init; }
    }
}
