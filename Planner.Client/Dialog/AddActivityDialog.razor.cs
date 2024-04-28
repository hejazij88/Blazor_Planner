using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Client.Services;
using Planner.Domain.Model;
using Planner_Domain.Model;

namespace Planner.Client.Dialog
{
    public partial class AddActivityDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }


        [Inject] private ActivityServices ActivityServices { get; set; }

        [Inject] private ToDoServices ToDoServices { get; set; }

        public List<ToDo> ToDoS { get; set; } = new();

        [Parameter] public DatePlan DatePlan { get; set; } = new();

        private List<Activity> ActivityList { get; set; } = new();


        public bool isVisible { get; set; } = new();

        private MudForm form;

        protected override async void OnInitialized()
        {
            var user = await _localStorageService.GetItemAsync<User>("User");
            if (user != null)
            {
                ToDoS = await ToDoServices.LoadToDoByUserId(user.Id);
            }
            else
            {
                _snackbar.Add("User Does Not Exist", Severity.Error);
            }




            ActivityList = DatePlan.Activities;
            StateHasChanged();
        }
           
        private async Task<IEnumerable<ToDo>> SearchToDo(string? value)
        {

            var list = ToDoS;

            if (!string.IsNullOrEmpty(value))
                list = ToDoS.Where(p => p.Title.ToLower().Contains(value.ToLower())).ToList();


            return list;
        }
        private async Task Save()
        {
            isVisible = true;
            var result = await ActivityServices.UpdateActivity(ActivityList);
            isVisible = false;
            if (result != null)
            {
                isVisible = true;
                MudDialog.Close(DialogResult.Ok(result));
            }
        }

        private void AddAutoCumplite()
        {

            ActivityList.Add(new Activity { DateId = DatePlan.Id,DatePlan = DatePlan});
            StateHasChanged();
        }

        private void RemovePlan(Activity activity)
        {
            if (activity == null) return;

            ActivityList.Remove(activity);
            StateHasChanged();
        }

        private void Cancel() => MudDialog.Close(DialogResult.Cancel());
    }
}
