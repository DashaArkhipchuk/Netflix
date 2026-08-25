using Netflix.Domain.DTOs.Common;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.IRepository
{
    public interface ICastingCallRepository
    {
        bool ExistsCastingCallById(Guid castingId);
        Task<PagedResult<CastingCall>> GetAllAsync(int skip, int take, List<string> locations, List<string> playableAgeRanges, List<string> projectTypes, List<string> roleTypes, Guid? directorId = null);
        Task<PagedResult<CastingCall>> GetCastingCallsByActorIdAsync(Guid actorId, int skip, int take, List<string> locations, List<string> playableAgeRanges, List<string> projectTypes, List<string> roleTypes);
        Task<CastingCall?> GetByIdAsync(Guid? id);

        void Add(CastingCall castingCall);
        Task<bool> Remove(Guid castingCallId);
        Task<bool> UpdateAsync(CastingCall castingCall);
    }
}
