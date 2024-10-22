using FluentValidation;
using IdentityServer.Application.DTOs;

namespace IdentityServer.Application.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(x => x.Username)
           .NotEmpty().WithMessage(ValidationMessages.IsRequired<RegisterUserDto>(x => x.Username))
           .MinimumLength(6).WithMessage(ValidationMessages.MustBeGreaterThan<RegisterUserDto>(x => x.Username, 6))
           .Matches("^[A-Za-zÀ-ÿ]+(?:\\s[A-Za-zÀ-ÿ]+)*$")
           .WithMessage("Username can only contain letters and spaces, without special characters.");

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

        RuleFor(x => x.ApplicationKey)
            .NotEmpty().WithMessage(ValidationMessages.IsRequired<RegisterUserDto>(x => x.ApplicationKey));

        RuleFor(x => x.PhoneNumber)
            .Matches("^\\+\\d{10,15}$")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage("Phone number must be in the format +<country-code><number>.");
    }
}
