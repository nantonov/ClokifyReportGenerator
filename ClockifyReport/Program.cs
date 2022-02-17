using ClockifyReport.Interfaces;
using ClockifyReport.Models;
using ClockifyReport.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

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
            .BuildServiceProvider();

            serviceProvider.GetService<IExcelWriterService>().CreateReport().Wait();
        }
    }
}
