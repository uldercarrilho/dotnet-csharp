using CreateEndpoints.Models;
using CreateEndpoints.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreateEndpoints.Controllers;

// =============================================================================
// EXERCISE 7 — Controller-based endpoints (alternative to Minimal APIs)
// =============================================================================
//
// Minimal APIs (Program.cs) and controllers solve the same problem with different syntax.
// Many teams use controllers for larger APIs; interviews often ask you to do either.
//
// Setup required in Program.cs (uncomment when you start this exercise):
//   builder.Services.AddControllers();
//   ...
//   app.MapControllers();
//
// Key attributes to know:
//   [ApiController]       — enables automatic model validation and binding conventions
//   [Route("api/[controller]")] — base route becomes /api/books
//   [HttpGet], [HttpPost], [HttpPut], [HttpDelete] — HTTP verb + optional template
//   [FromBody], [FromQuery], [FromRoute] — where parameters come from
//
// Return types interviewers like:
//   ActionResult<Book>     — can return Ok(book), NotFound(), BadRequest(), etc.
//   IActionResult          — same flexibility without a typed payload
//   CreatedAtAction(...)   — 201 with Location header (RESTful create)
//
// TIP: Controller action names matter for CreatedAtAction(nameof(GetById), new { id = book.Id }, book)

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _repository;

    public BooksController(IBookRepository repository)
    {
        _repository = repository;
    }

    // TODO: GET /api/books
    // Return 200 OK with the full list from _repository.GetAll()
    [HttpGet]
    public ActionResult<IReadOnlyList<Book>> GetAll()
    {
        return Ok(_repository.GetAll());
    }

    // TODO: GET /api/books/{id}
    // Return 404 NotFound() when the book is missing, otherwise 200 OK with the book
    [HttpGet("{id:int}")]
    public ActionResult<Book> GetById([FromRoute] int id)
    {
        var book = _repository.GetById(id);
        return book == null ? NotFound() : Ok(book);
    }

    // TODO: POST /api/books
    // Validate Title and Author are not empty; return 400 BadRequest if invalid
    // On success return 201 CreatedAtAction pointing to GetById
    [HttpPost]
    public ActionResult<Book> Create([FromBody] CreateBookRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return BadRequest($"{nameof(request.Title)} is required.");

        if (string.IsNullOrWhiteSpace(request.Author))
            return BadRequest($"{nameof(request.Author)} is required.");

        var book = _repository.Add(request);

        return CreatedAtAction(nameof(Create), book);
    }

    // TODO: PUT /api/books/{id}
    // Return 404 if not found, otherwise 200 OK with updated book
    [HttpPut("{id:int}")]
    public ActionResult<Book> Update(int id, [FromBody] UpdateBookRequest request)
    {
        var updated = _repository.Update(id, request);
        return updated == null ? NotFound() : Ok(updated);
    }

    // TODO: DELETE /api/books/{id}
    // Return 404 if not found, otherwise 204 NoContent()
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        return _repository.Delete(id) ? NoContent() : NotFound();
    }
}
