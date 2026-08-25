using Microsoft.EntityFrameworkCore;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Netflix.Infrastructure.Repositories
{
    internal class SocialMediaProfileRepository : ISocialMediaProfileRepository
    {
        private readonly NetflixProjectContext _dbContext;

        public SocialMediaProfileRepository(NetflixProjectContext dbContext) => _dbContext = dbContext;

        public async Task<SocialMediaProfile?> GetByClientIdAsync(Guid clientId)
        {
            return await _dbContext.SocialMediaProfiles.FirstOrDefaultAsync(p => p.ClientId == clientId);
        }

        public async Task<SocialMediaProfile?> GetByIdAsync(Guid profileId)
        {
            return await _dbContext.SocialMediaProfiles.FirstOrDefaultAsync(p => p.Id == profileId);
        }

        public async Task<bool> NicknameExistsAsync(string nickname, Guid? excludeProfileId)
        {
            return await _dbContext.SocialMediaProfiles.AnyAsync(p => p.Nickname == nickname && p.Id != excludeProfileId);
        }

        public void Add(SocialMediaProfile socialMediaProfile)
        {
            _dbContext.SocialMediaProfiles.Add(socialMediaProfile);
        }

        public void Update(SocialMediaProfile socialMediaProfile)
        {
            _dbContext.SocialMediaProfiles.Update(socialMediaProfile);
        }
    }
}
