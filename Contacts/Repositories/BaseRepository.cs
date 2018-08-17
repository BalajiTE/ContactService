using EvolentContacts.Models;
using EvolentContacts.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EvolentContacts.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
      where T : EntityBase
    {
        private EvolentContactsContext context;
        protected readonly IDbSet<T> _dbset;

        public BaseRepository(EvolentContactsContext context)
        {
            this.context = context;
            this._dbset = context.Set<T>();
        }

        public virtual IEnumerable<T> All()
        {
            return _dbset.AsEnumerable<T>();
        }

        public IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = _dbset.Where(predicate).AsEnumerable();
            return query;
        }

        public virtual T Create(T entity)
        {
            return context.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual T Delete(T entity)
        {
            return context.Set<T>().Remove(entity);
        }

        public async Task<int?> Save()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}