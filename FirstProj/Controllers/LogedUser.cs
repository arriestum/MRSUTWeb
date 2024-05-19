using System.Web.Mvc;
using MRSUTWeb.BusinessLogic.Interfaces;
using MRSUTWeb.BusinessLogic;


namespace MRSUTWeb.Controllers
{
    public class LogedUser : Controller
    {
        private readonly ISession _session;

        public LogedUser()
        {
            var bl = new BusinessLogic();
            _session = bl.GetSessionBL();
        }

        public ActionResult Index()
        {
            var user = _session.GetUserByCookie(Request.Cookies["X-KEY"].Value);
            ViewBag.UserRole = user.Level;
            return View($"~/Views/Home/LogedHome.cshtml");
        }
    }
}