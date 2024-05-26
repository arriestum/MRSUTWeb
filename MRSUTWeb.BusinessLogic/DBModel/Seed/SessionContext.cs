using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MRSUTWeb.Domain.Entities.User;

namespace MRSUTWeb.BusinessLogic.DBModel.Seed
{
    public class SessionContext : DbContext
    {
        public SessionContext() : base("name=MRSUTWeb")
        {
            Database.SetInitializer<SessionContext>(new CreateDatabaseIfNotExists<SessionContext>());
        }
        public virtual DbSet<Session> Sessions { get; set; }
    }
}
