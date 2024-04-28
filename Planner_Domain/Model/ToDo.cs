using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Planner_Domain.Model
{
    [Table("ToDo")]

    public class ToDo
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        #region Relation

        public Guid? CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]public virtual Category? Category { get; set; }

        [InverseProperty(nameof(Activity.ToDo))] public virtual List<Activity> Activities { get; set; } = new();

        #endregion
    }

    public class ToDoValidation : AbstractValidator<ToDo>
    {
        public ToDoValidation()
        {
            RuleFor(todo => todo.Title).NotEmpty().NotNull();
            RuleFor(todo => todo.CategoryId).NotEmpty().NotNull();

        }
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result =
                await ValidateAsync(ValidationContext<ToDo>.CreateWithOptions((ToDo)model,
                    x => x.IncludeProperties(propertyName)));

            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
