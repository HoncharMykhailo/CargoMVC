using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CargoDomain.Model;

public partial class Client
{
    public int Id { get; set; }



    [Required(ErrorMessage = "поле не повинно бути порожнім")]
    [Display(Name = "Ім'я")]
    public string Name { get; set; } = null!;




    [Required(ErrorMessage = "You must provide a phone number")]
    [Display(Name = "Phone")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^\+\d{1,3}\(\d{3}\)\d{7}$", ErrorMessage = "Not a valid phone number")]
    public string Phone { get; set; } = null!;





    [Required(ErrorMessage = "поле не повинно бути порожнім")]
    [Display(Name = "Пошта")]
    [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid. format: example@gmail.com ")]
    public string Email { get; set; } = null!;



    public virtual ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();
}
