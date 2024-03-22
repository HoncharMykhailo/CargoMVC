using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CargoDomain.Model;
using CargoInfrastructure;
using System.Runtime.Intrinsics.Arm;

namespace CargoInfrastructure.Controllers
{
    public class DriversController : Controller
    {
        private readonly DbcargoContext _context;

        public DriversController(DbcargoContext context)
        {
            _context = context;
        }

        // GET: Drivers
        public async Task<IActionResult> Index(int? id, string? model)
        {
            //var dbcargoContext = _context.Drivers.Include(d => d.Truck);
           // return View(await dbcargoContext.ToListAsync());

            if(id==null) //return RedirectToAction("Trucks","Index");
            {
                var dbcargoContext = _context.Drivers.Include(d => d.Truck);
                return View(await dbcargoContext.ToListAsync());
            }

            ViewBag.TruckId = id;
            ViewBag.TruckModel = model;
            var driverByTruck = _context.Drivers.Where(d => d.TruckId == id).Include(d => d.Truck);
            return View(await driverByTruck.ToListAsync());

        }

        // GET: Drivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .Include(d => d.Truck)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // GET: Drivers/Create
        public IActionResult Create(int truckId)
        {


            ViewData["TruckId"] = new SelectList(_context.Trucks, "Id", "Model");



            if (truckId != null && truckId != 0)
            {


                if (truckId != null && truckId != 0) // if truck id determined
                {
                    var truck = _context.Trucks.Where(c => c.Id == truckId).FirstOrDefault();
                    ViewBag.TruckId = truckId;
                    ViewBag.TruckModel = truck.Model;
                    ViewData["TruckId"] = new SelectList(new List<Truck> { truck }, "Id", "Model");
                }
                else
                {
                    ViewData["TruckId"] = new SelectList(_context.Trucks, "Id", "Model");
                }
               
            }
            return View();
        }


        // POST: Drivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int truckId,[Bind("Id,Name,Phone,Email,TruckId")] Driver driver)
        {
            //driver.TruckId = truckId;

            Truck truck = _context.Trucks.FirstOrDefault(t => t.Id == driver.TruckId);
            driver.Truck = truck;
            ModelState.Clear();
            TryValidateModel(driver);


            if (ModelState.IsValid)
            {
                _context.Add(driver);
                await _context.SaveChangesAsync();
              // return RedirectToAction(nameof(Index));
              return RedirectToAction("Index", "Drivers", new {id = truckId,model = _context.Trucks.Where(t => t.Id == truckId).FirstOrDefault().Model });
            }
            // ViewData["TruckId"] = new SelectList(_context.Trucks, "Id", "Model", driver.TruckId);
            // return View(driver);
            return RedirectToAction("Index", "Drivers", new { id = truckId, model = _context.Trucks.Where(t => t.Id == truckId).FirstOrDefault().Model });
        }

        // GET: Drivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            ViewData["TruckId"] = new SelectList(_context.Trucks, "Id", "Model", driver.TruckId);
            return View(driver);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,Email,TruckId")] Driver driver)
        {
            if (id != driver.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverExists(driver.Id))
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
            ViewData["TruckId"] = new SelectList(_context.Trucks, "Id", "Model", driver.TruckId);
            return View(driver);
        }

        // GET: Drivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .Include(d => d.Truck)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver != null)
            {
                _context.Drivers.Remove(driver);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverExists(int id)
        {
            return _context.Drivers.Any(e => e.Id == id);
        }
    }
}
