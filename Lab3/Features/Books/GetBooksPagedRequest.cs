namespace LibraryManagement.Features.Books;

public record GetBooksPagedRequest(int Page = 1, int PageSize = 10);
