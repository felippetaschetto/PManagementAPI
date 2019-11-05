using PManagement.Core.Entities;
using PManagement.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using PManagement.Core.Interfaces.Repositories;
using PManagement.Core.Infrastructure.UnitOfWork;
using PManagement.Utils.Cryptography;
using PManagement.Entity.Authentication;
using System.Linq;
using PManagement.Entity.UserProfile;

namespace PManagement.Core.Services
{
    public class UserService  : IUserService
    {
        private readonly IUserRepository UserRepository;
        private readonly IUnitOfWork UnitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.UserRepository = userRepository;
            this.UnitOfWork = unitOfWork;
        }
        
        public async Task<UserProfileResponse> GetProfileAsync(int userId)
        {
            var userDetails = await UserRepository.GetAsync(userId);
            if (userDetails != null)
            {
                return new UserProfileResponse()
                {
                    Address = userDetails.Address.Street,
                    BirthDate = userDetails.BirthDate,
                    City = userDetails.Address.City,
                    Country = userDetails.Address.Country,
                    Email = userDetails.Email,
                    FirstName = userDetails.FirstName,
                    LastName = userDetails.LastName,
                    Phone = userDetails.Phone,
                    PostCode = userDetails.Address.PostCode,
                    FileName = userDetails.ProfileImg
                };
            }
            
            return null;
        }

        public UserProfileResponse UpdateUserProfileAsync(UserProfile userProfile, int userId)
        //public async Task<UserProfileResponse> UpdateUserProfileAsync(UserProfile userProfile, int userId)
        {
            //var userDetails = UserRepository.GetAsync(userId);
            var userDetails = UserRepository.Get(userId);
            if (userDetails != null)
            {
                userDetails.Address = new ValueObjects.Address(userProfile.Address, userProfile.PostCode, userProfile.City, userProfile.Country);
                userDetails.BirthDate = userProfile.BirthDate;
                userDetails.FirstName = userProfile.FirstName;
                userDetails.LastName = userProfile.LastName;
                userDetails.Phone = userProfile.Phone;
                //UserRepository.Update(userDetails);
                //UnitOfWork.Commit();
                UnitOfWork.CommitSync();
            }

            return null;
        }
        
    }
}
