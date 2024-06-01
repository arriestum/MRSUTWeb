using System.ComponentModel.DataAnnotations;

namespace MRSUTWeb.Web.Models
{
    public class UserLogin
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}