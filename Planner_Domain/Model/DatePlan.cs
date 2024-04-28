using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Planner_Domain.Model;

namespace Planner.Domain.Model
{
    [Table("DatePlan")]
    public class DatePlan
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime? DateTime { get; set; }
        public Guid? UserId { get; set; }
        [ForeignKey(nameof(UserId))]public virtual User? User { get; set; }

        [InverseProperty(nameof(Activity.DatePlan))]public virtual List<Activity> Activities { get; set; }=new List<Activity>();
    }

    public class DatePlanValidation : AbstractValidator<DatePlan>
    {
        public DatePlanValidation()
        {
            RuleFor(plan => plan.Title).NotNull().NotEmpty();
            RuleFor(plan => plan.DateTime).NotNull().NotEmpty();

        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result =
                await ValidateAsync(ValidationContext<DatePlan>.CreateWithOptions((DatePlan)model,
                    x => x.IncludeProperties(propertyName)));

            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
