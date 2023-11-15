using System.ComponentModel.DataAnnotations;

namespace RetoProgramacionApptelink.Models
{
    public class LoginUser
    {

        [Required(ErrorMessage ="El Nombre de Usuario es obligatorio")]
        [Display(Name = "Nombre de Usuario")]
        public string username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [Display(Name = "Contraseña")]
        public string password { get; set; }

        public int IntentosFallidos { get; set; }
        public LoginUser()
        {
            IntentosFallidos = 0;
        }
    }
}
