using LibraryProject.Application.Commands;
using LibraryProject.Domain.Repositories;
using LibraryProject.Domain.Validation;
using LibraryProject.Infrastructure.EntityFramework;
using MassTransit;
using MediatR;

namespace LibraryProject.Application.Handlers.Command
{
    public class ReturnLoanCommandHandler : IRequestHandler<ReturnLoanCommand>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublishEndpoint _bus;

        public ReturnLoanCommandHandler(
            ILoanRepository loanRepository,
            IBookRepository bookRepository,
            IUnitOfWork uow,
            IPublishEndpoint bus)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _unitOfWork = uow;
            _bus = bus;
        }

        public async Task Handle(ReturnLoanCommand request, CancellationToken ct)
        {
            var loan = await _loanRepository.GetByIdAsync(request.LoanId);
            if (loan == null) 
                throw new DomainException("Empréstimo não encontrado.");
            
            loan.Return();

            var book = await _bookRepository.GetByIdAsync(loan.BookId);
            if (book == null)
                throw new DomainException("Livro associado ao empréstimo não encontrado.");

            book.IncreaseAvailable();

            await _unitOfWork.CommitAsync(ct);

            await _bus.Publish(new Domain.Events.BookReturnedEvent(book.Id, loan.Id, loan.ReturnDate!.Value));
        }
    }
}
