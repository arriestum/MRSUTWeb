using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MRSUTWeb.Domain.Enums;

namespace MRSUTWeb.Domain.Entities.User
{
    public class UDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_User { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Loginul nu poate fi mai mare de 30 de caractere.")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Parola nu poate fi mai scurta de 10 caractere.")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Last Login")]
        public DateTime LastLogin { get; set; }

        [StringLength(30)]
        public string LastLoginIP { get; set; }

        public URoles Level { get; set; }
    }
}
