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
using System.ComponentModel.DataAnnotations;
using Microsoft.Win32;


namespace MRSUTWeb.BusinessLogic.Core
{
    public class UserApi
    {



        internal URegister RegisterUserAction(URegister register)
        {
            string salt = HashSaltGenerate.GenerateSalt();
            string code = HashSaltGenerate.GeneraterRandomCode();
            string hashPassword = HashSaltGenerate.HashPasswordWithSalt(register.Password, salt);
            //UDbTable existingEmail;
            //UDbTable existingUsername;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(register.Email))
            {
                //check if email exists
                //using (var db = new UserContext())
                //{
                //    existingEmail = db.Userr.FirstOrDefault(x => x.Email == register.Email);
                //    existingUsername = db.Userr.FirstOrDefault(x => x.Username == register.Username);

                //}
                //if (existingEmail != null)
                //{
                //    throw new Exception("Email already exists");
                //}

                var newuser = new UDbTable
                {
                    Name = register.Name,
                    Surname = register.Surname,
                    Username = register.Username,
                    Email = register.Email,
                    Password = hashPassword,
                    Salt = salt,
                    Code = code,
                    ID_Type_user = 1
                };
                using (var db = new UserContext())
                {
                    db.Userr.Add(newuser);
                    db.SaveChanges();
                }

            }


            SendEmail(register, code);
            return register;
        }
        internal void SendEmail(URegister register, string code)
        {
            // Send email to the user
            MailMessage mail = new MailMessage("madarableach55@gmail.com", register.Email);
            mail.Subject = "Confirmare cont";
            mail.Body = "Codul tau de confirmare a contului BadBank: " + code;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("madarableach55@gmail.com", "nhrjcgocqzznmove");//ascundeti parola
            smtp.EnableSsl = true;
            smtp.Send(mail);

        }

        public void SendEmail_ResetPassword(string email, string code)
        {
            // Send email to the user
            MailMessage mail = new MailMessage("madarableach55@gmail.com", email);
            mail.Subject = "Restabilire parola";
            mail.Body = "Codul pentru restabilire a parolei de la contul BadBank: " + code;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("madarableach55@gmail.com", "nhrjcgocqzznmove");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }



        public void SendResetCode(string username)
        {
            // Generare și salvare cod resetare în baza de date
            string code = HashSaltGenerate.GeneraterRandomCode();
            using (var db = new UserContext())
            {
                var user = db.Userr.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    user.ResetPasswordCode = code;
                    db.SaveChanges();

                    // Trimitere cod de resetare prin email
                    SendEmail_ResetPassword(user.Email, code);
                }
            }
        }



        public void ResetPassword(string username, string code, string newPassword)
        {
            using (var db = new UserContext())
            {
                var user = db.Userr.FirstOrDefault(u => u.Username == username && u.ResetPasswordCode.Equals(code, StringComparison.OrdinalIgnoreCase));
                if (user != null)
                {
                    string salt = HashSaltGenerate.GenerateSalt();
                    string hashPassword = HashSaltGenerate.HashPasswordWithSalt(newPassword, salt);
                    user.Password = hashPassword;
                    user.Salt = salt;
                    user.ResetPasswordCode = null;
                    db.SaveChanges();
                }
            }
        }

        public bool VerifyResetCode(string username, string code)
        {
            using (var db = new UserContext())
            {
                var user = db.Userr.FirstOrDefault(u => u.Username == username && u.ResetPasswordCode == code);
                return user != null;
            }
        }




        internal ULoginResp UserLoginAction(ULoginData userLoginData)
        {
            ULoginResp loginResp = new ULoginResp();
            string salt;
            using (var db = new UserContext())
            {

                if (db.Userr.FirstOrDefault(x => x.Username == userLoginData.Username) == null)
                {
                    loginResp.Status = false;
                    loginResp.StatusMsg = "Login failed";
                    return loginResp;
                }
                salt = db.Userr.FirstOrDefault(x => x.Username == userLoginData.Username).Salt;
            }
            var hashPassword = HashSaltGenerate.HashPasswordWithSalt(userLoginData.Password, salt);
            UDbTable user;
            using (var db = new UserContext())
            {
                user = db.Userr.FirstOrDefault(x => x.Username == userLoginData.Username && x.Password == hashPassword);
            }
            if (user != null)
            {
                loginResp.Status = true;
                loginResp.StatusMsg = "Login successful";
            }
            else
            {
                loginResp.Status = false;
                loginResp.StatusMsg = "Login failed";
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
                UDbTable currentUser = db.Userr.FirstOrDefault(x => x.Username == loginCredential);

                if (currentUser != null)
                {
                    var currentSession = db.Sessions.FirstOrDefault(x => x.ID_User == currentUser.ID_User);
                    if (currentSession != null)
                    {
                        currentSession.SessionToken = apiCookie.Value;
                        currentSession.ExpiryDateTime = DateTime.Now.AddHours(3);
                    }
                    else
                    {
                        db.Sessions.Add(new Session
                        {
                            ID_User = currentUser.ID_User,
                            SessionToken = apiCookie.Value,
                            ExpiryDateTime = DateTime.Now.AddHours(3)
                        });
                    }
                    db.SaveChanges();
                }
            }
            return apiCookie;
        }


        internal UserMinimal GetUserByRole(int roleValues)
        {
            //function to return usersw type role
            using (var db = new UserContext())
            {
                var user = db.Userr.FirstOrDefault(u=>u.ID_Type_user == roleValues);
                return GetUserByRole(roleValues);
            }
        }
        internal UserMinimal UserCookie(string cookie)
        {
            Session session;
            UDbTable currentUser;

            using (var db = new UserContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.SessionToken == cookie && s.ExpiryDateTime > DateTime.Now);
            }

            if (session == null) return null;

            using (var db = new UserContext())
            {
                currentUser = db.Userr.FirstOrDefault(u => u.ID_User == session.ID_User);
            }

            if (currentUser == null) return null;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<UDbTable, UserMinimal>());
            var mapper = config.CreateMapper();
            var userminimal = mapper.Map<UserMinimal>(currentUser);

            return userminimal;
        }


    }
}
