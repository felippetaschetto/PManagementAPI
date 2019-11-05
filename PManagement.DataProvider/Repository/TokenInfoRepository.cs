using PManagement.Core.Entities;
using PManagement.Core.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using PManagement.Core.Interfaces.Repositories;

namespace PManagement.DataProvider.Repository
{    
    public class TokenInfoRepository : BaseRepository<TokenInfo>, ITokenInfoRepository
    {
        public TokenInfoRepository(IUnitOfWork context) : base(context)
        {
        }

        public async Task<IEnumerable<TokenInfo>> GetAll()
        {
            var result = await this.Context.TokensInfo.ToListAsync();

            return result;
        }

        public async Task<TokenInfo> GetByToken(string token)
        {
            var result = await this.Context.TokensInfo.FirstOrDefaultAsync(x => x.Token == token);
            return result;
        }
    }
}
