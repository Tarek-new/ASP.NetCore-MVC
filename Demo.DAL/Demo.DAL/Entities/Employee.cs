using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    public class Employee
    {
     
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required ")]
        [MaxLength(50)]
        [MinLength(5)]
        public string Name { get; set; }

        public string Address { get; set; }

        public int? Age  { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public decimal Salary    { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }=DateTime.Now;

        public string ImageURL { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }


    }
}
