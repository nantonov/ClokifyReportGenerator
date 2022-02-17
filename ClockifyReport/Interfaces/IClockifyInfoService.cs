using ClockifyReport.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClockifyReport.Interfaces
{
    public interface IClockifyInfoService
    {
        Task<User> GetUser();
        Task<List<TimeEntries>> GetTimeEntries();
    }
}
