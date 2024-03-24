/*

using CargoInfrastructure;
using CargoInfrastructure.Services;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

namespace CargoInfrastructure.Services
{

    public class StationExportService : IExportService<CargoDomain.Model.Station>
    {
        private const string RootWorksheetName = "";

        private static readonly IReadOnlyList<string> HeaderNames =
            new string[]
            {
                "Назва",
                "Автор 1",
                "Автор 2",
                "Автор 3",
                "Автор 4",
                "Категорія",
                "Інформація",
            };
        private readonly DbcargoContext _context;

        private static void WriteHeader(IXLWorksheet worksheet)
        {
            for (int columnIndex = 0; columnIndex < HeaderNames.Count; columnIndex++)
            {
                worksheet.Cell(1, columnIndex + 1).Value = HeaderNames[columnIndex];
            }
            worksheet.Row(1).Style.Font.Bold = true;
        }

        private void WriteBook(IXLWorksheet worksheet, CargoDomain.Model.Cargo cargo, int rowIndex)
        {
            var columnIndex = 1;
            worksheet.Cell(rowIndex, columnIndex++).Value = cargo.Contain;

            var clientscargo = _context.Clients.Where(c => c.Id == cargo.Id)
                                                    .Distinct();
            //book.AuthorBooks.ToList();
            foreach (var client in clientscargo.Take(4))
            {
                //більше 4 авторів писати нікоди, ігноруємо їх
                if (client.Id is not null)
                {
                    worksheet.Cell(rowIndex, columnIndex++).Value = client.Name;
                }
            }
            //  worksheet.Cell(rowIndex, 7).Value = cargo.Info;
        }

        private void WriteBooks(IXLWorksheet worksheet, ICollection<CargoDomain.Model.Cargo> cargoes)
        {
            WriteHeader(worksheet);
            int rowIndex = 2;
            foreach (var book in cargoes)
            {
                WriteBook(worksheet, book, rowIndex);
                rowIndex++;
            }
        }

        private void WriteCategories(XLWorkbook workbook, ICollection<CargoDomain.Model.Station> stations)
        {
            //для усіх категорій формуємо сторінки
            foreach (var st in stations)
            {

                if (st is not null)
                {
                    var worksheet = workbook.Worksheets.Add(st.Name);
                    WriteBooks(worksheet, st.Cargos.ToList());
                }
            }
        }

        public StationExportService(DbcargoContext context)
        {
            _context = context;
        }

        public async Task WriteToAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (!stream.CanWrite)
            {
                throw new ArgumentException("Input stream is not writable");
            }
            //тут для прикладу пишемо усі книги в усіх категоріях, в своїх проєктах потрібно писати лише вибрані категорії та книги
            var stations = await _context.Stations
                .Include(station => station.Cargos)
                .ToListAsync(cancellationToken);

            var workbook = new XLWorkbook();

            WriteCategories(workbook, stations);
            workbook.SaveAs(stream);
        }

    }
    
}

*/