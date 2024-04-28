using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Client.Dialog;
using Planner.Client.Services;
using Planner.Domain.Model;
using Planner_Domain.Model;

namespace Planner.Client.Pages
{
    public partial class DateList
    {
        [Inject] private DatePlanServices DatePlanServices { get; set; }

        [Inject] private IDialogService _dialogService { get; set; }

        private List<DatePlan> DatePlans { get; set; } = new();

        private DatePlan? SelectedDatePlan { get; set; }
        private string SearchString;

        private bool QuickFilter(DatePlan datePlan)
        {
            if (string.IsNullOrWhiteSpace(SearchString))
                return true;


            if (datePlan.Title.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
        private async void OnRowClick(DataGridRowClickEventArgs<DatePlan> selectedRow)
        {

            if (SelectedDatePlan != null && SelectedDatePlan == selectedRow.Item)
            {
                SelectedDatePlan = null;
            }
            else
            {
                SelectedDatePlan = selectedRow.Item;
            }

            StateHasChanged();
        }

        private string SelectStyle(DatePlan datePlan, int index)
        {
            if (SelectedDatePlan == null) return string.Empty;

            return datePlan == SelectedDatePlan ? "active-Select" : string.Empty;
        }

        private bool IsSelectedCategory => SelectedDatePlan != null;


        private async void AddDatePlan()
        {
            var options = new DialogOptions
            { CloseOnEscapeKey = false, DisableBackdropClick = true, FullWidth = false, MaxWidth = MaxWidth.Small };

            var dialog = await _dialogService.ShowAsync<AddDatePlanDialog>("Add Date Plan", options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                var data =(DatePlan) result.Data;
                DatePlans.Add(data);
                _snackbar.Add("DatePlan Saved", Severity.Success);

                var option = new DialogOptions
                { CloseOnEscapeKey = false, DisableBackdropClick = true, FullWidth = true, MaxWidth = MaxWidth.Small };
                var parameter = new DialogParameters<AddActivityDialog> { { x => x.DatePlan,data} };
                var dialogActivity = await _dialogService.ShowAsync<AddActivityDialog>("Add Activity",parameter, options);
                var resultActivity = await dialog.Result;

                if (!result.Canceled)
                {;
                }
            }

            StateHasChanged();
        }

        private async void EditDatePlan()
        {
            var options = new DialogOptions
                { CloseOnEscapeKey = false, DisableBackdropClick = true, FullWidth = false, MaxWidth = MaxWidth.Small };
            var parameterDatePlan = new DialogParameters<AddDatePlanDialog> { { x => x.DatePlan , SelectedDatePlan }};
            var dialog =
                await _dialogService.ShowAsync<AddDatePlanDialog>("Edit Date Plan", parameterDatePlan, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                var data = (DatePlan)result.Data;
                DatePlans.Remove(SelectedDatePlan);
                DatePlans.Add(data);
                _snackbar.Add("DatePlan Saved", Severity.Success);
                StateHasChanged();

                var option = new DialogOptions
                {
                    CloseOnEscapeKey = false, DisableBackdropClick = true, FullWidth = true, MaxWidth = MaxWidth.Small
                };
                var parameter = new DialogParameters<AddActivityDialog> { { x => x.DatePlan,data } };
                var dialogActivity =
                    await _dialogService.ShowAsync<AddActivityDialog>("Edit Activity", parameter, options);
                var resultActivity = await dialog.Result;

            }
            StateHasChanged();
        }

        private async void DeleteDatePlan()
        {
            if(SelectedDatePlan==null) return;

            var parameters = new DialogParameters<DeleteDialog>{{x=>x.ContentText,SelectedDatePlan.Title}};
            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog=await _dialogService.ShowAsync<DeleteDialog>("Delete", parameters, options);
            var dialogResult=await dialog.Result;
            if (!dialogResult.Canceled)
            {
                var result =await DatePlanServices.DeleteDatePlan(SelectedDatePlan);
                if (result != null)
                {
                    DatePlans.Remove(SelectedDatePlan);
                }
            }
            StateHasChanged();
        }

        protected override async void OnInitialized()
        {
            var user = await _localStorageService.GetItemAsync<User>("User");
            if (user != null)
            {
                DatePlans = await DatePlanServices.LoadDatePlan(user.Id);
            }
            else
            {
                _snackbar.Add("Does Not Exist User", Severity.Error);
            }
            StateHasChanged();
        }
    }
}
