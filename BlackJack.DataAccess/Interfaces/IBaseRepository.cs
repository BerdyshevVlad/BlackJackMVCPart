using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities;

namespace BlackJack.DataAccess.Interfaces
{
    public interface IBaseRepository<T> where T:BaseEntity
    {
        Task Insert(T item);
        Task Delete(int id);
        Task<IEnumerable<T>> Find(Func<T, bool> predicate);    ///////
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Update(T item);
    }
}
