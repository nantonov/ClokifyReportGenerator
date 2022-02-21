using ClockifyReport.Interfaces;
using ClockifyReport.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ClockifyReport
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddHttpClient()
            .AddSingleton<IClockifyInfoService, ClockifyInfoService>()
            .AddSingleton<IExcelWriterService, ExcelWriterService>()
            .AddAutoMapper(typeof(Program))
            .BuildServiceProvider();

            serviceProvider.GetService<IExcelWriterService>().CreateReport().Wait();
        }
    }
}
