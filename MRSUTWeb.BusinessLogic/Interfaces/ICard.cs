using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MRSUTWeb.Domain.Entities.Card;
using MRSUTWeb.Domain.Entities.User;

namespace MRSUTWeb.BusinessLogic.Interfaces
{
    public interface ICard
    {
        string GenerateCardNumber_M();
        DateTime GenerateExpiryDate_M();
        int GenerateCVV_M();
        void InsertCard_M(CardDbTable card , HttpCookie xKeyCookie);
        UDbTable GetUserFromDatabase(int ID_User);
        UserMinimal GetUserByCookie(string token);
        List<CardDbTable> GetUserCards(int userId);
        //.GetUserFromDatabase(ID_User)

    }
}
