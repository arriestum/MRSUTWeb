using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRSUTWeb.Domain.Enums;

namespace MRSUTWeb.Domain.Entities.User
{
    public class UserMinimal
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
        public string LastIp { get; set; }
        public URoles Level { get; set; }
    }
}
