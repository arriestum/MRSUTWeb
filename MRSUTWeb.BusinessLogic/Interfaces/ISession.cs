using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRSUTWeb.Domain.Entities.User;

namespace MRSUTWeb.BusinessLogic.Interfaces
{
    public interface ISession
    {
        UserLogin UserLogin(ULoginData data);
    }

}
