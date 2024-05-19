using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MRSUTWeb.BusinessLogic.Core;
using MRSUTWeb.BusinessLogic.Interfaces;
using MRSUTWeb.Domain.Entities.User;

namespace MRSUTWeb.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public UserMinimal GetUserByCookie (string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }
        public ULoginResp UserLogin (ULoginData uLoginData)
        {
            return UserLoginAction(uLoginData);
        }
        public void Insert_RegisterUserAction(URegister register)
        {
            RegisterUserAction(register);
        }
        public void SendEmail_Register(URegister register)
        {
            SendEmail(register);
        }
        public HttpCookie GenCookie(string loginCredential)
        {
            return GenerateAuthCookie(loginCredential);
        }
    }
}
