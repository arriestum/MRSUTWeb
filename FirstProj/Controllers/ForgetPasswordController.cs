using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstProj.Models;
using MRSUTWeb.BusinessLogic.Core;
using MRSUTWeb.Web.Models;
using static MRSUTWeb.BusinessLogic.Core.UserApi;

namespace FirstProj.Controllers
{
    public class ForgetPasswordController : Controller
    {
        public ActionResult ForgetPassword()
        {
            return View();
        }
        private readonly UserApi _userApi;

        public ForgetPasswordController()
        {
            _userApi = new UserApi();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendCode(string username)
        {
            _userApi.SendResetCode(username);
            return RedirectToAction("EnterCode");
        }

        public ActionResult EnterCode()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {

            if (!ModelState.IsValid)
            {

                return View("EnterCode", model);
            }

            if (!_userApi.VerifyResetCode(model.Username, model.Code))
            {

                ModelState.AddModelError("Code", "Invalid reset code");
                return View("EnterCode", model);
            }

            _userApi.ResetPassword(model.Username, model.Code, model.NewPassword);

            return RedirectToAction("PasswordResetSuccess");
        }


        public ActionResult PasswordResetSuccess()
        {
            return View();
        }




    }

}