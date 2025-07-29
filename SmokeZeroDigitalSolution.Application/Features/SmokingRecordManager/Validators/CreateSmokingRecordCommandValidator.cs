using SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Commands;

namespace SmokeZeroDigitalSolution.Application.Features.SmokingRecordManager.Validators
{
    public class CreateSmokingRecordCommandValidator : AbstractValidator<CreateSmokingRecordCommand>
    {
        public CreateSmokingRecordCommandValidator()
        {
            RuleFor(x => x.Record.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.Record.CigarettesSmoked)
                .GreaterThanOrEqualTo(0).WithMessage("CigarettesSmoked must be >= 0.");

            RuleFor(x => x.Record.CostIncurred)
                .GreaterThanOrEqualTo(0).WithMessage("CostIncurred must be >= 0.");

            RuleFor(x => x.Record.RecordDate)
                .NotEmpty().WithMessage("RecordDate is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("RecordDate cannot be in the future.");

            RuleFor(x => x.Record.Notes)
                .MaximumLength(1000).WithMessage("Notes must be at most 1000 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Record.Notes));
        }
    }
}
