using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstProj.Models;
using System.Net.Mail;
using System.Net;

namespace FirstProj.Controllers
{
    public class Email_SetupController : Controller
    {
        // GET: Email_Setup
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index( FirstProj.Models.Mail model)
        {
           
            MailMessage mail = new MailMessage(model.From,model.To);
            mail.Subject = model.Subject;
            mail.Body = model.Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("contpm2@gmail.com", "Secret_11");
            smtp.EnableSsl = true;
            smtp.Send(mail);
            ViewBag.Message = "Email sent successfully!";

            return View();
        }
    }
}