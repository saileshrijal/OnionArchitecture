using Microsoft.EntityFrameworkCore;
using Onion.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Infrastructures.Repository.Interface
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Student>> GetAll()
        {
            return await _context.Students.Include(x => x.Faculty).ToListAsync();
        }
        public override async Task<Student> GetById(int Id)
        {
            var student =  await _context.Students.Include(x => x.Faculty).FirstOrDefaultAsync(x=>x.Id==Id);
            return student!;
        }
    }
}
