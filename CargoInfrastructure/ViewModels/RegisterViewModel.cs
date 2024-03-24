using CargoDomain.Model;
using System.ComponentModel.DataAnnotations;

namespace CargoInfrastructure.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "поле не повинно бути порожнім")]
        [Display(Name = "Пошта")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; } = null!;




        [Required(ErrorMessage = "поле не повинно бути порожнім")]
        [Display(Name = "Рік народження")]
        public int Year { get; set; }


        [Required(ErrorMessage = "поле не повинно бути порожнім")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }


        [Required(ErrorMessage = "поле не повинно бути порожнім")]
        [Compare("Password", ErrorMessage ="Паролі не співпадають")]
        [Display(Name = "Підтвердження пароля")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }



    }
}
