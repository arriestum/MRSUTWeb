using System;
using MRSUTWeb.Domain.Entities;
using MRSUTWeb.Domains.Enums;

namespace MRSUTWeb.Domain.Entities.User
{
    public class UserMin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
        public string LasIp { get; set; }
        public URole Level { get; set; }
    }
}