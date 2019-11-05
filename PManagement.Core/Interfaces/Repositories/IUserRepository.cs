using PManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PManagement.Core.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        List<Role> GetUserRoles(int userId);
        Task<User> GetByEmailAsync(string email);        
    }
}
