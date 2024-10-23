using FluentValidation;
using IdentityServer.Application.DTOs;

namespace IdentityServer.Application.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(x => x.Username)
           .NotEmpty().WithMessage(ValidationMessages.IsRequired<RegisterUserDto>(x => x.Username))
           .MinimumLength(11).WithMessage(ValidationMessages.MustBeGreaterThan<RegisterUserDto>(x => x.Username, 11))
           .Matches("^[0-9]")
           .WithMessage("Username can only contain numbers.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidationMessages.IsRequired<RegisterUserDto>(x => x.Email))
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ValidationMessages.IsRequired<RegisterUserDto>(x => x.Password))
            .MinimumLength(8).WithMessage(ValidationMessages.MustBeGreaterThan<RegisterUserDto>(x => x.Password, 8))
            .MaximumLength(64).WithMessage("Password must not exceed 64 characters.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.PhoneNumber)
            .Matches("^\\+\\d{10,15}$")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage("Phone number must be in the format +<country-code><number>.");
    }
}
