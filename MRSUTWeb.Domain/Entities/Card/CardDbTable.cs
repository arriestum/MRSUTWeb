using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRSUTWeb.Domain.Entities.User;

namespace MRSUTWeb.Domain.Entities.Card
{
    public class CardDbTable
    {
        [Key , Column(Order =0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_Card { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("Users")]
        public int ID_User { get; set; }
        [Key, Column(Order = 2)]
        public CardTypeDbTable ID_Type { get; set; }
        
        public string CardNumber { get; set; }
        
        public DateTime ExpiryDate { get; set; }
        
        public int CVV { get; set; }
        
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }
        
        public string Name { get; set; }
        
        public string UserSurname { get; set; }
        public virtual UDbTable Users { get; set; }

    }
}
