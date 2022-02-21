using ClockifyReport.Interfaces;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
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
            await using FileStream fs = new("Sample.xlsx", FileMode.Create);
            IWorkbook workbook = new XSSFWorkbook();

            ISheet sheet1 = workbook.CreateSheet("Sheet1");
            IRow headers = sheet1.CreateRow(0);
            headers.CreateCell(0).SetCellValue("Date");
            headers.CreateCell(1).SetCellValue("Description");
            headers.CreateCell(2).SetCellValue("Duration");
            
            int rowIndex = 1;

            foreach (var item in timeEntries)
            {
                IRow row = sheet1.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(item.Date.ToShortDateString());
                row.CreateCell(1).SetCellValue(item.Description);
                row.CreateCell(2).SetCellValue(item.Duration);
                rowIndex++;
            }

            workbook.Write(fs);

        }
    }
}
