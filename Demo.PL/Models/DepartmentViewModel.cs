using Demo.DAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Demo.PL.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required ! ")]
        [MinLength(2, ErrorMessage = "Minimum Length is 3 Characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department Code is required ! ")]
        public string Code { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

    }
}
