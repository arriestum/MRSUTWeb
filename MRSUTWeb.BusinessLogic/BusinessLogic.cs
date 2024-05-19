using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRSUTWeb.BusinessLogic.Interfaces;

namespace MRSUTWeb.BusinessLogic
{
    public class BusinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

        public ISession GetSessionAdminBL()
        {
            return null;
        }
    }
}
