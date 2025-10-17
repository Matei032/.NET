using FluentValidation;
using LibraryManagement.Features.Books;

namespace LibraryManagement.Validators;

public class CreateBookValidator : AbstractValidator<CreateBookRequest>
{
    public CreateBookValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Author).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Year).InclusiveBetween(1450, DateTime.UtcNow.Year);
    }
}
