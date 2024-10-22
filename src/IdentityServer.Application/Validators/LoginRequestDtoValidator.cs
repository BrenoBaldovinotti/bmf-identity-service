using FluentValidation;
using IdentityServer.Application.DTOs;

namespace IdentityServer.Application.Validators;

public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage(ValidationMessages.IsRequired<LoginRequestDto>(x => x.Username));
        RuleFor(x => x.Password).NotEmpty().WithMessage(ValidationMessages.IsRequired<LoginRequestDto>(x => x.Password));
    }
}
