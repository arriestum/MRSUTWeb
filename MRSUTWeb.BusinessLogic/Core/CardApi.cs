using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRSUTWeb.Domain.Entities.Card;
using MRSUTWeb.Helpers;
using MRSUTWeb.BusinessLogic.DBModel;
using MRSUTWeb.BusinessLogic.DBModel.Seed;
using System.ComponentModel.DataAnnotations;
using MRSUTWeb.Domain.Entities.User;
using AutoMapper;
using System.Web;

namespace MRSUTWeb.BusinessLogic.Core
{
    public class CardApi
    {
        public string GenerateCardNumber()
        {
            GenerateDataCard generateDataCard = new GenerateDataCard();
            return generateDataCard.GenerateCardNumber();
        }

        public DateTime GenerateExpiryDate()
        {
            GenerateDataCard generateDataCard = new GenerateDataCard();
            return DateTime.Parse(generateDataCard.GenerateExpiryDate());
        }

        public int GenerateCVV()
        {
            GenerateDataCard generateDataCard = new GenerateDataCard();
            return generateDataCard.GenerateCVV();

        }
        ////////////////
        internal void InsertCardAction(CardDbTable card, HttpCookie xKeyCookie)
        {
            //get id_user from cookie
            var user = UserCookie(xKeyCookie.Value);
            card.ID_User = user.ID_User;
            card.Name = user.Name;
            card.UserSurname = user.Surname;
            card.ID_Type = card.ID_Type;
            var newCard = new CardDbTable
            {

                ID_Type = card.ID_Type,
                ID_User = card.ID_User,
                CardNumber = GenerateCardNumber(),
                ExpiryDate = GenerateExpiryDate(),
                CVV = GenerateCVV(),
                Balance = card.Balance,
                Name = card.Name,
                UserSurname = card.UserSurname
            };
            using (var db = new UserContext())
            {
                db.CardDb.Add(newCard);
                db.SaveChanges();
            }

        }
        ///////////////
        public UDbTable GetUser(int ID_User)
        {
            using (var db = new UserContext())
            {
                return db.Userr.Find(ID_User);
            }
        }
        public UserMinimal GetUserByCookie(string token)
        {
            using (var db = new UserContext())
            {
                var user = db.Sessions.FirstOrDefault(u => u.SessionToken == token);
                return UserCookie(token);
            }

        }
        internal UserMinimal UserCookie(string cookie)
        {
            Session session;
            UDbTable currentUser;

            using (var db = new UserContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.SessionToken == cookie && s.ExpiryDateTime > DateTime.Now);
            }

            if (session == null) return null;

            using (var db = new UserContext())
            {
                currentUser = db.Userr.FirstOrDefault(u => u.ID_User == session.ID_User);
            }

            if (currentUser == null) return null;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<UDbTable, UserMinimal>());
            var mapper = config.CreateMapper();
            var userminimal = mapper.Map<UserMinimal>(currentUser);

            return userminimal;
        }
        public List<CardDbTable> GetUserCards(int userId)
        {
            using (var db = new UserContext())
            {
                return db.CardDb.Where(c => c.ID_User == userId).ToList();
            }
        }
        public void TransferBalance(string senderCardNumber, string receiverCardNumber, decimal amount)
        {
            using (var db = new UserContext())
            {
                var senderCard = db.CardDb.FirstOrDefault(c => c.CardNumber == senderCardNumber);
                var receiverCard = db.CardDb.FirstOrDefault(c => c.CardNumber == receiverCardNumber);

                if (senderCard == null || receiverCard == null)
                {
                    throw new Exception("One or both cards not found");
                }

                if (senderCard.Balance < amount)
                {
                    throw new Exception("Insufficient funds");
                }

                senderCard.Balance -= amount;
                receiverCard.Balance += amount;

                db.SaveChanges();
            }
        }

    }
}
