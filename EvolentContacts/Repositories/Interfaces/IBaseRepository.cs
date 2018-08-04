using EvolentContacts.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EvolentContacts.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : EntityBase
    {
        IEnumerable<T> All();

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        T Create(T entity);

        void Update(T entity);

        T Delete(T entity);

        Task<int?> Save();
    }
}
