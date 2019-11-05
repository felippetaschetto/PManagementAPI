using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PManagement.Core.Interfaces.Services;
using PManagement.Entity.Authentication;

namespace PManagement.API.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        private readonly IAuthenticationService AuthenticationService;

        public BaseController(IAuthenticationService authenticationService)
        {
            this.AuthenticationService = authenticationService;
        }

        public TokenDetailsDTO GetTokenInfo()
        {
            string token = Request.Headers["Authorization"].ToString();
            var tokenInfo = AuthenticationService.GetTokenInfo(token);
            return tokenInfo;
        }

    }
}