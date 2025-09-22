using LibraryProject.Application.Commands;
using LibraryProject.Domain.Repositories;
using LibraryProject.Domain.Validation;
using LibraryProject.Infrastructure.EntityFramework;
using MassTransit;
using MediatR;

namespace LibraryProject.Application.Handlers.Command
{
    public class RequestLoanCommandHandler : IRequestHandler<RequestLoanCommand, Guid>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublishEndpoint _bus;

        public RequestLoanCommandHandler(IBookRepository bookRepository, ILoanRepository loanRepository, IUnitOfWork uow, IPublishEndpoint bus)
        {
            _bookRepository = bookRepository;
            _loanRepository = loanRepository;
            _unitOfWork = uow;
            _bus = bus;
        }

        public async Task<Guid> Handle(RequestLoanCommand request, CancellationToken ct)
        {
            var book = await _bookRepository.GetByIdAsync(request.BookId);
            if (book == null) 
                throw new DomainException("Livro não encontrado.");
            
            book.ReduceAvailable();

            var loan = new Domain.Entities.Loan(request.BookId);
            await _loanRepository.AddAsync(loan);

            await _unitOfWork.CommitAsync(ct);

            await _bus.Publish(new Domain.Events.BookLoanedEvent(request.BookId, loan.Id));

            return loan.Id;
        }
    }
}
