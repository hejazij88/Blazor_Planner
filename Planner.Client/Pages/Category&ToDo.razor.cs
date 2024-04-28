using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Client.Dialog;
using Planner.Client.Services;
using Planner.Domain.Model;
using Planner_Domain.Model;

namespace Planner.Client.Pages
{
    public partial class Category_ToDo
    {
        [Inject] private CategoryServices CategoryServices { get; set; }
        [Inject] private ToDoServices ToDoServices { get; set; }

        [Inject] private IDialogService _dialogService { get; set; }
        private List<Category> Categories { get; set; } = new();
        private List<ToDo> ToDoS { get; set; } = new();
        private string SearchStringCategory { get; set; }
        private string SearchStringToDo { get; set; }

        private Category? selectedCategory;

        private ToDo? selectedToDo;


        private bool QuickFilter(Category category)
        {
            if (string.IsNullOrWhiteSpace(SearchStringCategory))
                return true;


            if (category.Title.Contains(SearchStringCategory, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        private bool QuickFilterToDo(ToDo? toDo)
        {
            if (string.IsNullOrWhiteSpace(SearchStringToDo))
                return true;


            if (toDo.Title.Contains(SearchStringCategory, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        private bool IsSelectedCategory => selectedCategory != null;
        private bool IsSelectedToDo => selectedToDo != null;

        private async void OnRowClickCategory(DataGridRowClickEventArgs<Category> selectedRow)
        {

            if (selectedCategory != null && selectedCategory == selectedRow.Item)
            {
                selectedCategory = null;
                ToDoS.Clear();
            }
            else
            {
                selectedCategory = selectedRow.Item;
                ToDoS = await ToDoServices.LoadToDo(selectedCategory.Id);
            }

            StateHasChanged();
        }

        private string SelectCategoryStyle(Category category, int index)
        {
            if (selectedCategory == null) return string.Empty;

            return category == selectedCategory ? "active-Category" : string.Empty;
        }

        private string SelectToDoStyle(ToDo toDo, int index)
        {
            if (selectedCategory == null) return string.Empty;

            return toDo == selectedToDo ? "active-ToDo" : string.Empty;
        }

        private async void OnRowClickToDo(DataGridRowClickEventArgs<ToDo> selectedRow)
        {
            if (selectedToDo != null && selectedToDo == selectedRow.Item)
                selectedToDo = null;
            else
                selectedToDo = selectedRow.Item;

            StateHasChanged();
        }

        protected override async void OnInitialized()
        {
            var user = await _localStorageService.GetItemAsync<User>("User");
            if (user != null)
            {
                Categories = await CategoryServices.LoadCategory(user.Id);
            }
            else
            {
                _snackbar.Add("Dose Not Exist User", Severity.Error);
            }

            StateHasChanged();
        }

        private async void AddCategory()
        {
            var category = new Category();

            var options = new DialogOptions
                { CloseOnEscapeKey = false, DisableBackdropClick = true, FullWidth = false, MaxWidth = MaxWidth.Small };

            var parameters = new DialogParameters<AddCategoryDialog> { { x => x.Category, category } };

            var dialog = await _dialogService.ShowAsync<AddCategoryDialog>("Add Category", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                Categories.Add((Category)result.Data);
                _snackbar.Add("Category Saved", Severity.Success);
                StateHasChanged();
            }
        }

        private async void EditCategory()
        {
            var options = new DialogOptions
                { CloseOnEscapeKey = false, DisableBackdropClick = true, FullWidth = true, MaxWidth = MaxWidth.Medium };
            var parameter = new DialogParameters<AddCategoryDialog> { { x => x.Category, selectedCategory } };
            var dialog = await _dialogService.ShowAsync<AddCategoryDialog>("Edit Currency", parameter, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                Categories.Remove(selectedCategory);
                Categories.Add((Category)result.Data);
                _snackbar.Add("Currency Saved", Severity.Success);
                StateHasChanged();


            }
        }

        private async void AddToDo()
        {
            var toDo = new ToDo { CategoryId = selectedCategory.Id,Category = selectedCategory};

            var options = new DialogOptions
                { CloseOnEscapeKey = false, DisableBackdropClick = true, FullWidth = false, MaxWidth = MaxWidth.Small };

            var parameters = new DialogParameters<AddToDoDialog> { { x => x.ToDo, toDo } };

            var dialog = await _dialogService.ShowAsync<AddToDoDialog>("Add ToDo", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                ToDoS.Add((ToDo)result.Data);
                _snackbar.Add("ToDo Saved", Severity.Success);
                StateHasChanged();
            }
        }
        private async void EditToDo()
        {
            selectedToDo.Category = selectedCategory;
            var options = new DialogOptions
                { CloseOnEscapeKey = false, DisableBackdropClick = true, FullWidth = true, MaxWidth = MaxWidth.Medium };
            var parameter = new DialogParameters<AddToDoDialog> { { x => x.ToDo, selectedToDo } };
            var dialog = await _dialogService.ShowAsync<AddToDoDialog>("Edit ToDo", parameter, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                ToDoS.Remove(selectedToDo);
                ToDoS.Add((ToDo)result.Data);
                _snackbar.Add("ToDo Saved", Severity.Success);
                StateHasChanged();


            }
        }

        private async void DeleteCategory()
        {
            if (selectedCategory == null) return;

            var parameters = new DialogParameters<DeleteDialog> { { x => x.ContentText, selectedCategory.Title } };
            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await _dialogService.ShowAsync<DeleteDialog>("Delete", parameters, options);
            var dialogResult = await dialog.Result;
            if (!dialogResult.Canceled)
            {
                var result = await CategoryServices.DeleteCategory(selectedCategory);
                if (result != null)
                {
                    Categories.Remove(selectedCategory);
                }
            }
            StateHasChanged();
        }

        private async void DeleteToDo()
        {
            if (selectedToDo == null) return;

            var parameters = new DialogParameters<DeleteDialog> { { x => x.ContentText, selectedToDo.Title } };
            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await _dialogService.ShowAsync<DeleteDialog>("Delete", parameters, options);
            var dialogResult = await dialog.Result;
            if (!dialogResult.Canceled)
            {
                var result = await ToDoServices.DeleteToDo(selectedToDo);
                if (result != null)
                {
                    ToDoS.Remove(selectedToDo);
                }
            }
            StateHasChanged();
        }


    }
}
