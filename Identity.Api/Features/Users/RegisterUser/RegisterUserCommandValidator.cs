using FluentValidation;

namespace Identity.Api.Features.Users.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MinimumLength(5).WithMessage("Full name must be at least 5 characters long.")
                .Must(HaveAtLeastTwoWords).WithMessage("Please enter your full name (at least two words).");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(16).WithMessage("Password must be at least 16 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");
        }

        private bool HaveAtLeastTwoWords(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return false;

            var words = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return words.Length >= 2;
        }
    }
}