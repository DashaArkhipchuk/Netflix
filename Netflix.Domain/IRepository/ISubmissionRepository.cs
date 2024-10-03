using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.IRepository
{
    public interface ISubmissionRepository
    {
        void Add(Submission sub);
        Task<Submission?> GetSubmissionByIdAsync(Guid id);

        Task<List<Submission>> GetAllSubmissionsByCastingCallAsync(Guid castingCallId, int skip = 0, int take = 10);
        Task<bool> Remove(Guid submissionId);
        Task<List<Submission>> GetSubmissionsByActorIdAsync(Guid actorId, int skip = 0, int take = 10);
    }
}
