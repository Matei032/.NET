using LibraryManagement.Persistence;
using LibraryManagement.Validators;

namespace LibraryManagement.Features.Books;

public class CreateBookHandler(LibraryContext context)
{
    private readonly LibraryContext _context = context;

    public async Task<IResult> Handle(CreateBookRequest request)
    {
        var validator = new CreateBookValidator();
        var validation = await validator.ValidateAsync(request);
        if (!validation.IsValid)
            return Results.BadRequest(validation.Errors);

        var entity = new Book(0, request.Title, request.Author, request.Year);
        _context.Books.Add(entity);
        await _context.SaveChangesAsync();

        return Results.Created($"/books/{entity.Id}", entity);
    }
}
