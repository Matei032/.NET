using Microsoft.EntityFrameworkCore;
using LibraryManagement.Persistence;

namespace LibraryManagement.Features.Books;

public class GetBookByIdHandler(LibraryContext db)
{
    private readonly LibraryContext _db = db;

    public async Task<IResult> Handle(GetBookByIdRequest request)
    {
        var book = await _db.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == request.Id);
        return book is null ? Results.NotFound() : Results.Ok(book);
    }
}
