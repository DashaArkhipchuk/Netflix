﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public class CastingCall
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime SubmissionDue { get; set; }
        public DateTime WorkingDateFrom { get; set; }
        public DateTime WorkingDateTo { get; set; }
        public DateTime PostedDate { get; set; }

        public Guid ProjectTypeId { get; set; }
        public ProjectType ProjectType { get; set; } = null!;

        public Guid RoleTypeId { get; set; }
        public RoleType RoleType { get; set; } = null!;

        public int PlayableAgeFrom { get; set; }
        public int PlayableAgeTo { get; set; }
        public string? Payment { get; set; }
        public string? UnionDetails { get; set; }
        public string? RoleDescription { get; set; }
        public string? RateDetails { get; set; }
        public string? WorkRequirements { get; set; }
        public string? WorkInformation { get; set; }
        public string? RequestedMedia { get; set; }
        public string? InstructionsForSubmissionNote { get; set; }
        public string? RequestingSubmissionsFrom { get; set; }
        public bool IsAnyEthnicAppearanceAccepted { get; set; }
        public bool IsAnyGenderAccepted { get; set; }
        public Guid? CreatedByDirectorId { get; set; }

        public CastingDirector? CreatedByDirector { get; set; }


        public ICollection<Location> Locations { get; set; } = new List<Location>();
        public ICollection<Gender> Genders { get; set; } = new List<Gender>();
        public ICollection<EthnicAppearance> EthnicAppearances { get; set; } = new List<EthnicAppearance>();
        public ICollection<Audition> Auditions { get; set; } = new List<Audition>();
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
