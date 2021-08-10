using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DomingoRoofworksApp.Models
{
    public class CustomerModel
    {
        [Display(Name = "CustomerID")]
        public string id { get; set; }

        [Required(ErrorMessage ="Please enter the customer's first name")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Please enter the customer's surname")]
        public string surname { get; set; }

        [Required(ErrorMessage = "Please enter the customer's address")]
        public string address { get; set; }
    }
}