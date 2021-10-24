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
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            Registration user = new Registration();
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection("Server=DESKTOP-BD5S77L; database=RegLogin;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetEnrollmentDetails3", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    List<Registration> userlist = new List<Registration>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Registration uobj = new Registration();
                        //if (Session["Username"].ToString() == ds.Tables[0].Rows[i]["Username"].ToString())
                        //{
                        //    uobj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        //    uobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        //    uobj.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                        //    uobj.Username = ds.Tables[0].Rows[i]["Username"].ToString();
                        //    uobj.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                        //    uobj.Role = ds.Tables[0].Rows[i]["Role"].ToString();
                        //}
                        uobj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        uobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        uobj.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                        uobj.Username = ds.Tables[0].Rows[i]["Username"].ToString();
                        uobj.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                        uobj.Role = ds.Tables[0].Rows[i]["Role"].ToString();

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