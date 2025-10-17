using Microsoft.EntityFrameworkCore;
using LibraryManagement.Persistence;

namespace LibraryManagement.Features.Books;

public class DeleteBookHandler(LibraryContext db)
{
    private readonly LibraryContext _db = db;

    public async Task<IResult> Handle(DeleteBookRequest request)
    {
        var entity = await _db.Books.FirstOrDefaultAsync(b => b.Id == request.Id);
        if (entity is null) return Results.NotFound();

        _db.Books.Remove(entity);
        await _db.SaveChangesAsync();
        return Results.NoContent();
    }
}
