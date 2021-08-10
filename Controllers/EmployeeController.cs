using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DomingoRoofworksApp.Data_access;
using DomingoRoofworksApp.Models;

namespace DomingoRoofworksApp.Controllers
{
    public class EmployeeController : Controller
    {

        //GET
        public ActionResult Index()
        {
            return View();
        }

        //POST: Employee/ Find an employee
        public ActionResult FindEmployee(string searching)
        {
            sqlAccess sa = new sqlAccess(); //Instance of the sql data access class
            List<EmployeeModel> list = new List<EmployeeModel>(); //List to hold all the employees
            //If the search string is empty, return all the employees
            if(searching == null)
            {
                list = sa.FindEmployee();
            }
            else
            {
                //Find the employee based on their id
                list.Add(sa.FindEmployee().Find(emp => emp.id == searching));
                if(list == null)
                {
                    list = null; //return null if the list is empty
                }
            }
            
            return View(list); //return the list to the view

        }

        //GET:Employee/ return add employee view

        public ActionResult AddEmployee()
        {
            return View();
        }

        //POST:Employee/Add an employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(EmployeeModel emp)
        {
            sqlAccess sa = new sqlAccess(); //Connect to the database methods class

            try
            {
                //Try to insert the employee
                if (sa.CreateEmployee(emp.firstName,emp.lastname))
                {
                    ViewBag.Bool = true ; //Indicate that the employee was added
                }
                else
                {
                    ViewBag.Bool = false; //Indicate that the employee was not added
                }

                return View();

            }catch(Exception)
            {
                return View();
            }
        }


        //GET:Employee/ return Employee view
        public ActionResult UpdateEmployee()
        {
            return View();
        }

        //post:Employe/ update an employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEmployee(EmployeeModel emp)
        {
         sqlAccess sa = new sqlAccess(); //Connect to the database methods

            if (ModelState.IsValid) //if all the fields have been entered
            {
                if (sa.UpdateEmployee(emp)) //attempt to update the employee
                {
                    ViewBag.Bool = true; //indicate that the employee was updated
                    return View();
                }

                else
                    ViewBag.Bool = false; //indicate that the employee was not updated
                    ViewBag.Message = "Please check that Employee ID is correct and try again";
                return View(); 
            }
            else
            {
                ViewBag.Bool = false; //idicate that the fields are invalid
                ViewBag.Message = "Please fill in all the details";
                return View();
            }
        }
    }
}
