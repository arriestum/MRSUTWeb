using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRSUTWeb.Domain.Entities.User;

namespace MRSUTWeb.BussinesLogic.DBModel
{
    public class SessionContext : DbContext
    {
        public SessionContext() : base("name=MRSUTWeb")
        {
        }

        public virtual DbSet<Session> Sessions { get; set; }
    }
}
