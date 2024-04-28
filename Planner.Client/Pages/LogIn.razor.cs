using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Client.Services;
using Planner.Domain.ViewModel;

namespace Planner.Client.Pages
{
    public partial class LogIn
    {
        [Inject] private UserServices _userServices { get; set; }

        private LogInVM user { get; set; } = new();

        private MudForm _form;

        private LogInValidation LogInValidation { get; set; } = new();

        public Adornment ShowIcon => string.IsNullOrWhiteSpace(user.Password) ? Adornment.None : Adornment.End;

        private string passwordInputIcon = Icons.Material.Filled.VisibilityOff;
        private InputType passwordInput = InputType.Password;
        private bool isShow;
        private void ShowPassword()
        {
            if (isShow)
            {
                isShow = false;
                passwordInputIcon = Icons.Material.Filled.VisibilityOff;
                passwordInput = InputType.Password;
            }
            else
            {
                isShow = true;
                passwordInputIcon = Icons.Material.Filled.Visibility;
                passwordInput = InputType.Text;
            }
        }

        private async void LogInUser()
        {
            await _form.Validate();

            if (_form.IsValid)
            {
                var result = await _userServices.LogIn(user);
                if (result != null)
                {
                  await _localStorageService.SetItemAsync("User", result);
                    _navigationManager.NavigateTo("Home");
                }
            }
        }
    }
}
