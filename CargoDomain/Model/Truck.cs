using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CargoDomain.Model;

public partial class Truck
{
    public int Id { get; set; }



    [Display(Name = "Модель")]
    [Required(ErrorMessage = "поле не повинно бути порожнім")]
    public string Model { get; set; } = null!;




    [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
    [Display(Name = "Потужність")]
    [Required(ErrorMessage = "поле не повинно бути порожнім")]
    public int Power { get; set; }


    [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
    public int Status { get; set; }

    public virtual ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
}
