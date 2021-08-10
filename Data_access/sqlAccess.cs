using DomingoRoofworksApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DomingoRoofworksApp.Data_access
{
    public class sqlAccess
    {
        //Global
        //==============================================================================================
        static string constr = ConfigurationManager.ConnectionStrings["getConn"].ToString();

        private static SqlConnection conn = new SqlConnection(constr);

        private static SqlCommand cmd;
        //=============================================================================================



        /// <summary>
        /// Method to Create a new job card
        /// </summary>
        /// <param name="cust_id"></param>
        /// <param name="cust_name"></param>
        /// <param name="cust_last"></param>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <param name="city"></param>
        /// <param name="postal"></param>
        /// <param name="type"></param>
        /// <param name="num_days"></param>
        /// <param name="materials"></param>
        /// <returns>returns true if the card was created successfully</returns>
        public bool CreateJob(string cust_id, string cust_name,string cust_last,string address1,string address2,string city,string postal,string type,int num_days, string materials)
        {

            conn.Open();

            try
            {

                cmd = new SqlCommand("select * from Job_Card", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                int count = dt.Rows.Count;

                da.Dispose();
                dt.Dispose();
                cmd.Dispose();

                cmd = new SqlCommand("createJob", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@j_card", Convert.ToString(count + 1));
                cmd.Parameters.AddWithValue("@cust_id", cust_id);
                cmd.Parameters.AddWithValue("@cust_firstname", cust_name);
                cmd.Parameters.AddWithValue("@cust_lastname", cust_last);
                cmd.Parameters.AddWithValue("@address1", address1);
                cmd.Parameters.AddWithValue("@address2", address2);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@postal", postal);
                cmd.Parameters.AddWithValue("@type_id", type);
                cmd.Parameters.AddWithValue("@no_of_days", num_days);
                cmd.Parameters.AddWithValue("@materials", materials);

                int i = cmd.ExecuteNonQuery();

                conn.Close();

                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
            
        }

        /// <summary>
        /// Method to get all job cards
        /// </summary>
        /// <returns>List of type JobCardModel</returns>
        public List<JobCardModel> getAllJobs()
        {
            List<JobCardModel> list = new List<JobCardModel>();

            conn.Open();

            try
            {     

                cmd = new SqlCommand("searchJob", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {



                    list.Add(
                        new JobCardModel
                        {
                            id = Convert.ToString(item["Job_Card_No"]),
                            cust_id = Convert.ToString(item["Customer_ID"]),
                            cust_name = Convert.ToString(item["Customer_firstname"]),
                            cust_lastname = Convert.ToString(item["Customer_lastname"]),
                            addressline1 = Convert.ToString(item["addressline1"]),
                            addressline2 = Convert.ToString(item["addressline2"]),
                            city = Convert.ToString(item["city"]),
                            postal = Convert.ToString(item["postal"]),
                            type = Convert.ToString(item["Job_Type_ID"]),
                            num_days = Convert.ToInt32(item["No_Of_Days"]),
                            materials = Convert.ToString(item["Job_Materials"])
                        }
                        
                        );

                }

                conn.Close();
                return list;

            }catch(Exception)
            {
                conn.Close();
                return null;
            }

        }

        /// <summary>
        /// Method to update the current job rate of the selected job type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rate"></param>
        /// <returns>returns true if the rate was updated</returns>
        public bool UpdateJobRate(string id, double rate)
        {
            conn.Open();

            try
            {
                cmd = new SqlCommand("updateJobRate", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@job_type_num", id);
                cmd.Parameters.AddWithValue("@rate", rate);

                int i = cmd.ExecuteNonQuery();
                conn.Close();

                if(i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }



            }catch(Exception ex)
            {
                conn.Close();
                return false;
            }
        }

        /// <summary>
        /// Method to create a new employee
        /// </summary>
        /// <param name="first_name"></param>
        /// <param name="surname"></param>
        /// <returns>Returns true if the employee was created successfully</returns>
        public bool CreateEmployee(string first_name, string surname)
        {
            conn.Open();

            try
            {
                cmd = new SqlCommand("select * from Employee", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                int count = dt.Rows.Count;
                string id = "EMP" + (count + 1000);

                da.Dispose();
                dt.Dispose();
                cmd.Dispose();

                cmd = new SqlCommand("addEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@first_name", first_name);
                cmd.Parameters.AddWithValue("@last_name", surname);

                int i = cmd.ExecuteNonQuery();

                conn.Close();

                if(i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }catch(Exception ex)
            {
                conn.Close();
                return false;
            }
        }


        /// <summary>
        /// Method to update an existing employee
        /// </summary>
        /// <param name="emp"></param>
        /// <returns>returns true if the employee was updated successfully</returns>
        public bool UpdateEmployee(EmployeeModel emp)
        {
            conn.Open();

            try
            {

                cmd = new SqlCommand("updateEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", emp.id);
                cmd.Parameters.AddWithValue("@first_name", emp.firstName);
                cmd.Parameters.AddWithValue("@last_name",emp.lastname);

                int i = cmd.ExecuteNonQuery();

                conn.Close();

                if(i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }catch(Exception ex)
            {
                conn.Close();
                return false;
            }
        }


        /// <summary>
        /// Method to return all employees
        /// </summary>
        /// <returns>returns a list of EmployeeModel</returns>
        public List<EmployeeModel> FindEmployee()
        {
            List<EmployeeModel> emps = new List<EmployeeModel>();
            conn.Open();

            try
            {

                cmd = new SqlCommand("findEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {

                    emps.Add(                       
                            new EmployeeModel
                            {
                                id = Convert.ToString(item["Employee_ID"]),
                                firstName = Convert.ToString(item["Employee_name"]),
                                lastname = Convert.ToString(item["Employee_surname"])
                                
                            }  
                        );
                }

                conn.Close();
                return emps;
                //TODO: Check if the employee was found
            }
            catch(Exception ex)
            {
                conn.Close();
                return null;

            }
        }

        /// <summary>
        /// Method to create a new invoice
        /// </summary>
        /// <param name="type"></param>
        /// <param name="number_days"></param>
        /// <param name="customer_id"></param>
        /// <param name="job_id"></param>
        /// <returns>Returns all invoices in the database</returns>
        public List<InvoiceModel> CreateInvoice(string type,int number_days,string customer_id,string job_id)
        {
            List<InvoiceModel> inv = new List<InvoiceModel>();
            double rate = 0;
            conn.Open();

            try
            {

                cmd = new SqlCommand("select Daily_Rate from job_Type where Job_Type_ID = '" + type + "'",conn);
                SqlDataReader sd = cmd.ExecuteReader();
                while (sd.Read())
                {
                    rate = Convert.ToDouble(sd["Daily_Rate"]);
                }
                sd.Close();
                cmd.Dispose();

                //===============================================

                cmd = new SqlCommand("Invoice_Line", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@rate", rate);
                cmd.Parameters.AddWithValue("@num_days", number_days);
                cmd.Parameters.AddWithValue("@customer_id", customer_id);
                cmd.Parameters.AddWithValue("@card_no", job_id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    inv.Add(
                        
                            new InvoiceModel
                            {
                                id = Convert.ToInt32(item["Invoice_No"]),
                                cust_id = Convert.ToString(item["Customer_ID"]),
                                job_card_id = Convert.ToString(item["Job_card_No"]),
                                rate = Convert.ToInt32(item["rate"]),
                                num_days = Convert.ToInt32(item["no_Of_Days"]),
                                subtotal = Convert.ToDouble(item["subtotal"]),
                                total = Convert.ToDouble(item["total"])
                            }
                        
                        );

                }

                conn.Close();
                return inv;
            }catch(Exception ex)
            {
                conn.Close();
                return null;
            }
        }


        /// <summary>
        /// Method to search for an existing invoice
        /// </summary>
        /// <returns> returns a list of all invoices</returns>
        public List<InvoiceModel> GetInvoices()
        {
            List<InvoiceModel> list = new List<InvoiceModel>();
            conn.Open();

            try
            {
                cmd = new SqlCommand("GetAllInvoices", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                try
                {
                    foreach (DataRow item in dt.Rows)
                    {

                        list.Add(

                            new InvoiceModel
                            {
                                id = Convert.ToInt32(item["Invoice_No"]),
                                cust_id = Convert.ToString(item["Customer_ID"]),
                                job_card_id = Convert.ToString(item["Job_Card_No"]),
                                rate = Convert.ToDouble(item["rate"]),
                                num_days = Convert.ToInt32(item["no_Of_Days"]),
                                subtotal = Convert.ToDouble(item["subtotal"]),
                                total = Convert.ToDouble(item["total"])
                            }

                            );

                    }
                    conn.Close();
                    return list;
                }
                catch (Exception)
                {
                    conn.Close();
                    return null;
                }
            }
            catch (Exception)
            {
                conn.Close();
                return null;
            }

        }
        //TODO:Method for invoice
    }
}