using FluentValidation;
using SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands;
namespace SmokeZeroDigitalSolution.Application.Features.UsersManager.Validators
{


    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(100).WithMessage("Full name must be less than 100 characters.");

            RuleFor(x => x.Gender)
                .InclusiveBetween(0, 2).WithMessage("Gender must be between 0 and 2.");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Now).When(x => x.DateOfBirth.HasValue)
                .WithMessage("Date of birth must be in the past.");
        }
    }

}
