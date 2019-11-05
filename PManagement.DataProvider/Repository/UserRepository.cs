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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork context) : base(context)
        {
        }

        public List<Role> GetUserRoles(int userId)
        {
            var roles = this.DbQuery().Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefault(x => x.Id == userId).UserRoles.Select(x => x.Role).ToList();
            return roles;
        }

        public async Task<User> GetByEmailAsync(string email)
        {            
            return await this.DbQuery().FirstOrDefaultAsync(x => x.Email == email);
        }

    }
}
