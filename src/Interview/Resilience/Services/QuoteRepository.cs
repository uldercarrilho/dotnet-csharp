using Resilience.Models;

namespace Resilience.Services;

// In-memory store — focus your practice on REST endpoints and resilience policies in Program.cs.
public class QuoteRepository : IQuoteRepository
{
    private readonly List<Quote> _quotes =
    [
        new(1, "Fall seven times, stand up eight.", "Japanese proverb"),
        new(2, "The only way to do great work is to love what you do.", "Steve Jobs"),
        new(3, "Simplicity is the ultimate sophistication.", "Leonardo da Vinci"),
    ];

    private int _nextId = 4;

    public IReadOnlyList<Quote> GetAll() => _quotes.AsReadOnly();

    public Quote? GetById(int id) => _quotes.FirstOrDefault(q => q.Id == id);

    public Quote Add(CreateQuoteRequest request)
    {
        var quote = new Quote(_nextId++, request.Text, request.Author);
        _quotes.Add(quote);
        return quote;
    }
}
