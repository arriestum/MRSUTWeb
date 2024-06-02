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
using System.Text.RegularExpressions;

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
        public ActionResult UserCards()
        {
            var xKeyCookie = Request.Cookies["X-KEY"];
            if (xKeyCookie != null)
            {
                var user = _context.GetUserByCookie(xKeyCookie.Value);
                if (user != null)
                {
                    var cardsDb = _context.GetUserCards(user.ID_User);
                    var cards = cardsDb.Select(card => new Card
                    {

                        CardNumber = card.CardNumber,
                        UserName = card.Name,
                        UserSurname = card.UserSurname,
                        Balance = card.Balance,
                        CardExpDate = card.ExpiryDate

                    }).ToList();

                    return PartialView("_UserCardsPartial", cards);
                }
            }

            return PartialView("_UserCardsPartial", new List<Card>());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Transfer(string senderCardNumber, string receiverCardNumber, decimal amount, string senderExpireDate, string senderCVV)
        {
            /*if (string.IsNullOrEmpty(senderExpireDate) || string.IsNullOrEmpty(senderCVV))
            {
                ViewBag.Error = "Data de expirare și CVV-ul sunt obligatorii.";
                return RedirectToAction("TransferNotSucces");
            }


             if (!Regex.IsMatch(senderExpireDate, @"^(0[1-9]|1[0-2])/\d{4}$") || !Regex.IsMatch(senderCVV, @"^\d{3}$"))
            {
                ViewBag.Error = "Data de expirare sau CVV incorecte.";
                return RedirectToAction("TransferNotSucces");
            } */
            try
            {
                _cardApi.TransferBalance(senderCardNumber, receiverCardNumber, amount);
                return RedirectToAction("TransferSucces");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("TransferNotSucces");
            }
        }
        public ActionResult TransferSucces()
        {
            return View();
        }
        public ActionResult TransferNotSucces()
        {
            return View();
        }
    }
}