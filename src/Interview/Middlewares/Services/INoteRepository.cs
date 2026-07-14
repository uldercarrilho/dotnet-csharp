using Middlewares.Models;

namespace Middlewares.Services;

public interface INoteRepository
{
    IReadOnlyList<Note> GetAll();
    Note? GetById(int id);
}
