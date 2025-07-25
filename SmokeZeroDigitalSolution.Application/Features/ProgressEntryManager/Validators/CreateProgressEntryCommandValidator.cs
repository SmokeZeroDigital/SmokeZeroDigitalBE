namespace SmokeZeroDigitalSolution.Application.Features.ProgressEntryManager.Commands;

public class CreateProgressEntryCommandValidator : AbstractValidator<CreateProgressEntryCommand>
{
    public CreateProgressEntryCommandValidator()
    {
        RuleFor(x => x.Entry.UserId).NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.Entry.EntryDate)
            .NotEmpty().WithMessage("EntryDate is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("EntryDate cannot be in the future.");

        RuleFor(x => x.Entry.CigarettesSmokedToday)
            .GreaterThanOrEqualTo(0).WithMessage("Cigarettes smoked today must be non-negative.");

        RuleFor(x => x.Entry.MoneySavedToday)
            .GreaterThanOrEqualTo(0).WithMessage("Money saved must be non-negative.");

        RuleFor(x => x.Entry.CravingLevel)
            .InclusiveBetween(1, 10).WithMessage("Craving level must be between 1 and 10.");

        RuleFor(x => x.Entry.HealthStatusNotes)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Entry.HealthStatusNotes));

        RuleFor(x => x.Entry.Challenges)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Entry.Challenges));

        RuleFor(x => x.Entry.Successes)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Entry.Successes));
    }
}
