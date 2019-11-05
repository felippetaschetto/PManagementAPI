using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PManagement.API.Controllers.Base;
using PManagement.Core.Infrastructure.UnitOfWork;
using PManagement.Core.Interfaces.Repositories;
using PManagement.Core.Interfaces.Services;
using PManagement.Entity.UserProfile;

namespace PManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserRepository UserRepository;
        private readonly IUserService UserService;
        private readonly IUnitOfWork UnitOfWork;
        private readonly IAuthenticationService AuthenticationService;

        public UserProfileController(IUnitOfWork unitOfWork, IAuthenticationService authenticationService, IUserService userService)
            //:base(authenticationService)
        {
            //this.UserRepository = userRepository;
            this.UnitOfWork = unitOfWork;
            this.AuthenticationService = authenticationService;
            this.UserService = userService;
        }

        // GET: api/UserProfile
        [HttpGet("GetUserProfile")]
        public async Task<ActionResult<UserProfileResponse>> Get()
        {
            string token = Request.Headers["Authorization"].ToString();
            var tokenInfo = AuthenticationService.GetTokenInfo(token);
            //var tokenInfo = GetTokenInfo();
            if(tokenInfo != null)
                return await this.UserService.GetProfileAsync(tokenInfo.UserId);

            return null;
        }
        
        // POST: api/UserProfile
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/UserProfile/5
        [HttpPut]
        //public async void Put([FromBody] UserProfile userProfile)
        public void Put([FromBody] UserProfile userProfile)
        {
            string token = Request.Headers["Authorization"].ToString();
            var tokenInfo = AuthenticationService.GetTokenInfo(token);
            //var tokenInfo = GetTokenInfo();
            if (tokenInfo != null)
            {
                var ret =  this.UserService.UpdateUserProfileAsync(userProfile, tokenInfo.UserId);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
