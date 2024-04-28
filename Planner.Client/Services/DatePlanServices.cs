using MudBlazor;
using Planner_Domain.Model;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using Planner.Domain.Model;

namespace Planner.Client.Services
{
    public class DatePlanServices
    {
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;


        public DatePlanServices(HttpClient httpClient, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _snackbar = snackbar;
        }

        public async Task<List<DatePlan>> LoadDatePlan(Guid userId)
        {
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"api/DatePlan/LoadDatePlan?userId={userId}");
                var response = await _httpClient.SendAsync(httpRequest);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var list = JsonSerializer.Deserialize<List<DatePlan>>(json, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true,
                        IgnoreReadOnlyProperties = true
                    });

                    if (list != null)
                        return list;
                }

                _snackbar.Add(await response.Content.ReadAsStringAsync(), Severity.Error);
                return new List<DatePlan>();
            }
            catch (Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                return new List<DatePlan>();
            }
        }

        public async Task<DatePlan?> UpdateDatePlan(DatePlan datePlan)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/DatePlan/UpdateDatePlan", datePlan,new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true,
                    IgnoreReadOnlyProperties = true
                });

                var result = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var data = JsonSerializer.Deserialize<DatePlan>(result, new JsonSerializerOptions
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


        public async Task<DatePlan?> DeleteDatePlan(DatePlan datePlan)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/DatePlan/DeleteDatePlan", datePlan, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true,
                    IgnoreReadOnlyProperties = true
                });

                var result = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var data = JsonSerializer.Deserialize<DatePlan>(result, new JsonSerializerOptions
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
    }
}
