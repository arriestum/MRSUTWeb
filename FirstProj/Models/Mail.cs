using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstProj.Models
{
    public class Mail
    {
        private string receiver;
        private string email;

        public Mail(string receiver, string email)
        {
            this.receiver = receiver;
            this.email = email;
        }

        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Code { get; set; }
        //public Mail() { }
        //public Mail(string to, string from, string subject, string body)
        //{
        //    To = to;
        //    From = from;
        //    Subject = subject;
        //    Body = body;
        //}
    }
}