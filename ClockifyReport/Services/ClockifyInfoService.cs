using ClockifyReport.Interfaces;
using ClockifyReport.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClockifyReport.Services
{
    public class ClockifyInfoService: IClockifyInfoService
    {
        readonly HttpClient _httpClient;
        public ClockifyInfoService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        public async Task<User> GetUser()
        {
            string adress = "https://api.clockify.me/api/v1/user";
            using var requestMessage =
            new HttpRequestMessage(HttpMethod.Get, adress);
            requestMessage.Headers.Add("X-Api-Key", "N2I4MmRmNmQtMDE0Mi00ODIzLThjMGEtZjYxMDFlMmIzYzAz");
            return JsonConvert.DeserializeObject<User>(await (await _httpClient.SendAsync(requestMessage))
                .Content.ReadAsStringAsync());
        }
        public async Task<List<TimeEntries>> GetTimeEntries()
        {
            User user =  await GetUser();
            string adress = $"https://api.clockify.me/api/v1/workspaces/{user.ActiveWorkspace}/user/{user.Id}/time-entries";
            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, adress);
            requestMessage.Headers.Add("X-Api-Key", "N2I4MmRmNmQtMDE0Mi00ODIzLThjMGEtZjYxMDFlMmIzYzAz");
            var i = await (await _httpClient.SendAsync(requestMessage)).Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TimeEntries>>(i);
        }
    }
}
