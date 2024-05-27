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


namespace MRSUTWeb.BusinessLogic.Core
{
    public class UserApi
    {



        internal URegister RegisterUserAction(URegister register)
        {
            string salt = HashSaltGenerate.GenerateSalt();
            string code = HashSaltGenerate.GeneraterRandomCode();
            string hashPassword = HashSaltGenerate.HashPasswordWithSalt(register.Password, salt);
            UDbTable existingEmail;
            UDbTable existingUsername;
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
            string salt;
            using (var db = new UserContext())
            {
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
