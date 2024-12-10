using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using TechSolutions.Models;

namespace TechSolutions.Controllers
{
    public class StudentsController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        // GET: Students
        public ActionResult Index()
        {
            try
            {
                List<StudentModel> list = new List<StudentModel>();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_Student_Fetch", con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        list.Add(new StudentModel
                        {
                            Id = Convert.ToInt32(sdr["Id"].ToString()),
                            name = sdr["name"].ToString(),
                            course = sdr["course"].ToString(),
                            fees = Convert.ToInt32(sdr["fees"].ToString()),
                            payment = sdr["payment"].ToString(),
                            profile = sdr["profile"].ToString()

                        });
                    }
                    con.Close();
                }


                return View(list);
            }
            catch
            {
                return View();
            }
            
        }

		[HttpPost]
		public ActionResult Index(string Search)
		{
			try
			{
				List<StudentModel> list = new List<StudentModel>();
				using (SqlConnection con = new SqlConnection(constr))
				{
					SqlCommand cmd = new SqlCommand("sp_Student_Search " + Search, con);
					con.Open();
					SqlDataReader sdr = cmd.ExecuteReader();
					while (sdr.Read())
					{
						list.Add(new StudentModel
						{

							name = sdr["name"].ToString(),
							course = sdr["course"].ToString(),
							fees = Convert.ToInt32(sdr["fees"].ToString()),
							payment = sdr["payment"].ToString(),
							profile = sdr["profile"].ToString()


						});
					}
					con.Close();
				}


				return View(list);
			}
			catch
			{
				return View();
			}

		}

		// GET: Students/Details/5
		public ActionResult Details(int id)
        {
            try
            {
                StudentModel list = new StudentModel();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_Student_FetchId " + id, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        list = new StudentModel
                        {
                            Id = Convert.ToInt32(sdr["Id"].ToString()),
                            name = sdr["name"].ToString(),
                            course = sdr["course"].ToString(),
                            fees = Convert.ToInt32(sdr["fees"].ToString()),
                            payment = sdr["payment"].ToString(),
                            profile = sdr["profile"].ToString()

                        };
                    }
                    con.Close();
                }


                return View(list);
            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        public ActionResult Create(StudentModel student)
        {
            try
            {
                HttpPostedFileBase upload = student.Temp_Profile;
                if (upload != null)
                {
                    string Extension = Path.GetExtension(student.Temp_Profile.FileName);
                    string imagename = student.name + Extension;
                    student.profile = imagename;
                    string imgpath = Path.Combine(Server.MapPath("~/Content/Profiles/" + imagename));
                    student.Temp_Profile.SaveAs(imgpath);
                }
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    string query = "sp_Student_Add @name,@course,@fees,@payment,@profile";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", student.name);
                    cmd.Parameters.AddWithValue("@course", student.course);
                    cmd.Parameters.AddWithValue("@fees", student.fees);
                    cmd.Parameters.AddWithValue("@payment", student.payment);
                    cmd.Parameters.AddWithValue("@profile", student.profile);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return RedirectToAction("Index");

                }
                
            }
            catch (Exception ex)
            {
                return View();
            }
           
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
            try 
            { 

                StudentModel list = new StudentModel();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_Student_FetchId " + id, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        list = new StudentModel
                        {
                            Id = Convert.ToInt32(sdr["Id"].ToString()),
                            name = sdr["name"].ToString(),
                            course = sdr["course"].ToString(),
                            fees = Convert.ToInt32(sdr["fees"].ToString()),
                            payment = sdr["payment"].ToString(),
                            profile = sdr["profile"].ToString()

                        };
                    }
                    con.Close();
                }


                return View(list);
            }
            catch
            {
                return View();
            }
        }

        // POST: Students/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, StudentModel student)
        {
            try
            {
                HttpPostedFileBase upload = student.Temp_Profile;
                if (upload != null)
                {
                    string Extension = Path.GetExtension(student.Temp_Profile.FileName);
                    string imagename = student.name + Extension;
                    student.profile = imagename;
                    string imgpath = Path.Combine(Server.MapPath("~/Content/Profiles/" + imagename));
                    student.Temp_Profile.SaveAs(imgpath);
                }
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    string query = "sp_Student_Update @id,@course,@fees,@payment,@profile";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@course", student.course);
                    cmd.Parameters.AddWithValue("@fees", student.fees);
                    cmd.Parameters.AddWithValue("@payment", student.payment);
                    cmd.Parameters.AddWithValue("@profile", student.profile);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                StudentModel list = new StudentModel();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_Student_FetchId " + id, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        list = new StudentModel
                        {
                            Id = Convert.ToInt32(sdr["Id"].ToString()),
                            name = sdr["name"].ToString(),
                            course = sdr["course"].ToString(),
                            fees = Convert.ToInt32(sdr["fees"].ToString()),
                            payment = sdr["payment"].ToString(),
                            profile = sdr["profile"].ToString()

                        };
                    }
                    con.Close();
                }


                return View(list);
            }
            catch
            {
                return View();
            }
            
        }

        // POST: Students/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                string path = Request.MapPath("~/Content/Profiles/" + Profile);
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                using(SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_Student_Delete " + id, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

		public ActionResult Search()
		{
			try
			{
				List<StudentModel> list = new List<StudentModel>();
				using (SqlConnection con = new SqlConnection(constr))
				{
					SqlCommand cmd = new SqlCommand("sp_Student_Fetch", con);
					con.Open();
					SqlDataReader sdr = cmd.ExecuteReader();
					while (sdr.Read())
					{
						list.Add(new StudentModel
						{
							
							name = sdr["name"].ToString(),
							course = sdr["course"].ToString(),
							fees = Convert.ToInt32(sdr["fees"].ToString()),
							payment = sdr["payment"].ToString(),
							profile = sdr["profile"].ToString()


						});
					}
					con.Close();
				}


				return View(list);
			}
			catch
			{
				return View();
			}

		}

		[HttpPost]
		public ActionResult Search(string Search)
		{
			try
			{
				List<StudentModel> list = new List<StudentModel>();
				using (SqlConnection con = new SqlConnection(constr))
				{
					SqlCommand cmd = new SqlCommand("sp_Student_Search " + Search, con);
					con.Open();
					SqlDataReader sdr = cmd.ExecuteReader();
					while (sdr.Read())
					{
						list.Add(new StudentModel
						{
							
							name = sdr["name"].ToString(),
							course = sdr["course"].ToString(),
							fees = Convert.ToInt32(sdr["fees"].ToString()),
							payment = sdr["payment"].ToString(),
							profile = sdr["profile"].ToString()


						});
					}
					con.Close();
				}


				return View(list);
			}
			catch
			{
				return View();
			}

		}
	}
}
