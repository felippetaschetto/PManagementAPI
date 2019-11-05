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

namespace PManagement.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string TokenSecret = "PManagement2018PriestAkslv,mfkjhsd837595kjndfglhaserljhb4y20ei455sdf";
        private const string TokenIssuer = "pmanagement.co.uk";
        private readonly IUserRepository UserRepository;
        private readonly IUnitOfWork UnitOfWork;
        private readonly ITokenInfoRepository TokenInfoRepository;

        public AuthenticationService(IUserRepository userRepository, IUnitOfWork unitOfWork, ITokenInfoRepository tokenInfoRepository)
        {
            this.UserRepository = userRepository;
            this.UnitOfWork = unitOfWork;
            this.TokenInfoRepository = tokenInfoRepository;
        }

        private string GenerateToken(UserDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TokenSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = TokenIssuer,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("CompanyId", user.CompanyId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var roles = new List<Claim>();
            
            foreach (var item in user.Roles)
            {
                roles.Add(new Claim(ClaimTypes.Role, item));
            }

            tokenDescriptor.Subject.AddClaims(roles);
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string retToken = tokenHandler.WriteToken(token);

            return retToken;
        }

        public bool IsValidToken(string token)
        {
            if (TokenInfoRepository.List().Any(x => x.Token == token && x.ExpireDate >= DateTime.UtcNow))
            {
                var validationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenSecret)),
                    ValidIssuer = TokenIssuer,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken = null;

                try
                {
                    tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                    return validatedToken != null;
                }
                catch (SecurityTokenException)
                {
                    return false;
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return false;
        }

        private async Task<TokenInfo> SaveTokenInfo(string token)
        {
            string renewToken = Guid.NewGuid().ToString();

            var tokenInfo = new TokenInfo()
            {
                Token = token,
                RenewKey = renewToken,
                ExpireDate = DateTime.UtcNow.AddHours(3)
            };

            await TokenInfoRepository.InsertAsync(tokenInfo);
            return tokenInfo;
        }

        public async Task<LoginResponseDTO> LoginAsync(string username, string password)
        {
            var userDetails = await UserRepository.GetByEmailAsync(username);
            if (userDetails != null)
            {
                string passwordHash = Cryptography.GenerateHash(password, userDetails.Salt);

                // Valid User
                if (passwordHash.Equals(userDetails.Password))
                {
                    string token = GenerateToken(new UserDTO()
                    {
                        UserId = userDetails.Id,
                        CompanyId = userDetails.CompanyId,
                        FirstName = userDetails.FirstName,
                        LastName = userDetails.LastName,
                        Roles = UserRepository.GetUserRoles(userDetails.Id).Select(x => x.Value).ToList()
                    });

                    var savedTokenInfo = await SaveTokenInfo(token);
                    await UnitOfWork.Commit();

                    return new LoginResponseDTO()
                    {
                        FirstName = userDetails.FirstName,
                        LastName = userDetails.LastName,
                        RenewKey = savedTokenInfo.RenewKey,
                        Token = token
                    };
                }
            }

            return null;
        }

        public TokenDetailsDTO GetTokenInfo(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenInfo = tokenHandler.ReadJwtToken(token);

            var tokenDTO = new TokenDetailsDTO()
            {
                UserId = int.Parse(tokenInfo.Claims.First(c => c.Type == "UserId").Value),
                CompanyId = int.Parse(tokenInfo.Claims.First(c => c.Type == "CompanyId").Value),
                Roles = tokenInfo.Claims.Where(c => c.Type == "role").Select(x => x.Value).ToList()
            };

            return tokenDTO;
        }

        public async Task<LoginResponseDTO> RefreshTokenAsync(string token, string salt)
        {
            var tokenEntity = await TokenInfoRepository.GetByToken(token);
            if(tokenEntity != null)
            {
                if (tokenEntity.ExpireDate >= DateTime.UtcNow &&
                    tokenEntity.RenewKey == salt)
                {
                    var tokenInfo = GetTokenInfo(token);
                    var user = UserRepository.Get(tokenInfo.UserId);

                    string renewToken = GenerateToken(new UserDTO()
                    {
                        UserId = user.Id,
                        CompanyId = user.CompanyId,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Roles = UserRepository.GetUserRoles(user.Id).Select(x => x.Value).ToList()
                    });

                    var savedTokenInfo = await SaveTokenInfo(renewToken);
                    TokenInfoRepository.Delete(tokenEntity.Id);
                    await UnitOfWork.Commit();

                    return new LoginResponseDTO()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        RenewKey = savedTokenInfo.RenewKey,
                        Token = renewToken
                    };
                }

                TokenInfoRepository.Delete(tokenEntity.Id);
                await UnitOfWork.Commit();
            }

            return null;
        }        

        public async Task LogoutAsync(string token)
        {
            var entity = await TokenInfoRepository.GetByToken(token);
            if (entity != null)
            {
                TokenInfoRepository.Delete(entity.Id);
                await UnitOfWork.Commit();
            }
        }
    }
}
