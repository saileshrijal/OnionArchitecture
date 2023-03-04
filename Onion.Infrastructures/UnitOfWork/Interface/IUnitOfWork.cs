using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Infrastructures.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        void Save();
        Task SaveAsync();
        Task CreateAsync<T>(T entity);
        Task CreateRangeAsync<T>(IEnumerable<T> entities) where T : class;
        void Update<T>(T entity);
        void UpdateRange<T>(IEnumerable<T> entities) where T : class;
        void Remove<T>(T entity);
        void RemoveRange<T>(IEnumerable<T> entities) where T : class;
    }
}
