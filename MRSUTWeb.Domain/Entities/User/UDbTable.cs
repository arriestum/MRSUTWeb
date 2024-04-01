using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MRSUTWeb.Domain.Entities.User
{
    public class UDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Loginul nu poate fi mai mare de 30 de caractere.")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(30, MinimumLength = 8 , ErrorMessage = "Parola nu poate fi mai scurta de 10 caractere.")]

        [DataType(DataType.Date)]
        public DateTime LastLogin { get; set; }

        [StringLength(30)]
        public string LastLoginIP { get; set; }

        //public URole Level { get; set; }



    }
}
