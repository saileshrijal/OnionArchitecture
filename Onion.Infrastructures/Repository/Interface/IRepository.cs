using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Infrastructures.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllBy(Expression<Func<T, bool>> predicate);
        Task<T> GetBy(Expression<Func<T, bool>> predicate);
        Task<T> GetById(int Id);
    }
}
