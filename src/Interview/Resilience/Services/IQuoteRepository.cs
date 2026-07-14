using Resilience.Models;

namespace Resilience.Services;

public interface IQuoteRepository
{
    IReadOnlyList<Quote> GetAll();
    Quote? GetById(int id);
    Quote Add(CreateQuoteRequest request);
}
