using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRSUTWeb.Domain.Entities.Card;
using MRSUTWeb.Domain.Entities.User;


namespace MRSUTWeb.BusinessLogic.DBModel
{
    public class UserContext : DbContext   
    {
        public UserContext() : base("name=MRSUTWeb")//string conectare la baza de date in web.config
        {
        }

        public virtual DbSet<UDbTable> Userr { get; set; }
        public virtual DbSet<CardDbTable> CardDb { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
    }
}
