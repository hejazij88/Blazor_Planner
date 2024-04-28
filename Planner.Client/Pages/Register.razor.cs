using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Client.Services;
using Planner.Domain.ViewModel;

namespace Planner.Client.Pages
{
    public partial class Register
    {
        [Inject] private UserServices _userServices { get; set; }

        private RegisterVM user = new();

        private MudForm _form;

        private RegisterVMValidation Validation { get; set; } = new();

        private async void RegisterUser()
        {
            await _form.Validate();
            if (_form.IsValid)
            {
                var result = await _userServices.Register(user);
                if (result != null)
                {
                    _snackbar.Add("Success", Severity.Success);
                    _navigationManager.NavigateTo("/");
                }
            }


        }
    }
}
