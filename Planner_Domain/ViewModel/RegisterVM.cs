using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Planner.Domain.ViewModel
{
    public class RegisterVM
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Respassword { get; set; }
    }

    public class RegisterVMValidation : AbstractValidator<RegisterVM>
    {

        public RegisterVMValidation()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.Password).NotNull().NotEmpty();
            RuleFor(x => x.Respassword).NotNull().NotEmpty();
            RuleFor(customer => customer.Respassword)
                .Equal(customer => customer.Password).WithMessage("Must Be Password Same RePassword");

        }


        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result =
                await ValidateAsync(ValidationContext<RegisterVM>.CreateWithOptions((RegisterVM)model,
                    x => x.IncludeProperties(propertyName)));

            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };

    }



}
