using MediatR;

namespace LibraryProject.Application.Commands
{
    public class RequestLoanCommand : IRequest<Guid>
    {
        public Guid BookId { get; set; }

        public RequestLoanCommand(Guid bookId)
        {
            BookId = bookId;
        }
    }
}
