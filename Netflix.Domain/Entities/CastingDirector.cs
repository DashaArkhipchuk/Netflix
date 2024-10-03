using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public class CastingDirector
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string FullName { get; set; } = null!;
        public Guid TypeId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? Website { get; set; }
        public string Address { get; set; } = null!;
        public string RegionName { get; set; } = null!;
        public string PhoneNumberWithCountryCode { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Client Client { get; set; } = null!;
        public CastingDirectorType CastingDirectorType { get; set; } = null!;

        public ICollection<CastingCall> CastingCallsCreated { get; set; } = new List<CastingCall>();


    }
}
