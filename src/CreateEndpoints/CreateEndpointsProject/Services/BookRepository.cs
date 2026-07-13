using CreateEndpointsProject.Models;

namespace CreateEndpointsProject.Services;

// In-memory implementation — fine for interviews and local practice.
// You do not need to implement this; focus on the endpoints in Program.cs and BooksController.cs.
public class BookRepository : IBookRepository
{
    private readonly List<Book> _books =
    [
        new(1, "The Pragmatic Programmer", "Hunt & Thomas", 1999),
        new(2, "Clean Code", "Robert C. Martin", 2008),
        new(3, "Designing Data-Intensive Applications", "Martin Kleppmann", 2017),
    ];

    private int _nextId = 4;

    public IReadOnlyList<Book> GetAll() => _books.AsReadOnly();

    public IReadOnlyList<Book> Search(string? author, int? year)
    {
        IEnumerable<Book> query = _books;

        if (!string.IsNullOrWhiteSpace(author))
            query = query.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));

        if (year is not null)
            query = query.Where(b => b.PublishedYear == year);

        return query.ToList().AsReadOnly();
    }

    public Book? GetById(int id) => _books.FirstOrDefault(b => b.Id == id);

    public Book Add(CreateBookRequest request)
    {
        var book = new Book(_nextId++, request.Title, request.Author, request.PublishedYear);
        _books.Add(book);
        return book;
    }

    public Book? Update(int id, UpdateBookRequest request)
    {
        var index = _books.FindIndex(b => b.Id == id);
        if (index < 0)
            return null;

        var current = _books[index];
        var updated = current with
        {
            Title = request.Title ?? current.Title,
            Author = request.Author ?? current.Author,
            PublishedYear = request.PublishedYear ?? current.PublishedYear,
        };

        _books[index] = updated;
        return updated;
    }

    public bool Delete(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book is null)
            return false;

        _books.Remove(book);
        return true;
    }
}
