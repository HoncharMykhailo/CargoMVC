

/*
using CargoDomain.Model;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using CargoInfrastructure.Services;
using LibraryInfrastructure.Services;

namespace CargoInfrastructure.Services
{
    
    public class StationImportService : IImportService<Station>
    {
        private readonly DbcargoContext _context;

        public StationImportService(DbcargoContext context)
        {
            _context = context;
        }

        public async Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken)
        {
            if(!stream.CanRead)
            {
                throw new ArgumentException("дане не можуть бути прочитані", nameof (stream));
            }

            using(XLWorkbook workbook = new XLWorkbook(stream, XLEventTracking.Disabled)) 
            {
                foreach(IXLWorksheet worksheet in workbook.Worksheets)
                {
                    var statName = worksheet.Name;
                    var station = await _context.Stations.FirstOrDefault(st => st.Name == statName, cancellationToken);
                    if(station == null) 
                    {
                        station = new Station();
                        station.Name = statName;
                        station.CityName = "Excel";
                        station.Phone = "+1(111)9999999";
                        station.Email = "excelStaion@gmail.com";

                        _context.Stations.Add(station);
                    }

                    foreach(var row in worksheet.RowsUsed().Skip(1))
                    {
                        await AddCargoAsync(row, cancellationToken, station);
                    }
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task AddCargoAsync(IXLRow row, CancellationToken cancellationToken, Station station)
        {
            Cargo cargo = new Cargo();
            cargo.Contain = GetCargoContain(row);
            cargo.Station = station;
            _context.Stations.Add(station);
            await GetClientsAsync(row, cargo, cancellationToken);
        }


        private static string GetCargoContain(IXLRow row)
        {
            return row.Cell(1).Value.ToString();
        }


        private static string GetCargoContains(IXLRow row)
        {
            return row.Cell(6).Value.ToString();
        }

        

        private async Task GetClientsAsync(IXLRow row, Cargo cargo, CancellationToken cancellationToken)
        {
            //у разі наявності автора знайти його, у разі відсутності - додати
            for (int i = 2; i <= 5; i++)
            {
                //чи є запис про автора
                if (row.Cell(i).Value.ToString().Length > 0)
                {
                    var autName = row.Cell(i).Value.ToString();
                    //перевірка чи є такий автор в базі
                    var client = await _context.Clients.FirstOrDefaultAsync(aut => aut.Name == autName, cancellationToken);
                    if (client is null)
                    {
                        client = new Author();
                        client.Name = autName;
                        client.Info = "from EXCEL";
                        _context.Clients.Add(client);
                    }
                }
            }
        }

        



    }

    
}




*/