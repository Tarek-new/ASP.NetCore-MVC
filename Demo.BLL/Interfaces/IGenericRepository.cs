using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> GetById(int? id);
        Task<IEnumerable<T>> GetAll();

        Task<int> Add(T Record);
        Task<int> Update(T Record);
        Task<int> Delete(T Record);
    }
}
