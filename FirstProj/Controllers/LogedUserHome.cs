using System.Web.Mvc;
using MRSUTWeb.BusinessLogic.Interfaces;
using MRSUTWeb.BusinessLogic;
using Microsoft.Ajax.Utilities;

namespace MRSUTWeb.Web.Controllers
{
    public class LogedUserHomeController : Controller
    {
        private readonly ISession _session;

        public LogedUserHomeController()
        {
            var bl = new MRSUTWeb.BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }

        public ActionResult LogedHome()
        {
            var xKeyCookie = Request.Cookies["X-KEY"];
            if (xKeyCookie != null)
            {
                var user = _session.GetUserByCookie(xKeyCookie.Value);
                if (user != null)
                {
                    ViewBag.UserRole = user.ID_Type_user;

                    switch (user.ID_Type_user)
                    {
                        case 0:
                            return RedirectToAction("ConfirmateAccount", "Register");
                        case 1:
                            return RedirectToAction("ClientHome", "LogedUserHome");
                        case 2:
                            return RedirectToAction("AdminHome", "LogedUserHome");

                    }
                }
            }
            if (xKeyCookie == null)
            {
                return RedirectToAction("LogIn", "Login");
            }
            return View();
        }
        public ActionResult ClientHome()
        {
            return View();
        }
        public ActionResult AdminHome()
        {
            return View();
        }
    }
}

