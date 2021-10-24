using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication2.Models;


namespace WebApplication2.Controllers
{
    public class PageTwoController : Controller
    {
        public string value = "";

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Registration e)
        {
            if (Request.HttpMethod == "POST")
            {
                using (SqlConnection con = new SqlConnection("Server=DESKTOP-BD5S77L; database=RegLogin;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_EnrollDetail2", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", e.Name);
                        cmd.Parameters.AddWithValue("@Address", e.Address);
                        cmd.Parameters.AddWithValue("@Username", e.Username);
                        cmd.Parameters.AddWithValue("@Password", e.Password);
                        cmd.Parameters.AddWithValue("@Role", e.Role);
                        cmd.Parameters.AddWithValue("@status", "INSERT");
                        con.Open();
                        ViewData["result"] = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            return View();
        }
    }
}