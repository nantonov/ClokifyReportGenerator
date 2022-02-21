using System.Xml;
using AutoMapper;
using ClockifyReport.Models;
using ClockifyReport.Models.Response;

namespace ClockifyReport.Automapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<TimeEntriesResponse, ReportData>()
                .ForMember(x => x.Date, opt => opt.MapFrom(src => src.TimeInterval.Start))
                .ForMember(x => x.Duration, opt => opt.MapFrom(src => XmlConvert.ToTimeSpan(src.TimeInterval.Duration)));
        }
    }
}
