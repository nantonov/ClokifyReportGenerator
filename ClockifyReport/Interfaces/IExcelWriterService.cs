using System.Threading.Tasks;

namespace ClockifyReport.Interfaces
{
    public interface IExcelWriterService
    { 
        Task CreateReport();
    }
}
