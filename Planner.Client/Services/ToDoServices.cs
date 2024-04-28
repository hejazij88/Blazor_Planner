using Planner_Domain.Model;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using MudBlazor;

namespace Planner.Client.Services
{
    public class ToDoServices
    {
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;

        public ToDoServices(HttpClient httpClient,ISnackbar snackbar)
        {
            _httpClient=httpClient;
            _snackbar=snackbar;
        }



        public async Task<List<ToDo>> LoadToDo(Guid categoryId)
        {
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"api/ToDo/LoadToDo?categoryId={categoryId}");
                var response = await _httpClient.SendAsync(httpRequest);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var list = JsonSerializer.Deserialize<List<ToDo>>(json, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true,
                        IgnoreReadOnlyProperties = true
                    });

                    if (list != null)
                        return list;
                }

                _snackbar.Add(await response.Content.ReadAsStringAsync(), Severity.Error);
                return new List<ToDo>();
            }
            catch (Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                return new List<ToDo>();
            }
        }

        public async Task<List<ToDo>> LoadToDoByUserId(Guid userId)
        {
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"api/ToDo/LoadToDoByUserId?userId={userId}");
                var response = await _httpClient.SendAsync(httpRequest);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var list = JsonSerializer.Deserialize<List<ToDo>>(json, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true,
                        IgnoreReadOnlyProperties = true
                    });

                    if (list != null)
                        return list;
                }

                _snackbar.Add(await response.Content.ReadAsStringAsync(), Severity.Error);
                return new List<ToDo>();
            }
            catch (Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                return new List<ToDo>();
            }
        }


        public async Task<ToDo?> UpdateToDo(ToDo toDo)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/ToDo/UpdateToDo", toDo);

                var result = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var data = JsonSerializer.Deserialize<ToDo>(result, new JsonSerializerOptions
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

        public async Task<ToDo?> DeleteToDo(ToDo toDo)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/ToDo/DeleteToDo", toDo);

                var result = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var data = JsonSerializer.Deserialize<ToDo>(result, new JsonSerializerOptions
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
