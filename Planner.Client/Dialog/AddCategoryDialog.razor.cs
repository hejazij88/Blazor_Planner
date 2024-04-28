using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Client.Services;
using Planner_Domain.Model;

namespace Planner.Client.Dialog
{
    public partial class AddCategoryDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Inject] private CategoryServices CategoryServices { get; set; }

        [Parameter] public Category Category { get; set; } = new();

        private CategoryValidation CategoryValidation { get; set; } = new();

        public bool isVisible { get; set; } = new();

        private MudForm form;


        private async Task Save()
        {
            await form.Validate();

            if (form.IsValid)
            {
                isVisible = true;
                var user = await _localStorageService.GetItemAsync<User>("User");
                Category.UserId = user.Id;
                var result = await CategoryServices.UpdateCategory(Category);

                isVisible = false;
                if (Category != null)
                {
                    isVisible = true;
                    MudDialog.Close(DialogResult.Ok(result));
                }
            }
        }

        private void Cancel() => MudDialog.Close(DialogResult.Cancel());
    }
}
