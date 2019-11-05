using Microsoft.EntityFrameworkCore;
using PManagement.Core.Infrastructure.UnitOfWork;
using PManagement.Core.Interfaces.Entities;
using PManagement.Core.Interfaces.Repositories;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

namespace PManagement.DataProvider.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
        where T : class, IBaseEntity

    {
        public BaseRepository(IUnitOfWork context)
        {
            //Check.Argument.IsNotNull(context, "context");
            this.Context = (PManagementContext)context;
        }

        protected PManagementContext Context;

        protected DbSet<T> DbSet
        {
            get
            {
                return this.Context.Set<T>();
            }
        }

        protected IQueryable<T> DbQuery(bool asNoTracking = false)
        {
            return (!asNoTracking) ? this.DbSet : this.DbSet.AsNoTracking();
        }

        public virtual T Get(int id, bool asNoTracking = false)
        {
            return this.DbQuery(asNoTracking).SingleOrDefault(e => e.Id.Equals(id));
        }

        public virtual async Task<T> GetAsync(int id, bool asNoTracking = false)
        {            
            return await this.DbQuery().FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public virtual IQueryable<T> List(bool asNoTracking = false)
        {
            return this.DbQuery(asNoTracking);
        }

        public virtual void InsertOrUpdate(T entity)
        {
            if (entity.Id == 0)
                this.Insert(entity);
            else
                this.Update(entity);
        }

        public virtual void Insert(T entity)
        {
            //Check.Argument.IsNotNull(entity, "entity");

            entity.InsertStamp = DateTime.Now;
            this.DbSet.Add(entity);
        }

        public virtual async Task InsertAsync(T entity)
        {
            entity.InsertStamp = DateTime.Now;
            await this.DbSet.AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            //Check.Argument.IsNotNull(entity, "entity");
            entity.UpdateStamp = DateTime.Now;

            var entry = Context.Entry(entity);
            this.DbSet.Attach(entity);
            entry.State = EntityState.Modified;
        }

        public virtual void Delete(int id)
        {
            //Check.Argument.IsNotNull(entity, "entity");
            T entity = Get(id);
            if(entity != null)
                this.DbSet.Remove(entity);
        }
    }
}
