using Microsoft.EntityFrameworkCore;
using Netflix.Application.Submissions.Queries.GetAllSubmissionsByCastingCall;
using Netflix.Domain;
using Netflix.Domain.DTOs;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Netflix.Infrastructure.Repositories
{
    public class SubmissionRepository(NetflixProjectContext dbContext) : ISubmissionRepository
    {
        public void Add(Submission sub)
        {
            dbContext.Submissions.Add(sub);
            dbContext.SaveChanges();
        }

        public async Task<Submission?> GetSubmissionByIdAsync(Guid id)
        {
            return await dbContext.Submissions.Include(x => x.Actor).Include(x => x.CastingCall).ThenInclude(c=>c.Locations).Include(c=>c.CastingCall).ThenInclude(c=>c.Genders).Include(c => c.CastingCall).ThenInclude(c => c.ProjectType).Include(c => c.CastingCall).ThenInclude(c => c.RoleType).Include(x => x.SubmissionMedias).SingleOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<PagedResult<Submission>> GetAllSubmissionsByCastingCallAsync(Guid castingCallId, int skip = 0, int take = 10)
        {
            var query = dbContext.Submissions
                        .Include(x => x.Actor)
                        .Include(x => x.CastingCall)
                            .ThenInclude(c => c.Locations)
                        .Include(c => c.CastingCall)
                            .ThenInclude(c => c.Genders)
                        .Include(c => c.CastingCall)
                            .ThenInclude(c => c.ProjectType)
                        .Include(c => c.CastingCall)
                            .ThenInclude(c => c.RoleType)
                        .Include(x => x.SubmissionMedias)
                        .Where(x => x.CastingId == castingCallId)
                        .AsQueryable();

            var totalItems = query.Count();

            // Apply pagination
            query = query.Skip(skip).Take(take);

            var items = await query.ToListAsync() ?? new List<Submission>();

            return new PagedResult<Submission> { TotalCount = totalItems, Items = items }; ;
        }

        public async Task<bool> Remove(Guid submissionId)
        {
            var entity = await dbContext.Submissions.FindAsync(submissionId);

            if (entity == null)
            {
                return false;
            }

            dbContext.Submissions.Remove(entity);

            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<List<Submission>> GetSubmissionsByActorIdAsync(Guid actorId, int skip = 0, int take = 10)
        {
            return await dbContext.Submissions.Include(x => x.Actor).Include(x => x.CastingCall).ThenInclude(c => c.Locations).Include(c => c.CastingCall).ThenInclude(c => c.Genders).Include(c => c.CastingCall).ThenInclude(c => c.ProjectType).Include(c => c.CastingCall).ThenInclude(c => c.RoleType).Include(x => x.SubmissionMedias).Where(x => x.ActorId == actorId).Skip(skip).Take(take).ToListAsync();
        }
    }
}
