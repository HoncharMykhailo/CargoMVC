using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClosedXML.Excel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Bibliography;

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


public class ExcelHelper
{
    public static List<Cargo> ReadCargoDataFromExcel(Stream fileStream)
    {
        using (var workbook = new XLWorkbook(fileStream))
        {
            var worksheet = workbook.Worksheet(1);
            var cargoList = new List<Cargo>();

            foreach (var row in worksheet.RowsUsed().Skip(1)) // Skip header row
            {
                var cargo = new Cargo
                {
                    
                    Weight = int.Parse(row.Cell(1).GetFormattedString()),
                    Volume = int.Parse(row.Cell(2).GetFormattedString()),
                    Contain = row.Cell(3).GetString(),
                    ClientId = int.Parse(row.Cell(4).GetFormattedString()),
                    StationId = int.Parse(row.Cell(5).GetFormattedString()),
                    TruckId = int.Parse(row.Cell(6).GetFormattedString())


                // Parse other columns as needed and assign them to the Cargo object
            };



                cargoList.Add(cargo);
            }
            return cargoList;
        }
    }
}