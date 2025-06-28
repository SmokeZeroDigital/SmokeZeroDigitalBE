namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Validators
{


    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.User.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.User.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

            RuleFor(x => x.User.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(100).WithMessage("Full name must be less than 100 characters.");

            RuleFor(x => x.User.DateOfBirth)
                .LessThan(DateTime.Now).When(x => x.User.DateOfBirth.HasValue)
                .WithMessage("Date of birth must be in the past.");
        }
    }

}
