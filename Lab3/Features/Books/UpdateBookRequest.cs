namespace LibraryManagement.Features.Books;

public record UpdateBookRequest(int Id, string Title, string Author, int Year);
