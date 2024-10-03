
using Microsoft.EntityFrameworkCore;
using Netflix.Application.Common.Errors;
using Netflix.Domain;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Infrastructure.Repositories
{
    internal class CastingCallRepository(NetflixProjectContext dbContext) : ICastingCallRepository
    {
        public class PlayableRange
        {
            public int? From { get; set; }  // Nullable to support "Under X" ranges
            public int? To { get; set; }    // Nullable to support "X+" ranges
        }


        public async Task<List<CastingCall>> GetAllAsync(int skip, int take, List<string> locations, List<string> playableAgeRanges, List<string> projectTypes, List<string> roleTypes, Guid? directorId = null)
        {
            // Start with the base query
            var query = dbContext.CastingCalls.Include(c => c.Genders).Include(c => c.Locations).Include(c => c.ProjectType).Include(c => c.RoleType).AsQueryable();

            //Get Casting calls by director id if provided
            if (directorId is not null)
            {
                query = query.Where(x => x.CreatedByDirectorId == directorId);
            }

            // Apply locations filter if provided
            if (locations != null && locations.Count != 0)
            {
                var lowerLocations = locations.Select(g => g.ToLower()).ToList();

                query = query.Where(casting => casting.Locations.Any(l => lowerLocations.Any(location =>
                            (l.LocationName + ", " + l.RegionName).ToLower().Contains(location))));
            }

            // Apply project types filter if provided
            if (projectTypes != null && projectTypes.Count != 0)
            {
                var lowerProjectTypes = projectTypes.Select(g => g.ToLower()).ToList();
                query = query.Where(casting => lowerProjectTypes.Contains(casting.ProjectType.ProjectTypeName));
            }

            // Apply role types filter if provided
            if (roleTypes != null && roleTypes.Count != 0)
            {
                var lowerRoleTypes = roleTypes.Select(g => g.ToLower()).ToList();
                query = query.Where(casting => lowerRoleTypes.Contains(casting.RoleType.RoleTypeName));
            }

            // Apply playable ages filter if provided
            if (playableAgeRanges != null && playableAgeRanges.Count != 0)
            {
                var parsedRanges = ParsePlayableAgeRanges(playableAgeRanges);
                if (parsedRanges != null && parsedRanges.Count > 0)
                {
                    // Initialize the first range query (or an empty query if there are no valid ranges)
                    IQueryable<CastingCall> combinedQuery = null!;

                    foreach (var range in parsedRanges)
                    {
                        // Create a query for each range and apply filtering
                        var rangeQuery = query;

                        if (range.From.HasValue)
                        {
                            rangeQuery = rangeQuery.Where(casting => casting.PlayableAgeFrom >= range.From.Value);
                        }

                        if (range.To.HasValue)
                        {
                            rangeQuery = rangeQuery.Where(casting => casting.PlayableAgeTo <= range.To.Value);
                        }

                        // Union the results to the combined query
                        combinedQuery = combinedQuery == null ? rangeQuery : combinedQuery.Union(rangeQuery);
                    }

                    // Apply Distinct to ensure no duplicate entries
                    query = combinedQuery.Distinct();
                }
            }

            // Apply pagination
            query = query.Skip(skip).Take(take);

            return await query.ToListAsync() ?? new List<CastingCall>();
        }

        public static List<PlayableRange> ParsePlayableAgeRanges(List<string> ageRangeStrings)
        {
            var parsedRanges = new List<PlayableRange>();
            List<string> lowerranges = ageRangeStrings.Select(s => s.ToLower()).ToList();

            foreach (var range in lowerranges)
            {
                if (range.StartsWith("under"))
                {
                    if (!int.TryParse(range.Replace("under", ""), out int ageLimit))
                    {

                        throw new ParsingValidationException(range);
                    }
                    parsedRanges.Add(new PlayableRange { From = null, To = ageLimit - 1 });
                }
                else if (range.EndsWith("+"))
                {
                    if (!int.TryParse(range.Replace("+", ""), out int ageLimit))
                    {

                        throw new ParsingValidationException(range);
                    }
                    parsedRanges.Add(new PlayableRange { From = ageLimit, To = null });
                }
                else if (range.Contains("-"))
                {
                    var parts = range.Split('-');
                    if (parts.Length < 2 || parts.Length > 2)
                    {
                        throw new ParsingValidationException(range);
                    }
                    if (!int.TryParse(parts[0], out int from) || !int.TryParse(parts[1], out int to))
                    {

                        throw new ParsingValidationException(range);
                    }
                    if (from > to)
                    {
                        throw new ParsingValidationException(range, $"Playable range`s \"{range}\" lower bound is greater than the upper bound");
                    }
                    parsedRanges.Add(new PlayableRange { From = from, To = to });
                }
            }

            return parsedRanges;
        }

        public async Task<CastingCall?> GetByIdAsync(Guid? id)
        {
            return await dbContext.CastingCalls.Include(c => c.RoleType).Include(c => c.ProjectType).Include(c => c.Auditions).ThenInclude(a=>a.Location).Include(c => c.EthnicAppearances).Include(c => c.Genders).Include(c => c.Locations).Include(c=>c.Submissions).ThenInclude(s=>s.SubmissionMedias).Include(c=>c.Submissions).SingleOrDefaultAsync(c => c.Id == id);
        }

        public bool ExistsCastingCallById(Guid castingId)
        {
            return dbContext.CastingCalls.SingleOrDefault(c => c.Id == castingId)is not null;
        }

        public void Add(CastingCall castingCall)
        {
            dbContext.CastingCalls.Add(castingCall);
            dbContext.SaveChanges();
        }

        public async Task<bool> Remove(Guid castingCallId)
        {
            var entity = await dbContext.CastingCalls.FindAsync(castingCallId);

            if (entity == null)
            {
                return false;
            }

            dbContext.CastingCalls.Remove(entity);

            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateAsync(CastingCall castingCall)
        {
            dbContext.CastingCalls.Update(castingCall);

            return await dbContext.SaveChangesAsync() > 0;
        }

    }
}
