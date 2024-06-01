using MRSUTWeb.BusinessLogic;
using MRSUTWeb.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRSUTWeb.Domain.Entities.Card;
using FirstProj.Models;
using MRSUTWeb.BusinessLogic.DBModel;
using MRSUTWeb.BusinessLogic.Core;
using MRSUTWeb.Domain.Entities.User;

namespace FirstProj.Controllers
{
    public class CardController : Controller
    {
        private readonly ICard _context;
        private readonly CardApi _cardApi = new CardApi();

        public CardController()
        {
            var bl = new BusinessLogic();
            _context = bl.GetCardBL();
        }

        public ActionResult RequestCard()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestCard(Card register)
        { 
            var xKeyCookie = Request.Cookies["X-KEY"];
            if (xKeyCookie != null)
            {
                CardDbTable card = new CardDbTable
                {
                    //ID_User = _context.GetUserByCookie(xKeyCookie.Value).ID_User,
                    //CardNumber = _cardApi.GenerateCardNumber(),
                    //ExpiryDate = _cardApi.GenerateExpiryDate(),
                    //CVV = _cardApi.GenerateCVV(),
                    Balance = register.Balance,
                    ID_Type = (CardTypeDbTable)register.ID_Type,
                    //I want to get number for specific card type , 1 for debit card and 2 for credit card


                    //UserName = _context.GetUserFromDatabase(_context.GetUserByCookie(xKeyCookie.Value).ID_User).Username,
                    //UserSurname = _context.GetUserFromDatabase(_context.GetUserByCookie(xKeyCookie.Value).ID_User).Name
                };
                _context.InsertCard_M(card, xKeyCookie);
                //redirect to logged home
                return RedirectToAction("LogedHome", "LogedUserHome");
            }

            return RedirectToAction("LogedHome", "LogedUserHome");
        }
        // function to get user data from database

    }
}