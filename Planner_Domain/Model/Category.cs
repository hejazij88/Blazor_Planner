using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Planner.Domain.ViewModel;

namespace Planner_Domain.Model
{
    [Table("Category")]

    public class Category
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        #region Relation
        public Guid? UserId { get; set; }
        [ForeignKey(nameof(UserId))] public virtual User? User { get; set; }

        [InverseProperty(nameof(ToDo.Category))] public virtual List<ToDo> ToDoList { get; set; } = new List<ToDo>();
        #endregion

    }

    public class CategoryValidation : AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(category => category.Title).NotEmpty().NotNull();
            RuleFor(category => category.UserId).NotNull().NotEmpty();
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result =
                await ValidateAsync(ValidationContext<Category>.CreateWithOptions((Category)model,
                    x => x.IncludeProperties(propertyName)));

            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
