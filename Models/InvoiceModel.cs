using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DomingoRoofworksApp.Models
{
    public class InvoiceModel
    {
        [Display(Name = "Invoice number")]
        public int id { get; set; }

        [Required(ErrorMessage = "Please enter the job rate")]
        public double rate { get; set; }

        [Required(ErrorMessage = "Please enter the number of days")]
        public int num_days { get; set; }

        [Required(ErrorMessage = "Please enter the customer ID")]
        public string cust_id { get; set; }

        [Required(ErrorMessage = "Please enter the Job card number")]
        public string job_card_id { get; set; }

        public double subtotal { get; set; }

        public double total { get; set; }

    }
}