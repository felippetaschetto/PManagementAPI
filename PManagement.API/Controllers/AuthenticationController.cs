using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PManagement.Core.Infrastructure.UnitOfWork;
using PManagement.Core.Interfaces.Repositories;
using PManagement.Core.Interfaces.Services;
using PManagement.Entity.Authentication;
using PManagement.Utils.Cryptography;

namespace PManagement.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly ITokenInfoRepository TokenRepository;
        private readonly IUserRepository UserRepository;
        private readonly IUnitOfWork UnitOfWork;
        private readonly ICompanyRepository CompanyRepository;
        private readonly IAuthenticationService AuthenticationService;

        public AuthenticationController(ITokenInfoRepository tokenRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, ICompanyRepository companyRepository, IAuthenticationService authenticationService)
        {
            this.TokenRepository = tokenRepository;
            this.UserRepository = userRepository;
            this.UnitOfWork = unitOfWork;
            this.CompanyRepository = companyRepository;
            this.AuthenticationService = authenticationService;
        }


        // POST: api/Authentication
        [HttpPost("Login")]        
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginDTO login)
        {
            var loginReponse = await this.AuthenticationService.LoginAsync(login.UserName, login.Password);
            if (loginReponse != null)
                return loginReponse;

            ModelState.AddModelError("error", "Invalid Username or Password");
            return BadRequest(ModelState);
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<LoginResponseDTO>> RefreshToken([FromBody] RenewDTO renewDTO)
        {
            var refreshTokenResponse = await this.AuthenticationService.RefreshTokenAsync(renewDTO.Token, renewDTO.Key);
            if (refreshTokenResponse != null)
                return refreshTokenResponse;

            return Unauthorized("not authorized");
        }
    }
}