using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Domain.ViewModel
{
    public class LogInVM
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }

    public class LogInValidation : AbstractValidator<LogInVM>
    {
        public LogInValidation()
        {
            RuleFor(vm => vm.UserName).NotEmpty().NotNull();
            RuleFor(vm => vm.Password).NotEmpty().NotNull();
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result =
                await ValidateAsync(ValidationContext<LogInVM>.CreateWithOptions((LogInVM)model,
                    x => x.IncludeProperties(propertyName)));

            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
