using Microsoft.EntityFrameworkCore;
using LibraryManagement.Persistence;

namespace LibraryManagement.Features.Books;

public record PagedResult<T>(IReadOnlyList<T> Items, int Total, int Page, int PageSize);

public class GetBooksPagedHandler(LibraryContext db)
{
    private readonly LibraryContext _db = db;

    public async Task<IResult> Handle(GetBooksPagedRequest request)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize is < 1 or > 100 ? 10 : request.PageSize;

        var query = _db.Books.AsNoTracking().OrderBy(b => b.Id); // OrderBy obligatoriu
        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var result = new PagedResult<Book>(items, total, page, pageSize);
        return Results.Ok(result);
    }
}
