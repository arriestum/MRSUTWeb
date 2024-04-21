using FirstProj.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstProj.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult NewUser()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult NewUser(UserRegister register)
        {
            if (ModelState.IsValid)
            {
                // Add the user to the database         (LocalDB)\MSSQLLocalDB

                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MRSUTWeb;Integrated Security=True"; 
                string query = "INSERT INTO Temp_Userr (Name, Surname, Username, Email, Password) VALUES (@Name, @Surname, @Username, @Email, @Password)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_User", register.Id);
                        command.Parameters.AddWithValue("@Name", register.Name);
                        command.Parameters.AddWithValue("@Surname", register.Surname);
                        command.Parameters.AddWithValue("@Username", register.Username);
                        command.Parameters.AddWithValue("@Email", register.Email);
                        command.Parameters.AddWithValue("@Password", register.Password);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                ViewBag.Message = "User added successfully!";

            }
            else
            {
                ViewBag.Message = "Failed to add user!";
                return View(register);
            }
            return View();

        }
        public ActionResult Index()
        {
            return View();
        }



    }
}