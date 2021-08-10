using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DomingoRoofworksApp.Models
{
    public class JobTypeModel
    {
        [Display(Name = "Job type code")]
        [Required(ErrorMessage = "Please select the job type code")]
        public string id { get; set; }

        [Display(Name = "Job rate")]
        [Required(ErrorMessage = "Please enter the rate for this job")]
        public double rate { get; set; }
    }
}