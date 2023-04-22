using Demo.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required ")]
        [MaxLength(50)]
        [MinLength(5)]
        public string Name { get; set; }

        public string Address { get; set; }

        public int? Age { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; } = DateTime.Now;
        public string ImageURL { get; set; }
        public IFormFile Image { get; set; }
        public int DepartmentId { get; set; }
        public virtual DepartmentViewModel Department { get; set; }

    }
}
