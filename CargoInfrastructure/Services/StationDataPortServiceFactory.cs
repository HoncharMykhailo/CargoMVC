using CargoInfrastructure;
using CargoInfrastructure.Services;
using CargoDomain.Model;
namespace LibraryInfrastructure.Services
{
    /*
    public class CategoryDataPortServiceFactory
        : IDataPortServiceFactory<Station>
    {
        private readonly DbcargoContext _context;
        public CategoryDataPortServiceFactory(DbcargoContext context)
        {
            _context = context;
        }
        public IImportService<Station> GetImportService(string contentType)
        {
            if (contentType is "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                return new StationImportService(_context);
            }
            throw new NotImplementedException($"No import service implemented for movies with content type {contentType}");
        }
        public IExportService<Station> GetExportService(string contentType)
        {
            if (contentType is "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                return new StationExportService(_context);
            }
            throw new NotImplementedException($"No export service implemented for movies with content type {contentType}");
        }
    }
    */
}
