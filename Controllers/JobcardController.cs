using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomingoRoofworksApp.Data_access;
using DomingoRoofworksApp.Models;

namespace DomingoRoofworksApp.Controllers
{
    public class JobcardController : Controller
    {
    
        public ActionResult Index()
        {
            return View();
        }

        //GET: JobCard/return selected job card
        public ActionResult GetJob(string j_id)
        {
            sqlAccess sa = new sqlAccess(); //Connect to the database connection methods
            List<JobCardModel> jobs = new List<JobCardModel>(); //List tom hold all the job cards
            
            try //try to find the selected invoice
            {
                if(j_id == null)
                {
                    return View(sa.getAllJobs());
                }
                else
                {
                    jobs.Clear();
                    jobs.Add(sa.getAllJobs().Find(job => job.id == j_id)); //search for the selected invoice
                    if(sa.getAllJobs().Find(job => job.id == j_id) == null)
                    {
                        ViewBag.Bool = false; //indicate that the invoice could not be found
                        ViewBag.Message = "Job Card could not be found. Check the Job Card number entered and try again";
                        jobs = null;
                    }
                    return View(jobs);
        
                }
            }
            catch (Exception)
            {
                ViewBag.Serv = false; //Indicate the connection to the database failed
                ViewBag.Message = "Could not establish a connection to the server. Please try again";
                return View(jobs);
            }

        }

        //GET: JobCard/ return job card
        public ActionResult NewJob()
        {
            return View();
        }

        //POST: JobCard/Create job
        [HttpPost]
        public ActionResult Newjob(JobCardModel job)
        {
            sqlAccess sa = new sqlAccess();
            try
            {
                if (ModelState.IsValid) //Check if all fields are valid
                {
                    //Attempt to create the new job card
                    if(sa.CreateJob(job.cust_id, job.cust_name,job.cust_lastname,job.addressline1,job.addressline2,job.city,
                                    job.postal, job.type, job.num_days, job.materials))
                    {
                        ViewBag.Message = "Job Successfully created"; //Indicate that the job was created successfully
                        return View();
                    }
                    else
                    {
                        //Indicate the job was not created
                        ViewBag.Message = "Could not create a new job card, please check the information entered and try again";
                        ViewBag.Bool = false;
                        return View();
                    }
                }
                else
                {
                    //Fields are not valid or empty
                    return View();
                }


            }catch(Exception ex)
            {
                //Connection to the database did not work
                ViewBag.Message = "Could not connect to the server, please try again later";
                ViewBag.Bool = false;
                return View();
            }
        }
    }
}
