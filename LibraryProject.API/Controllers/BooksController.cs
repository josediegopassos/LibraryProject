using LibraryProject.Application.Commands;
using LibraryProject.Application.Query;
using LibraryProject.Infrastructure.Mongo.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookCreateCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = bookId }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _mediator.Send(new GetBookByIdQuery(id));
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _mediator.Send(new GetAllBookQuery());
            return Ok(books);
        }
    }
}
