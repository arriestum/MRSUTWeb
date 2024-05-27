using System.Web.Mvc;
using MRSUTWeb.BusinessLogic.Interfaces;
using MRSUTWeb.BusinessLogic;

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
                    ViewBag.UserRole = user.Level;
                    return View();
                }
            }
            if (xKeyCookie == null)
            {
                return RedirectToAction("LogIn", "Login");
            }
            return View();
        }
        
        public ActionResult AdminHome()
        {
            return View();
        }
    }
}

