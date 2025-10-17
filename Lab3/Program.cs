using FluentValidation;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Features.Books;
using LibraryManagement.Persistence;
using LibraryManagement.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<LibraryContext>(opt =>
    opt.UseSqlite("Data Source=library.db"));

builder.Services.AddScoped<CreateBookHandler>();
builder.Services.AddScoped<GetAllBooksHandler>();
builder.Services.AddScoped<UpdateBookHandler>();
builder.Services.AddScoped<DeleteBookHandler>();
builder.Services.AddScoped<GetBookByIdHandler>();builder.Services.AddScoped<GetBooksPagedHandler>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateBookValidator>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/books", async (CreateBookRequest req, CreateBookHandler h) =>
    await h.Handle(req));

app.MapPut("/books/{id:int}", async (int id, UpdateBookRequest body, UpdateBookHandler h) =>
{
    var cmd = body with { Id = id };
    return await h.Handle(cmd);
});

app.MapGet("/books/all", async (GetAllBooksHandler h) =>
    await h.Handle(new GetAllBooksRequest()));

app.MapDelete("/books/{id:int}", async (int id, DeleteBookHandler h) =>
    await h.Handle(new DeleteBookRequest(id)));

app.MapGet("/books/{id:int}", async (int id, GetBookByIdHandler h) =>
    await h.Handle(new GetBookByIdRequest(id)));

app.MapGet("/books", async (int page, int pageSize, GetBooksPagedHandler h) =>
    await h.Handle(new GetBooksPagedRequest(page, pageSize)));

app.Run();
