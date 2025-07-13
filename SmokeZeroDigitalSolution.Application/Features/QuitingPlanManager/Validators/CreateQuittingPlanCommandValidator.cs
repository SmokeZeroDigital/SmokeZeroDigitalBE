namespace SmokeZeroDigitalSolution.Application.Features.QuittingPlanManager.Commands;

public class CreateQuittingPlanCommandValidator : AbstractValidator<CreateQuittingPlanCommand>
{
    public CreateQuittingPlanCommandValidator()
    {
        RuleFor(x => x.QuittingPlan.UserId).NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.QuittingPlan.ReasonToQuit)
            .NotEmpty().WithMessage("ReasonToQuit is required.")
            .MaximumLength(500);

        RuleFor(x => x.QuittingPlan.StartDate)
            .LessThan(x => x.QuittingPlan.ExpectedEndDate)
            .WithMessage("StartDate must be earlier than ExpectedEndDate.");

        RuleFor(x => x.QuittingPlan.InitialCigarettesPerDay)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Initial cigarettes per day must be non-negative.");

        RuleFor(x => x.QuittingPlan.InitialCostPerCigarette)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Initial cost must be non-negative.");

        RuleFor(x => x.QuittingPlan.Stages)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.QuittingPlan.Stages));

        RuleFor(x => x.QuittingPlan.CustomNotes)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrWhiteSpace(x.QuittingPlan.CustomNotes));
    }
}
