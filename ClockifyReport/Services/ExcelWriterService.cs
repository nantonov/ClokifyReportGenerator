
using ClockifyReport.Interfaces;
using ClockifyReport.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;

using System.Threading.Tasks;

namespace ClockifyReport.Services
{
    public class ExcelWriterService: IExcelWriterService
    {
        private readonly IClockifyInfoService _service;

        public ExcelWriterService(IClockifyInfoService service)
        {
            _service = service;
        }
        public async Task CreateReport()
        {
            List<TimeEntries> timeEntries = await _service.GetTimeEntries();
            using FileStream fs = new("Sample.xlsx", FileMode.Create);
            IWorkbook workbook = new XSSFWorkbook();

            ISheet sheet1 = workbook.CreateSheet("Sheet1");
            IRow headers = sheet1.CreateRow(0);
            headers.CreateCell(0).SetCellValue("Date");
            headers.CreateCell(1).SetCellValue("Description");
            headers.CreateCell(2).SetCellValue("Duration");
            int rowIndex = 1;
            timeEntries.ForEach(item =>
            {
                IRow row = sheet1.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(item.TimeInterval.Start.ToString());
                row.CreateCell(1).SetCellValue(item.Description);
                row.CreateCell(2).SetCellValue(item.TimeInterval.Duration[2..]);
                rowIndex++;
            });
            workbook.Write(fs);

        }
    }
}
