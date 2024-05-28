    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using MRSUTWeb.BusinessLogic;
    using MRSUTWeb.BusinessLogic.Interfaces;
    using MRSUTWeb.Domain.Entities.User;
    using FirstProj.Models;


    namespace FirstProj.Controllers
    {

        public class LoginController : Controller
        {
            private readonly ISession _session;
            public LoginController()
            {
                var bl = new BusinessLogic();
                _session = bl.GetSessionBL();
            }
            //GET: Login
            public ActionResult LogIn()
            {
            var xKeyCookie = Request.Cookies["X-KEY"];
            if (xKeyCookie != null)
            {
                var user = _session.GetUserByCookie(xKeyCookie.Value);
                if (user != null)
                {
                    ViewBag.UserRole = user.Level;
                    return RedirectToAction("LogedHome", "LogedUserHome");
                }
            }
            
        return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(MRSUTWeb.Web.Models.UserLogin login)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<MRSUTWeb.Web.Models.UserLogin, ULoginData>());
                var mapper = config.CreateMapper();
                var data = mapper.Map<ULoginData>(login);
                data.LoginIp = Request.UserHostAddress;
                data.LoginDateTime = DateTime.Now;
                var userLogin = _session.UserLogin(data);
                if (userLogin.Status)
                {
                    HttpCookie cookie = _session.GenCookie(login.Username);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    HttpCookie xKeyCookie = new HttpCookie("X-KEY", cookie.Value);
                    ControllerContext.HttpContext.Response.Cookies.Add(xKeyCookie);

                    return RedirectToAction("LogedHome", "LogedUserHome");
                }
                else
                {
                    ModelState.AddModelError("", userLogin.StatusMsg);
                }
            }
            return View("LogIn", login);
        }

        public UserMinimal GetUserDetails(string authToken)
            {
                return _session.GetUserByCookie(authToken);
            }
            public ActionResult LogOut()
            {
                var cookie = Request.Cookies["X-KEY"];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now;
                    Response.Cookies.Add(cookie);
                }
                return View();
            }

        }
    }