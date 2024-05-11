using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRSUTWeb.BusinessLogic.Core;
using MRSUTWeb.BusinessLogic.Interfaces;
using MRSUTWeb.Domain.Entities.User;

namespace MRSUTWeb.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public UserLogin UserLogin(ULoginData data)
        {
            return UserLogin(data);
        }
        public void Insert_RegisterUserAction(URegister register)
        {
            RegisterUserAction(register);
        }
        public void SendEmail_Register(URegister register)
        {
            SendEmail(register);
        }
    }
}
