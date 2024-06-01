using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MRSUTWeb.BusinessLogic.Core;
using MRSUTWeb.BusinessLogic.DBModel;
using MRSUTWeb.BusinessLogic.Interfaces;
using MRSUTWeb.Domain.Entities.Card;
using MRSUTWeb.Domain.Entities.User;

namespace MRSUTWeb.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            using (var db = new UserContext())
            {
                var user = db.Sessions.FirstOrDefault(u=> u.SessionToken == apiCookieValue);
                
            }    
                return UserCookie(apiCookieValue);
        }
        public UserMinimal GetUserRoleByCookie(int roleValues) 
        { 
            using (var db = new UserContext())
            {
                var user = db.Userr.FirstOrDefault(u=>u.ID_Type_user == roleValues);
            }
            return GetUserByRole(roleValues);
        }
        public ULoginResp UserLogin(ULoginData uLoginData)
        {
            return UserLoginAction(uLoginData);
        }
        public void Insert_RegisterUserAction(URegister register)
        {
            RegisterUserAction(register);
        }
        public void SendEmail_Register(URegister register, string code)
        {
            SendEmail(register, code);
        }
        public HttpCookie GenCookie(string loginCredential)
        {
            return GenerateAuthCookie(loginCredential);
        }
    }
    public class CardBL : CardApi, ICard
    {
        public string GenerateCardNumber_M()
        {
            return GenerateCardNumber();
        }
        public DateTime GenerateExpiryDate_M()
        {
            return GenerateExpiryDate();
        }
        public int GenerateCVV_M()
        {
            return GenerateCVV();
        }
        public void InsertCard_M(CardDbTable card , HttpCookie xKeyCookie)
        {
            InsertCardAction(card, xKeyCookie);
        }
        public UDbTable GetUserFromDatabase(int ID_User)
        {
            return GetUser(ID_User);
        }
        public UserMinimal GetUserByCookie(string cookie)
        {
            return GetUserByCookie(cookie);
        }
    }
}
