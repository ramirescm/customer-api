using FluentValidation;

namespace Customer.Application.Features.Customer.Commands.Create;

public sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(e => e.FullName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");
        RuleFor(e => e.Email)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");

        RuleForEach(e => e.Phones)
            .SetValidator(new PhonesValidator());
    }
}

public class PhonesValidator : AbstractValidator<Phones>
{
    public PhonesValidator()
    {
        RuleFor(e => e.AreaCode)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");

        RuleFor(e => e.Number)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");

        RuleFor(e => e.Type)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Must(ValidateType)
            .WithMessage("{PropertyName} invalid type");
    }

    private bool ValidateType(string type)
    {
        var types = new List<string> { "Mobile", "Landline" };
        return types.Contains(type);
    }
}