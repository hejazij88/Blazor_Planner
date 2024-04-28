using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Client.Services;
using Planner_Domain.Model;

namespace Planner.Client.Dialog
{
    public partial class AddToDoDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Inject] private ToDoServices ToDoServices { get; set; }

        [Parameter] public ToDo ToDo { get; set; }

        private ToDoValidation ToDoValidation { get; set; } = new();

        public bool isVisible { get; set; } = new();

        private MudForm form;


        private async Task Save()
        {
            await form.Validate();

            if (form.IsValid)
            {
                isVisible = true;

                var result = await ToDoServices.UpdateToDo(ToDo);

                isVisible = false;
                if (result != null)
                {
                    isVisible = true;
                    MudDialog.Close(DialogResult.Ok(result));
                }
            }
        }

        private void Cancel() => MudDialog.Close(DialogResult.Cancel());
    }
}
