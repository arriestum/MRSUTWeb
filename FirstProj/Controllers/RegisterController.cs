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
                var bl = new BusinessLogic();
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
                _session.SendEmail_Register(newUser);

                ViewBag.Message = "User registered successfully";
                
            }
            return View();


        }
        public ActionResult Index()
        {
            return View();
        }


    }
}