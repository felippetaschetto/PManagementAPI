using PManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PManagement.Core.Interfaces.Repositories
{
    public interface ITokenInfoRepository : IBaseRepository<TokenInfo>
    {
        Task<IEnumerable<TokenInfo>> GetAll();
        Task<TokenInfo> GetByToken(string token);
    }
}
