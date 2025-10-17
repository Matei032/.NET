using Microsoft.EntityFrameworkCore;
using LibraryManagement.Persistence;

namespace LibraryManagement.Features.Books;

public class GetAllBooksHandler(LibraryContext db)
{
    private readonly LibraryContext _db = db;

    public async Task<IResult> Handle(GetAllBooksRequest _)
    {
        var list = await _db.Books.AsNoTracking().OrderBy(b => b.Id).ToListAsync();
        return Results.Ok(list);
    }
}
