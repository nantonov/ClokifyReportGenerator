using System;
using System.Globalization;
using ClockifyReport.Interfaces;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClockifyReport.Services
{
    public class ExcelWriterService : IExcelWriterService
    {
        private readonly IClockifyInfoService _service;

        public ExcelWriterService(IClockifyInfoService service)
        {
            _service = service;
        }
        public async Task CreateReport()
        {
            var timeEntries = await _service.GetTimeEntries();
            await using FileStream fs = new($"Report-{DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture)}.xlsx", FileMode.Create);
            var workbook = new XSSFWorkbook();

            var sheet1 = workbook.CreateSheet("Sheet1");

            var headers = sheet1.CreateRow(0);
            headers.CreateCell(0).SetCellValue("Date");
            headers.CreateCell(1).SetCellValue("Description");
            headers.CreateCell(2).SetCellValue("Duration");
            headers.CreateCell(3).SetCellValue("Duration in time");
            headers.CreateCell(5).SetCellValue("Total: ");
            headers.CreateCell(6, CellType.Numeric).SetCellValue(timeEntries.Sum(x => x.Duration.TotalHours));

            int rowIndex = 1;

            foreach (var item in timeEntries)
            {
                var row = sheet1.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(item.Date.ToShortDateString());
                row.CreateCell(1).SetCellValue(item.Description);
                row.CreateCell(2, CellType.Numeric).SetCellValue(item.Duration.TotalHours);
                row.CreateCell(3).SetCellValue(item.Duration.ToString(@"hh\:mm"));
                rowIndex++;
            }

            workbook.Write(fs);

        }
    }
}
