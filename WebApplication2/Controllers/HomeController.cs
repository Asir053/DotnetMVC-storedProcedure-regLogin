using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication2.Models;


namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        // GET: /UserLogin/  
        public string status;

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Registration e)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;
            SqlConnection con = new SqlConnection(mainconn);
            string SqlQuery = "select Username,Password from [RegLogin].[dbo].[Enrollment2] where Username=@Username and Password=@Password";
            con.Open();
            SqlCommand cmd = new SqlCommand(SqlQuery, con);
            cmd.Parameters.AddWithValue("@Username", e.Username);
            cmd.Parameters.AddWithValue("@Password", e.Password);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Session["Username"] = e.Username.ToString();
                return RedirectToAction("Welcome");
            }
            else
            {
                ViewData["Message"] = "User Login Details Failed!!";
            }
            if (e.Username.ToString() != null)
            {
                Session["Username"] = e.Username.ToString();
                status = "1";
            }
            else
            {
                status = "3";
            }

            con.Close();
            return View();

        }

        [HttpGet]
        public ActionResult Welcome(Registration e)
        {
            Registration user = new Registration();
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection("Server=DESKTOP-BD5S77L; database=RegLogin;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetEnrollmentDetails2", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 30).Value = Session["Username"].ToString();
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    List<Registration> userlist = new List<Registration>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Registration uobj = new Registration();
                        if (Session["Username"].ToString() == ds.Tables[0].Rows[i]["Username"].ToString())
                        {
                            uobj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                            uobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                            uobj.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                            uobj.Username = ds.Tables[0].Rows[i]["Username"].ToString();
                            uobj.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                            uobj.Role = ds.Tables[0].Rows[i]["Role"].ToString();
                        }
                        //uobj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        //uobj.FirstName = ds.Tables[0].Rows[i]["Name"].ToString();
                        //uobj.LastName = ds.Tables[0].Rows[i]["Address"].ToString();
                        //uobj.Password = ds.Tables[0].Rows[i]["Username"].ToString();
                        //uobj.Email = ds.Tables[0].Rows[i]["Password"].ToString();
                        //uobj.PhoneNumber = ds.Tables[0].Rows[i]["Role"].ToString();

                        userlist.Add(uobj);

                    }
                    user.Registrationsinfo = userlist;
                }
                con.Close();

            }
            return View(user);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "PageOne");
        }
    }
}