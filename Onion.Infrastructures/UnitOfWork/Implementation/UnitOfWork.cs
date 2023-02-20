using Onion.Infrastructures.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Infrastructures.UnitOfWork.Interface
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IFacultyRepository Faculty { get; private set; }
        public IStudentRepository Student { get; private set; }

        public UnitOfWork(ApplicationDbContext context) 
        {
            _context = context;
            Faculty = new FacultyRepository(context);
            Student = new StudentRepository(context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();   
        }
    }
}
