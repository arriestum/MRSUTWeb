using FirstProj.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

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
        [ValidateAntiForgeryToken]
        public ActionResult NewUser(UserRegister register)
        {
            if (ModelState.IsValid)
            {
                string salt = GenerateSalt();
                string code = GeneraterRandomCode();
                // Add the user to the database         (LocalDB)\MSSQLLocalDB
                string hashPassword = HashPasswordWithSalt(register.Password , salt);
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MRSUTWeb;Integrated Security=True";
                // import from Web.config connection string
                string query = "INSERT INTO Temp_Userr (Name, Surname, Username, Email, Hash_Password, Salt, Code) VALUES (@Name, @Surname, @Username, @Email, @Hash_Password, @Salt, @Code)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_User", register.Id);
                        command.Parameters.AddWithValue("@Name", register.Name);
                        command.Parameters.AddWithValue("@Surname", register.Surname);
                        command.Parameters.AddWithValue("@Username", register.Username);
                        command.Parameters.AddWithValue("@Email", register.Email);
                        command.Parameters.AddWithValue("@Hash_Password", hashPassword);
                        command.Parameters.AddWithValue("@Salt", salt);
                        command.Parameters.AddWithValue("@Code", code);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                ViewBag.Message = "User added successfully!";


                // Send email to the user
                MailMessage mail = new MailMessage("madarableach55@gmail.com", register.Email);
                mail.Subject = "Confirm your email";
                mail.Body = "Your confirmation code is: " + code;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("madarableach55@gmail.com", "nhrjcgocqzznmove");//ascundeti parola
                smtp.EnableSsl = true;
                smtp.Send(mail);
                ViewBag.Message = "Email sent successfully!";
            }
            else
            {
                ViewBag.Message = "Failed to add user!";
                return View();
            }
            return View();

        }
        public ActionResult Index()
        {
            return View();
        }
        private string HashPasswordWithSalt(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(password + salt); // Adaugă salt-ul la parolă
                byte[] hashedBytes = sha256.ComputeHash(saltedPasswordBytes);
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
        private string GeneraterRandomCode()
        {
            Random random = new Random();
            int rand = random.Next(100000, 999999);
            return rand.ToString();
        }
        

    }
}