using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CargoDomain.Model;

public partial class Cargo:Entity
{

    public int Id { get; set; }




    [Required(ErrorMessage = "поле не повинно бути порожнім")]
    [Display(Name = "Клієнт")]
    public int ClientId { get; set; }






    [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
    [Required(ErrorMessage ="поле не повинно бути порожнім")]
    [Display(Name = "Маса")]
    public int Weight { get; set; }




    [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
    [Required(ErrorMessage = "поле не повинно бути порожнім")]
    [Display(Name = "Об'єм")]
    public int Volume { get; set; }




    [Required(ErrorMessage = "поле не повинно бути порожнім")]
    [Display(Name = "Вміст")]
    public string Contain { get; set; } = null!;

    public int StationId { get; set; }

    public int TruckId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Station Station { get; set; } = null!;

    public virtual Truck Truck { get; set; } = null!;
}
