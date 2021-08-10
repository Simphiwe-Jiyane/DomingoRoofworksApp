using DomingoRoofworksApp.Data_access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomingoRoofworksApp.Models;
namespace DomingoRoofworksApp.Controllers
{
    public class InvoiceController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        //POST: Invoice/Create an invoice
        public ActionResult CreateInvoice(string type,string num_days,string cust_id,string job_id)
        {
            sqlAccess sa = new sqlAccess(); //Connect to the database methods
             
            List<InvoiceModel> inv = new List<InvoiceModel>(); //List to hold all the current nvoices

            if(type == null || num_days == null || cust_id == null || job_id == null) //Check if the fields are empty
            {
                return View(inv); //return an empty list
            }

            else
            {
                try
                {
                    inv = sa.CreateInvoice(type, Convert.ToInt32(num_days), cust_id, job_id); //Get the invoice based on the selected fields

                    if (inv == null) //if the list did not return a null-- check sqlAccess class
                    {
                        ViewBag.Bool = false; //Indicate that there are no invoices found 
                        ViewBag.Message = "There are no invoices on the system";
                        inv = null;
                    }

                    return View(inv);

                }
                catch (Exception)
                {
                    ViewBag.Bool = false; //Indicate that fileds must be filled out
                    ViewBag.Message = "Please fill out all the fields";
                    return View(inv);
                } 
            }
        }

        //POST: invoice/Find the selected invoice
        public ActionResult FindInvoice(string searching)
        {
            sqlAccess sa = new sqlAccess();//Connect to the database methods
            List<InvoiceModel> list = new List<InvoiceModel>(); //List to hold all the invoices from the db
            if (searching == null) //if the search field is null return all the invoices
            {
                list = sa.GetInvoices();
            }
            else
            {
                list.Add(sa.GetInvoices().Find(inv => inv.id == Convert.ToInt32(searching))); //Attempt to find the selected invoice
                if (sa.GetInvoices().Find(inv => inv.id == Convert.ToInt32(searching)) == null) //If sqlAccess returned null
                {
                    ViewBag.Bool = false; //Indicate that the invoice coulod not be found or does not exist
                    ViewBag.Message = "Invoice could not be found or does not exist. Check the Invoice number entered and try again";
                    list = null;
                }
            }

            return View(list);

        }
    }
}
