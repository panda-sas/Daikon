
using FluentValidation;
namespace Project.Application.Features.Commands.NewProjectCompoundEvolution
{
    public class NewProjectCompoundEvolutionCommandValidator : AbstractValidator<NewProjectCompoundEvolutionCommand>
    {
        public NewProjectCompoundEvolutionCommandValidator()
        {

            RuleFor(t => t.CompoundStructureSMILES)
            .NotEmpty().WithMessage("SMILES is required");

        }
    }
}