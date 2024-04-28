using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using MudBlazor;
using Planner_Domain.Model;
using Planner.Domain.ViewModel;
using System.Net.Http.Json;

namespace Planner.Client.Services
{
    public class CategoryServices
    {
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;


        public CategoryServices(HttpClient httpClient,ISnackbar snackbar)
        {
            _httpClient=httpClient;
            _snackbar=snackbar;
        }


        public async Task<List<Category>> LoadCategory(Guid userId)
        {
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"api/Category/LoadCategory?userId={userId}");
                var response = await _httpClient.SendAsync(httpRequest);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var list = JsonSerializer.Deserialize<List<Category>>(json, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true,
                        IgnoreReadOnlyProperties = true
                    });

                    if (list != null)
                        return list;
                }

                _snackbar.Add(await response.Content.ReadAsStringAsync(), Severity.Error);
                return new List<Category>();
            }
            catch (Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                return new List<Category>();
            }
        }

        public async Task<Category?> UpdateCategory(Category category)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Category/UpdateCategory", category);

                var result = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var data = JsonSerializer.Deserialize<Category>(result, new JsonSerializerOptions
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
        public async Task<Category?> DeleteCategory(Category category)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Category/DeleteCategory", category);

                var result = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var data = JsonSerializer.Deserialize<Category>(result, new JsonSerializerOptions
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
