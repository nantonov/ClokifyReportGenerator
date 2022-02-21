using ClockifyReport.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using ClockifyReport.Models;
using ClockifyReport.Models.Response;

namespace ClockifyReport.Services
{
    public class ClockifyInfoService : IClockifyInfoService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public ClockifyInfoService(IHttpClientFactory factory, IMapper mapper)
        {
            _mapper = mapper;
            _httpClient = factory.CreateClient();
        }

        public async Task<IReadOnlyCollection<ReportData>> GetTimeEntries()
        {
            var user = await GetUser();

            var address = $"https://api.clockify.me/api/v1/workspaces/{user.ActiveWorkspace}/user/{user.Id}/time-entries";

            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, address);

            requestMessage.Headers.Add("X-Api-Key", "N2I4MmRmNmQtMDE0Mi00ODIzLThjMGEtZjYxMDFlMmIzYzAz");

            var response = await (await _httpClient.SendAsync(requestMessage)).Content.ReadAsStringAsync();
            var deserializedResponse = JsonConvert.DeserializeObject<List<TimeEntriesResponse>>(response);

            var pattern = deserializedResponse.First().TimeInterval.Duration;
            var ts = System.Xml.XmlConvert.ToTimeSpan(pattern).TotalHours;

            return _mapper.Map<IReadOnlyCollection<ReportData>>(deserializedResponse);
        }

        private async Task<User> GetUser()
        {
            var address = "https://api.clockify.me/api/v1/user";
            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, address);

            requestMessage.Headers.Add("X-Api-Key", "N2I4MmRmNmQtMDE0Mi00ODIzLThjMGEtZjYxMDFlMmIzYzAz");

            return JsonConvert.DeserializeObject<User>(await (await _httpClient.SendAsync(requestMessage))
                .Content.ReadAsStringAsync());
        }
    }
}
