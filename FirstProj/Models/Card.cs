using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FirstProj.Models;

namespace FirstProj.Models
{
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_Card { get; set; }
        [Key]
        [ForeignKey("Users")]
        public int ID_User { get; set; }
        [Key]
        public CardType ID_Type { get; set; }
        [Key]
        public string CardNumber { get; set; }
        [Key]
        public DateTime CardExpDate { get; set; }
        [Key]
        public int CVV { get; set; }
        [Required]
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }


    }
}