using PManagement.Entity.Authentication;
using PManagement.Entity.UserProfile;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PManagement.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserProfileResponse> GetProfileAsync(int userId);

        UserProfileResponse UpdateUserProfileAsync(UserProfile userProfile, int userId);
        //Task<UserProfileResponse> UpdateUserProfileAsync(UserProfile userProfile, int userId);
    }
}