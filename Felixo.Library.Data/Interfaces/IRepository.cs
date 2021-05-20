using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Felixo.Library.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Add(T item);
        Task<T> Update(T item,object Id);
        Task<IEnumerable<T>> Get();
        Task<T> GetById(long id);
        Task Delete(long id);
    }
}
