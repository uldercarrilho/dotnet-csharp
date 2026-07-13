using CreateEndpointsProject.Models;

namespace CreateEndpointsProject.Services;

// Repository abstraction — endpoints depend on this interface, not the concrete store.
// Interview tip: mention dependency injection (DI). Register IBookRepository in Program.cs
// with builder.Services.AddSingleton<IBookRepository, BookRepository>() so handlers receive
// the same in-memory list for the lifetime of the app.
public interface IBookRepository
{
    IReadOnlyList<Book> GetAll();
    IReadOnlyList<Book> Search(string? author, int? year);
    Book? GetById(int id);
    Book Add(CreateBookRequest request);
    Book? Update(int id, UpdateBookRequest request);
    bool Delete(int id);
}
