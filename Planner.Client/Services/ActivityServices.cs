using MudBlazor;
using Planner.Domain.Model;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using Planner_Domain.Model;
using Planner.Domain.ViewModel;

namespace Planner.Client.Services
{
    public class ActivityServices
    {
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;


        public ActivityServices(HttpClient httpClient, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _snackbar = snackbar;
        }

        public async Task<List<Activity>> LoadActivityByDateId(Guid DatePlanId)
        {
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"api/Activity/LoadActivityByDateId?DatePlanId={DatePlanId}");
                var response = await _httpClient.SendAsync(httpRequest);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var list = JsonSerializer.Deserialize<List<Activity>>(json, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true,
                        IgnoreReadOnlyProperties = true
                    });

                    if (list != null)
                        return list;
                }

                _snackbar.Add(await response.Content.ReadAsStringAsync(), Severity.Error);
                return new List<Activity>();
            }
            catch (Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                return new List<Activity>();
            }
        }

        public async Task<List<Activity>?> UpdateActivity(List<Activity> activities)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Activity/UpdateActivity", activities, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true,
                    IgnoreReadOnlyProperties = true
                });

                var result = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var data = JsonSerializer.Deserialize<List<Activity>>(result, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true,
                        IgnoreReadOnlyProperties = true
                    });

                    if (data != null)
                        return data;
                }
                _snackbar.Add(result, Severity.Error);
                return null;
            }
            catch (Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                return null;
            }
        }

        public async Task<List<Activity>> DayActivity(Guid userId)
        {
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"api/Activity/DayActivity?userId={userId}");
                var response = await _httpClient.SendAsync(httpRequest);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var list = JsonSerializer.Deserialize<List<Activity>>(json, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true,
                        IgnoreReadOnlyProperties = true
                    });

                    if (list != null)
                        return list;
                }

                _snackbar.Add(await response.Content.ReadAsStringAsync(), Severity.Error);
                return new List<Activity>();
            }
            catch (Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                return new List<Activity>();
            }
        }
        public async Task<List<ChartVM>> MountActivity(Guid userId)
        {
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"api/Activity/MountActivity?userId={userId}");
                var response = await _httpClient.SendAsync(httpRequest);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var list = JsonSerializer.Deserialize<List<ChartVM>>(json, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true,
                        IgnoreReadOnlyProperties = true
                    });

                    if (list != null)
                        return list;
                }

                _snackbar.Add(await response.Content.ReadAsStringAsync(), Severity.Error);
                return new List<ChartVM>();
            }
            catch (Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                return new List<ChartVM>();
            }
        }

        public async Task<List<ChartVM>> MountNotActivity(Guid userId)
        {
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"api/Activity/MountNotActivity?userId={userId}");
                var response = await _httpClient.SendAsync(httpRequest);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var list = JsonSerializer.Deserialize<List<ChartVM>>(json, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true,
                        IgnoreReadOnlyProperties = true
                    });

                    if (list != null)
                        return list;
                }

                _snackbar.Add(await response.Content.ReadAsStringAsync(), Severity.Error);
                return new List<ChartVM>();
            }
            catch (Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                return new List<ChartVM>();
            }
        }

        public async Task<List<ChartVM>> AllTimeActivity(Guid userId)
        {
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"api/Activity/AllTimeActivity?userId={userId}");
                var response = await _httpClient.SendAsync(httpRequest);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var list = JsonSerializer.Deserialize<List<ChartVM>>(json, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true,
                        IgnoreReadOnlyProperties = true
                    });

                    if (list != null)
                        return list;
                }

                _snackbar.Add(await response.Content.ReadAsStringAsync(), Severity.Error);
                return new List<ChartVM>();
            }
            catch (Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                return new List<ChartVM>();
            }
        }



    }
}
