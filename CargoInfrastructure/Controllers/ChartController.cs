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
        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var stations = _context.Stations.ToList();
            List<object> statCargo = new List<object>();
            statCargo.Add(new[] { "Станція", "Кількість доставок" });
            foreach(var s in stations)
            {
                statCargo.Add(new object[] { s.Name, s.Cargos.Count() });
            }
            return new JsonResult(statCargo);
        }
    }
}
