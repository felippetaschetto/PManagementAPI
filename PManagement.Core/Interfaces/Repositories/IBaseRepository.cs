using PManagement.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PManagement.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class, IBaseEntity
    {
        T Get(int id, bool asNoTracking = false);

        Task<T> GetAsync(int id, bool asNoTracking = false);

        IQueryable<T> List(bool asNoTracking = false);

        void Insert(T entity);

        Task InsertAsync(T entity);

        void Update(T entity);

        void Delete(int id);
    }
}
