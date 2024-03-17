using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CargoDomain.Model;

public partial class Station
{
    public int Id { get; set; }


    [Display(Name = "Місто")]
    [Required(ErrorMessage = "поле не повинно бути порожнім")]
    public string CityName { get; set; } = null!;


    [Display(Name = "Назва")]
    [Required(ErrorMessage = "поле не повинно бути порожнім")]
    public string Name { get; set; } = null!;




    [Display(Name = "Телефон")]
    [Required(ErrorMessage = "поле не повинно бути порожнім")]
    [RegularExpression(@"^\+\d{1,3}\(\d{3}\)\d{7}$", ErrorMessage = "Not a valid phone number")]
    public string Phone { get; set; } = null!;



    [Required(ErrorMessage = "поле не повинно бути порожнім")]
    [Display(Name = "Пошта")]
    [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
    public string Email { get; set; } = null!;

    public virtual ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();
}
