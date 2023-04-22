using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
         private readonly MVCAppDbContext _context;
        public DepartmentRepository(MVCAppDbContext context) : base(context)
        {
            _context=context;
        }


        //    private readonly MVCAppDbContext _context;

        //    public DepartmentRepository(MVCAppDbContext context)
        //    {
        //        _context = context;
        //    }
        //    public int Add(Department department)
        //    {
        //        _context.Add(department);
        //        return _context.SaveChanges();
        //    }

        //    public int Delete(Department department)
        //    {
        //        _context.Remove(department);
        //        return _context.SaveChanges();
        //    }

        //    public IEnumerable<Department> GetAll()

        //      =>   _context.Departments.ToList();


        //    public Department GetById(int? id)

        //      =>   _context.Departments.Where(D => D.Id == id).FirstOrDefault();


        //    public int Update(Department department)
        //    {
        //        _context.Update(department);
        //        return _context.SaveChanges();
        //    }
        //}

    }
}