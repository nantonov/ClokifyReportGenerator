using System;
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
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;

        public ClockifyInfoService(IHttpClientFactory factory, IMapper mapper, AppSettings appSettings)
        {
            _mapper = mapper;
            _appSettings = appSettings;
            _httpClient = factory.CreateClient();
        }

        public async Task<IReadOnlyCollection<ReportData>> GetTimeEntries()
        {
            var user = await GetUser();

            var today = DateTime.Now;
            var currentMonthStart = new DateTime(today.Year, today.Month, 1);

            var address = $"https://api.clockify.me/api/v1/workspaces/{user.ActiveWorkspace}/user/{user.Id}/time-entries?start={currentMonthStart:yyyy-MM-ddThh:mm:ssZ}&page-size=5000";

            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, address);

            requestMessage.Headers.Add("X-Api-Key", _appSettings.ApiKey);

            var response = await (await _httpClient.SendAsync(requestMessage)).Content.ReadAsStringAsync();
            var deserializedResponse = JsonConvert.DeserializeObject<List<TimeEntriesResponse>>(response);

            return _mapper.Map<IReadOnlyCollection<ReportData>>(deserializedResponse);
        }

        private async Task<User> GetUser()
        {
            var address = "https://api.clockify.me/api/v1/user";
            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, address);

            requestMessage.Headers.Add("X-Api-Key", _appSettings.ApiKey);

            return JsonConvert.DeserializeObject<User>(await (await _httpClient.SendAsync(requestMessage))
                .Content.ReadAsStringAsync());
        }
    }
}
