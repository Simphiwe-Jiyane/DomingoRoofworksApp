using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DomingoRoofworksApp.Models
{
    public class EmployeeModel
    {

        [Display(Name = "Employee ID")]
        [Required(ErrorMessage = "Please enter the employee ID")]
        public string id { get; set; }

        [Display(Name ="First Name")]
        [Required(ErrorMessage ="Please enter the employee's first name")]
        public string firstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter the employee's last name")]
        public string lastname { get; set; }

    }
}