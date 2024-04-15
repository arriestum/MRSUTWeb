﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRSUTWeb.Domain.Entities.User
{
    public class UserLogin
    {
        public string Credential { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public string StatusMsg { get; set; }
    }
    public class ULoginData
    {
        public string Credential { get; set; }
        public string Password { get; set; }
        public string LoginIp { get; set; }
        public DateTime LoginDateTime { get; set; }
    }
    
}
