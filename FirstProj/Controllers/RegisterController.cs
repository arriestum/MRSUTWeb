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
using MRSUTWeb.BusinessLogic;
using MRSUTWeb.BusinessLogic.Interfaces;
using MRSUTWeb.Domain.Entities.User;


namespace FirstProj.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ISession _session;
        public RegisterController()
        {
            var bl = new BusinessLogic();
            _session = bl.GetSessionBL();
        }
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
                //create a new user
                URegister newUser = new URegister
                {
                    Name = register.Name,
                    Surname = register.Surname,
                    Username = register.Username,
                    Email = register.Email,
                    Password = register.Password
                };
                _session.Insert_RegisterUserAction(newUser);
                //_session.SendEmail_Register(newUser);

                ViewBag.Message = "User registered successfully";

            }
            return View();


        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ConfirmateAccount()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnterCode(VerificationCode model)
        {
            var xKeyCookie = Request.Cookies["X-KEY"];

            if (xKeyCookie != null)
            {
                var user = _session.GetUserByCookie(xKeyCookie.Value);
                if (user != null)
                {
                    //check inputed code with the one in the database

                    if (user.Code == model.Code)
                    {
                        //update user role
                        //_session.UpdateUserRole(xKeyCookie, model.Code);
                        //update user role to 1
                        ViewBag.UserRole = user.ID_Type_user;
                        user.ID_Type_user = 1;
                        _session.UpdateUserRole(xKeyCookie, user.ID_Type_user);
                        return RedirectToAction("ClientHome", "LogedUserHome");

                    }
                    else
                    {
                        ViewBag.UserRole = user.ID_Type_user;
                        return RedirectToAction("LogedHome", "LogedUserHome");

                    }
                }
            }
            return View();
        }

    }
}