using LibraryProject.Application.Commands;
using LibraryProject.Application.Query;
using LibraryProject.Domain.Validation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoansController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var loan = await _mediator.Send(new GetLoanByIdQuery(id));
            if (loan == null)
                return NotFound();

            return Ok(loan);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var loans = await _mediator.Send(new GetAllLoanQuery());
            return Ok(loans);
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestLoan([FromBody] RequestLoanCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var loanId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id = loanId }, null);
            }
            catch (DomainException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnLoan([FromBody] ReturnLoanCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
