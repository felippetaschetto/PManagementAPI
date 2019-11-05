using PManagement.Entity.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PManagement.Core.Interfaces.Services
{
    public interface IAuthenticationService
    {
        bool IsValidToken(string token);
        Task<LoginResponseDTO> LoginAsync(string username, string password);
        Task LogoutAsync(string token);
        Task<LoginResponseDTO> RefreshTokenAsync(string token, string salt);
        TokenDetailsDTO GetTokenInfo(string token);
    }
}