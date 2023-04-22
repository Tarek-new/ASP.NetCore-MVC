using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MVCAppDbContext _context;
        public EmployeeRepository(MVCAppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Department> GetDepartmentByEmployeeId(int? id)
        { var department=await _context.Employees.Where(e => e.Id == id).Include(e => e.Department)
                .FirstOrDefaultAsync();
            return department.Department;
        
        }
      
       

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentName(string depName)
            => await _context.Employees.Where(E=>E.Department.Name == depName).ToListAsync();

        public async Task<IEnumerable<Employee>> Search(string name)
        
           => await _context.Employees.Where(E=>E.Name.Contains(name)).ToListAsync();
        
    }
    
   
}
