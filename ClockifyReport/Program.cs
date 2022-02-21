using ClockifyReport.Interfaces;
using ClockifyReport.Models;
using ClockifyReport.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClockifyReport
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var appSettings = config.GetSection("AppSettings").Get<AppSettings>();

            var serviceProvider = new ServiceCollection()
            .AddHttpClient()
            .AddSingleton<IClockifyInfoService, ClockifyInfoService>()
            .AddSingleton<IExcelWriterService, ExcelWriterService>()
            .AddAutoMapper(typeof(Program))
            .AddSingleton(appSettings)
            .BuildServiceProvider();

            serviceProvider.GetService<IExcelWriterService>().CreateReport().Wait();
        }
    }
}
