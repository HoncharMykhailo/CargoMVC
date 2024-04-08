using CargoDomain.Model;
using System.ComponentModel.DataAnnotations;

namespace CargoInfrastructure.ViewModel
{
    public class RegisterViewModel
    {



        [Required(ErrorMessage = "поле не повинно бути порожнім")]
        [Display(Name = "Ім'я")]
        public string Name { get; set; } = null!;



        [Required(ErrorMessage = "поле не повинно бути порожнім")]
        [Display(Name = "Пошта")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid. Format: example@server.domain")]
        public string Email { get; set; } = null!;



        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+\d{1,3}\(\d{3}\)\d{7}$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; } = null!;




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
