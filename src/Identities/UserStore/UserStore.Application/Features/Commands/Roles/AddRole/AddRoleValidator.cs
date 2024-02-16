using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace UserStore.Application.Features.Commands.Roles.AddRole
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters");

            RuleFor(x => x.SelfAccessLevel)
                .InclusiveBetween(0, 7).WithMessage("Self Access Level must be between 0 and 7");

            RuleFor(x => x.OrganizationAccessLevel)
                .InclusiveBetween(0, 7).WithMessage("Organization Access Level must be between 0 and 7");

            RuleFor(x => x.AllAccessLevel)
                .InclusiveBetween(0, 7).WithMessage("All Access Level must be between 0 and 7");
        }
        
    }
}