using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CargoInfrastructure.Controllers
//namespace CargoWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly DbcargoContext _context;
        public ChartController(DbcargoContext context)
        {
            _context = context;
        }

        /*
        [HttpGet("StationData")]
        public async Task<IActionResult> GetStationData()
        {
            var stationData = await _context.Cargos
                                .AsNoTracking()
                                .GroupBy(d => d.Station)
                                .Select(group => new object[] { group.Key, group.Count() })
                                .ToListAsync();


            return Ok(stationData);
        }
        */

        [HttpGet("StationData")]
        public async Task<IActionResult> GetStationData()
        {
            var stationData = await _context.Cargos
                                .AsNoTracking()
                                .GroupBy(d => d.Station.Name) // Group by Station.Name
                                .Select(group => new object[] { group.Key, group.Count() })
                                .ToListAsync();

            return Ok(stationData);
        }



        [HttpGet("TruckData")]
        public async Task<IActionResult> GetTruckData()
        {
            var truckData = await _context.Cargos
                                .AsNoTracking()
                                .GroupBy(d => d.Truck.Model)
                                .Select(group => new object[] { group.Key, group.Count() })
                                .ToListAsync();


            return Ok(truckData);
        }

    }
}
