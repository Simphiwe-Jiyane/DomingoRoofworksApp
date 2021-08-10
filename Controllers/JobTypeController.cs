using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomingoRoofworksApp.Data_access;
using DomingoRoofworksApp.Models;

namespace DomingoRoofworksApp.Controllers
{
    public class JobTypeController : Controller
    {
        public ActionResult Index(JobTypeModel job)
        {
            sqlAccess sa = new sqlAccess(); //Connect to the db

            try
            {
                if (ModelState.IsValid) //Ensure all fields are valid and not empty
                {
                    if (!job.id.Equals("none")) //If the driop downlist value has been selected
                    {
                        if (sa.UpdateJobRate(job.id.ToUpper(), job.rate)) //attempt to update the job type rate
                        {
                            ViewBag.Bool = true; //Indicate that the job type  ratewas successfully updated
                            ViewBag.Message = "Job rate successfully updated";
                            return View();
                        }
                        else
                        {
                            ViewBag.Bool = true; //indicate that the job type rate has not been updated
                            ViewBag.Message = "Job rate could not updated"; 
                            return View();
                        }
                    }
                    else
                    {
                        //Indicate that the drop down list value must be selected
                        ViewBag.JobWrong = false;
                        return View();
                    }
                }
                else
                {
                    //Fields are empty or invalid
                    return View();
                }
            }
            catch (Exception)
            {
                //Indicate that the connection to the databse failed
                ViewBag.Message = "Cannot connect to the server";
                return View();
            }
        }
    }
}