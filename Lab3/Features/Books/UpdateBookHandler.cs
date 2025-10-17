using LibraryManagement.Persistence;
using LibraryManagement.Validators;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Features.Books;

public class UpdateBookHandler(LibraryContext db)
{
    private readonly LibraryContext _db = db;

    public async Task<IResult> Handle(UpdateBookRequest request)
    {
        var validator = new UpdateBookValidator();
        var validation = await validator.ValidateAsync(request);
        if (!validation.IsValid)
            return Results.BadRequest(validation.Errors);

        var existing = await _db.Books.FirstOrDefaultAsync(b => b.Id == request.Id);
        if (existing is null) return Results.NotFound();

        var updated = existing with { Title = request.Title, Author = request.Author, Year = request.Year };

        _db.Entry(existing).CurrentValues.SetValues(updated);
        await _db.SaveChangesAsync();
        return Results.NoContent();
    }
}
