using System;
using System.Data.SqlClient;
using MRSUTWeb.Domain.Entities.User;
using MRSUTWeb.Helpers;
using System.Net.Mail;
using System.Net;
using System.Web;
using MRSUTWeb.BusinessLogic.DBModel;
using System.Linq;
using MRSUTWeb.Domain.Enums;
using AutoMapper;


namespace MRSUTWeb.BusinessLogic.Core
{
    public class UserApi
    {
        string salt = HashSaltGenerate.GenerateSalt();
        string code = HashSaltGenerate.GeneraterRandomCode();

        internal URegister RegisterUserAction(URegister register)
        {


            string hashPassword = HashSaltGenerate.HashPasswordWithSalt(register.Password, salt);
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MRSUTWeb;Integrated Security=True";
            string query = "INSERT INTO Userr (Name, Surname, Username, Email, Hash_Password, Salt, Code, ID_Type_user) VALUES (@Name, @Surname, @Username, @Email, @Hash_Password, @Salt, @Code, @ID_Type_user)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_User", register.Id);
                    command.Parameters.AddWithValue("@ID_Type_user", 1);
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
            return register;
        }
        internal void SendEmail(URegister register)
        {
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
            
        }
        internal ULoginResp UserLoginAction(ULoginData userLoginData)
        {
            ULoginResp loginResp = new ULoginResp();
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MRSUTWeb;Integrated Security=True";
            string query = "SELECT * FROM Userr WHERE Username = @Username";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", userLoginData.Username);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string salt = reader["Salt"].ToString();
                            string hashPassword = reader["Hash_Password"].ToString();
                            string password = HashSaltGenerate.HashPasswordWithSalt(userLoginData.Password, salt);
                            if (password == hashPassword)
                            {
                                loginResp.Status = true;
                                loginResp.StatusMsg = "Login successful";
                            }
                            else
                            {
                                loginResp.Status = false;
                                loginResp.StatusMsg = "Invalid password";
                            }
                        }
                        else
                        {
                            loginResp.Status = false;
                            loginResp.StatusMsg = "Invalid username";
                        }
                    }
                }
            }
            return loginResp;
        }

        internal HttpCookie GenerateAuthCookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential),
                Expires = DateTime.Now.AddHours(3)
            };
            using (var db = new UserContext())
            { 
            UDbTable currentUser = null;
                var validate = db.Users.FirstOrDefault(x => x.Username == loginCredential);

                if (validate != null) 
                {
                    currentUser = db.Users.FirstOrDefault(x => x.Username == loginCredential);
                }


                if (currentUser != null)
                {
                    var currentSession = db.Sessions.FirstOrDefault(x => x.UserId == currentUser.ID_User);
                    if (currentSession != null)
                    {
                        currentSession.CookieString = apiCookie.Value;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Sessions.Add(new Session
                        {
                            UserId = currentUser.ID_User,
                            CookieString = apiCookie.Value,
                            ExpireTime = DateTime.Now.AddHours(3)
                        });
                        db.SaveChanges();
                    }   
                }
            }
            return apiCookie;
        }
        internal UserMinimal UserCookie(string cookie)
        {
            Session session;
            UDbTable currentUser;

            using (var db = new UserContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null; // If the session does not exist or is expired, return null

            using (var db = new UserContext())
            {
                currentUser = db.Users.FirstOrDefault(u => u.ID_User == session.UserId);
            }

            if (currentUser == null) return null; // If the user does not exist, return null

            var config = new MapperConfiguration(cfg => cfg.CreateMap<UDbTable, UserMinimal>());
            var mapper = config.CreateMapper();
            var userminimal = mapper.Map<UserMinimal>(currentUser);

            return userminimal; // Return the UserMinimal object if the cookie is valid
        }
    }
}
