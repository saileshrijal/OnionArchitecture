using Onion.Infrastructures.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Infrastructures.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        IFacultyRepository Faculty { get; }
        IStudentRepository Student { get; }
        Task<int> SaveAsync();
    }
}
