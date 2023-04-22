using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MVCAppDbContext _context;
        public GenericRepository(MVCAppDbContext context) => _context = context;

        public async Task<int> Add(T Record)
        {
            await _context.Set<T>().AddAsync(Record);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(T Record)
        {
            _context.Set<T>().Remove(Record);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
             => await _context.Set<T>().ToListAsync();


        public async Task<T> GetById(int? id)
             => await _context.Set<T>().FindAsync(id); //.Find first checks Local then Remote if local value=null


       public async Task<int> Update(T Record)
        {
             _context.Set<T>().Update(Record);
            return await _context.SaveChangesAsync();
        }
    }
}
