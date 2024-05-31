using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRSUTWeb.Helpers
{
    public class GenerateDataCard
    {
        public string GenerateCardNumber()
        {
            Random random = new Random();
            string cardNumber = "";
            for (int i = 0; i < 16; i++)
            {
                cardNumber += random.Next(0, 9).ToString();
            }
            return cardNumber;
        }
        public string GenerateExpiryDate()
        {
            DateTime now = DateTime.Now;
            DateTime expiryDate = now.AddYears(4);
            string month = expiryDate.Month.ToString();
            string year = expiryDate.Year.ToString();
            return month + "/" + year;
        }
        public int GenerateCVV()
        {
            Random random = new Random();
            return random.Next(100, 999);
        }
    }
}
