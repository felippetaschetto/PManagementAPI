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
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
