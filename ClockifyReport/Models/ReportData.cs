using System;

namespace ClockifyReport.Models
{
    public class ReportData
    {
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
