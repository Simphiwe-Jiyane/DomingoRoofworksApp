using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DomingoRoofworksApp.Models
{
    public class JobCardModel
    {

        [Display(Name = "Job Card Number")]
        public string id { get; set; }

        [Display(Name = "Customer ID")]
        [Required(ErrorMessage = "Please enter the customer ID")]
        public string cust_id { get; set; }

        [Display(Name ="Customer Firstname")]
        [Required(ErrorMessage = "Please enter the customer firstname")]
        public string cust_name { get; set; }

        [Display(Name = "Customer Lastname")]
        [Required(ErrorMessage = "Please enter the customer Surname")]
        public string cust_lastname { get; set; }

        [Display(Name = "Customer address line 1")]
        [Required(ErrorMessage = "Please enter the first address line")]
        public string addressline1 { get; set; }

        [Display(Name = "Customer address line 2")]
        [Required(ErrorMessage = "Please enter the second address line")]
        public string addressline2 { get; set; }

        [Display(Name = "Customer city")]
        [Required(ErrorMessage = "Please specify the city")]
        public string city { get; set; }

        [Display(Name = "Customer address")]
        [Required(ErrorMessage = "Please enter the postal code")]
        public string postal { get; set; }

        [Display(Name = "Job type")]
        [Required(ErrorMessage = "Please select the job type")]
        public string type { get; set; }

        [Display(Name = "number of days")]
        [Required(ErrorMessage = "Please enter the number of days it will take to complete the project")]
        public int num_days { get; set; }

        [Display(Name = "Materials used")]
        [Required(ErrorMessage = "Please enter the materials used")]
        public string materials { get; set; }
    }
}