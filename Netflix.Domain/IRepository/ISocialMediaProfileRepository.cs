using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.IRepository
{
    public interface ISocialMediaProfileRepository
    {
        Task<SocialMediaProfile?> GetByClientIdAsync(Guid clientId);
        Task<SocialMediaProfile?> GetByIdAsync(Guid id);
        Task<bool> NicknameExistsAsync(string nickname, Guid? excludeProfileId);
        void Add(SocialMediaProfile socialMediaProfile);
        void Update(SocialMediaProfile socialMediaProfile);
    }
}
