using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    [Index(nameof(Code),IsUnique =true)]
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Department name is required ! ")]
        [MinLength(2,ErrorMessage = "Minimum Length is 3 Characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department Code is required ! ")]
     
        public string Code { get; set; }

        public DateTime DateOfCreation { get; set; }

        public virtual ICollection  <Employee> Employees { get; set; }=new List<Employee>();

    }
}
