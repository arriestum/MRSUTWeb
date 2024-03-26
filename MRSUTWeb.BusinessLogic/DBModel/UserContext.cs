using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRSUTWeb.BusinessLogic.DBModel
{
    class UserContext : DbContext
    {
        public UserContext() : base("name=UserContext")//string conectare la baza de date in web.config
        {
        }

        public DbSet<UDbTable> Users { get; set; }
    }
}
