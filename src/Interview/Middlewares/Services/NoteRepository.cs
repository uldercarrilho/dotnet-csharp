using Middlewares.Models;

namespace Middlewares.Services;

// In-memory store — pre-built so middleware exercises have real endpoints to hit.
public class NoteRepository : INoteRepository
{
    private readonly List<Note> _notes =
    [
        new(1, "Middleware basics", "Each component can inspect or modify the request/response."),
        new(2, "Pipeline order", "Request goes down the stack; the response bubbles back up."),
        new(3, "Short-circuit", "Call next() to continue; skip it to stop the pipeline."),
    ];

    public IReadOnlyList<Note> GetAll() => _notes.AsReadOnly();

    public Note? GetById(int id) => _notes.FirstOrDefault(n => n.Id == id);
}
