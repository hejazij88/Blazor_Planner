    using MudBlazor;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
    using Planner_Domain.Model;
using Planner.Domain.ViewModel;

    namespace Planner.Client.Services
{
    public class UserServices
    {

        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;

        public UserServices(HttpClient httpClient, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _snackbar = snackbar;
        }

        public async Task<User?> Register(RegisterVM registerVm)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/UserManage/Register", registerVm);

                var result = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var data = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
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

        public async Task<User?> LogIn(LogInVM logInVm)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/UserManage/LogIn", logInVm);

                var result = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var data = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
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
