using LibraryProject.Application.Commands;
using LibraryProject.Domain.Repositories;
using LibraryProject.Infrastructure.EntityFramework;
using MassTransit;
using MediatR;

namespace LibraryProject.Application.Handlers.Command
{
    public class BookCreatedCommandHandler : IRequestHandler<BookCreateCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;
        private readonly IPublishEndpoint _bus;

        public BookCreatedCommandHandler(IUnitOfWork uow, IBookRepository bookRepository, IPublishEndpoint bus)
        {
            _unitOfWork = uow;
            _bookRepository = bookRepository;
            _bus = bus;
        }

        public async Task<Guid> Handle(BookCreateCommand request, CancellationToken cancellationToken)
        {
            var book = new Domain.Entities.Book(request.Title, request.Author, request.YearPublication, request.AvailableQuantity);
            await _bookRepository.AddAsync(book);
            await _unitOfWork.CommitAsync(cancellationToken);

            await _bus.Publish(new Domain.Events.BookCreatedEvent(book.Id, book.Title, book.Author, book.YearPublication, book.AvailableQuantity));

            return book.Id;
        }
    }
}
