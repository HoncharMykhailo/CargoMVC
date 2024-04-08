using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CargoDomain.Model;
using CargoInfrastructure;
using Microsoft.AspNetCore.Authorization;
using ClosedXML.Excel;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.ComponentModel;
using System.IO.Pipes;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace CargoInfrastructure.Controllers
{
  //  [Authorize(Roles = "admin,user")]
    public class CargoesController : Controller
    {
        private readonly DbcargoContext _context;
        private readonly UserManager<User> _userManager;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public CargoesController(DbcargoContext context, IWebHostEnvironment hostingEnvironment, UserManager<User> userManager)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        // GET: Cargoes
        public async Task<IActionResult> Index(int? id, string? name)
        {
               var dbcargoContext = _context.Cargos.Include(c => c.Client).Include(c => c.Station).Include(c => c.Truck);
            //   return View(await dbcargoContext.ToListAsync());


            //  if (id == null) return RedirectToAction("Clients", "Index");
            // if (id == null)// return RedirectToAction("Cargoes", "Index");
            if (User.IsInRole("admin")&&id==null)
            {
                return View(await dbcargoContext.ToListAsync());
            }

            if (id != null)
            {
                  ViewBag.ClientId = id;
                  ViewBag.ClientName = name;
                   var cargoByClient = _context.Cargos.Where(c=>c.ClientId==id).Include(c=>c.Client);

                   return View(await cargoByClient.ToListAsync());
            }

            var currentUser = await _userManager.GetUserAsync(User);
            var currentClient = await _context.Clients.FirstOrDefaultAsync(c => c.Email == currentUser.Email);



            var clientCargo = _context.Cargos.Where(
                c => c.ClientId == currentClient.Id)
                .Include(c => c.Client);

            return View(await clientCargo.ToListAsync());



        }

        // GET: Cargoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargos
                .Include(c => c.Client)
                .Include(c => c.Station)
                .Include(c => c.Truck)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        // GET: Cargoes/Create
        public IActionResult Create(int clientId)
        {
            ViewData["StationId"] = new SelectList(_context.Stations, "Id", "Name");
            ViewData["TruckId"] = new SelectList(_context.Trucks, "Id", "Model");

            if (clientId != null && clientId != 0) // if client id determined
            {
                var client = _context.Clients.Where(c => c.Id == clientId).FirstOrDefault();
                ViewBag.ClientId = clientId;
                ViewBag.ClientName = client.Name;
                ViewData["ClientId"] = new SelectList(new List<Client> { client }, "Id", "Name");
            }
            else
            {
                ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name");
            }

            return View();
        }


        // POST: Cargoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int clientId,int truckId, int stationId, [Bind("Id,ClientId,Weight,Volume,Contain,StationId,TruckId")] Cargo cargo)
        {

            cargo.ClientId = clientId;
            Truck truck = _context.Trucks.FirstOrDefault(t => t.Id == truckId);
            Client client = _context.Clients.FirstOrDefault(c => c.Id == clientId);
            Station station = _context.Stations.FirstOrDefault(s => s.Id == stationId);
            cargo.Truck = truck;
            cargo.Client = client;
            cargo.Station = station;
            ModelState.Clear();
           // TryValidateModel(cargo);


            if (ModelState.IsValid)
            {
                _context.Add(cargo);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index","Cargoes",new { id = clientId, name = _context.Clients.Where(c=>c.Id==clientId).FirstOrDefault().Name});
            }


            return RedirectToAction("Index", "Cargoes", new { id = clientId, name = _context.Clients.Where(c => c.Id == clientId).FirstOrDefault().Name });
        }

        // GET: Cargoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Email", cargo.ClientId);
            ViewData["StationId"] = new SelectList(_context.Stations, "Id", "CityName", cargo.StationId);
            ViewData["TruckId"] = new SelectList(_context.Trucks, "Id", "Model", cargo.TruckId);
            return View(cargo);
        }

        // POST: Cargoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,Weight,Volume,Contain,StationId,TruckId")] Cargo cargo)
        {
            if (id != cargo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cargo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CargoExists(cargo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Email", cargo.ClientId);
            ViewData["StationId"] = new SelectList(_context.Stations, "Id", "CityName", cargo.StationId);
            ViewData["TruckId"] = new SelectList(_context.Trucks, "Id", "Model", cargo.TruckId);
            return View(cargo);
        }

        // GET: Cargoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargos
                .Include(c => c.Client)
                .Include(c => c.Station)
                .Include(c => c.Truck)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        // POST: Cargoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo != null)
            {
                _context.Cargos.Remove(cargo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CargoExists(int id)
        {
            return _context.Cargos.Any(e => e.Id == id);
        }







        /*

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please select a file to import.");
            }

            if (Path.GetExtension(file.FileName) != ".xlsx" && Path.GetExtension(file.FileName) != ".xls")
            {
                return BadRequest("Invalid file format. Please select an Excel file.");
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    stream.Position = 0;

                    var cargoList = ExcelHelper.ReadCargoDataFromExcel(stream);





                    _context.Cargos.AddRange(cargoList);
                    await _context.SaveChangesAsync();

                    return Ok($"Successfully imported {cargoList.Count} cargoes from the Excel file.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while importing the Excel file: {ex.Message}");
            }
        }


        */

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please select a file to import.");
            }

            if (Path.GetExtension(file.FileName) != ".xlsx" && Path.GetExtension(file.FileName) != ".xls")
            {
                return BadRequest("Invalid file format. Please select an Excel file.");
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    stream.Position = 0;

                    // var cargoList = ExcelHelper.ReadCargoDataFromExcel(stream);
                    var cargoList = new List<Cargo>();

                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheet(1);
                       

                        foreach (var row in worksheet.RowsUsed().Skip(1)) // Skip header row
                        {
                            string client = row.Cell(4).GetString();
                            string station = row.Cell(5).GetString();
                            string truck = row.Cell(6).GetString();


                            var cargo = new Cargo
                            {

                                Weight = int.Parse(row.Cell(1).GetFormattedString()),
                                Volume = int.Parse(row.Cell(2).GetFormattedString()),
                                Contain = row.Cell(3).GetString(),




                                Client = await _context.Clients.FirstOrDefaultAsync(c => c.Name == client),
                                Station = await _context.Stations.FirstOrDefaultAsync(c => c.Name == station),
                                Truck = await _context.Trucks.FirstOrDefaultAsync(c => c.Model == truck),

                            };



                            cargoList.Add(cargo);
                        }
                    }



                    foreach (var cargo in cargoList) 
                    {
                        cargo.ClientId = cargo.Client.Id;
                        cargo.StationId = cargo.Station.Id;
                        cargo.TruckId = cargo.Truck.Id;
                    }


                    _context.Cargos.AddRange(cargoList);
                    await _context.SaveChangesAsync();

                   //   return Ok($"Successfully imported {cargoList.Count} cargoes from the Excel file.");
                    // return Ok();
                    // return View(cargo);
                    //  return View();
                    return RedirectToAction("Index", "Cargoes");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while importing the Excel file: {ex.Message}");
            }
        }






        // GET: Cargoes/Export
        public IActionResult Export()
        {
            var cargoes = _context.Cargos.Include(c => c.Client).Include(c => c.Station).Include(c => c.Truck).ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Cargoes");

                // Add headers in the first row
                worksheet.Cell(1, 1).Value = "Weight";
                worksheet.Cell(1, 2).Value = "Volume";
                worksheet.Cell(1, 3).Value = "Contain";
                worksheet.Cell(1, 4).Value = "Client";
                worksheet.Cell(1, 5).Value = "Station";
                worksheet.Cell(1, 6).Value = "Truck";

                // Add cargo data
                for (int i = 0; i < cargoes.Count; i++)
                {
                    var cargo = cargoes[i];
                    worksheet.Cell(i + 2, 1).Value = cargo.Weight;
                    worksheet.Cell(i + 2, 2).Value = cargo.Volume;
                    worksheet.Cell(i + 2, 3).Value = cargo.Contain;
                    worksheet.Cell(i + 2, 4).Value = cargo.Client.Name;
                    worksheet.Cell(i + 2, 5).Value = cargo.Station.CityName;
                    worksheet.Cell(i + 2, 6).Value = cargo.Truck.Model;
                }

                // Auto-fit columns
                worksheet.Columns().AdjustToContents();

                // Prepare the response
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Cargoes.xlsx");
                }
            }
        }





    }
}
