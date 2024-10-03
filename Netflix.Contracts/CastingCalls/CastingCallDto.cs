using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.CastingCalls
{
    public class CastingCallDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime SubmissionDue { get; set; }

        public string ProjectType { get; set; } = null!;

        public string RoleType { get; set; } = null!;

        public int PlayableAgeFrom { get; set; }
        public int PlayableAgeTo { get; set; }
        public string? Payment { get; set; }
        public string? UnionDetails { get; set; }
        public string? RoleDescription { get; set; }
        public bool IsAnyGenderAccepted { get; set; }


        public ICollection<string> Locations { get; set; } = new List<string>();
        public ICollection<string> Genders { get; set; } = new List<string>();

    }
}
