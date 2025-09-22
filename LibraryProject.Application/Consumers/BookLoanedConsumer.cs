using LibraryProject.Domain.Events;
using LibraryProject.Domain.Repositories;
using MassTransit;

namespace LibraryProject.Application.Consumers
{
    public class BookLoanedConsumer : IConsumer<BookLoanedEvent>
    {
        private readonly IBookReadRepository _repository;

        public BookLoanedConsumer(IBookReadRepository repository) => _repository = repository;

        public async Task Consume(ConsumeContext<BookLoanedEvent> context)
        {
            var message = context.Message;
            var book = await _repository.GetByIdAsync(message.BookId);
            if (book != null && book.AvailableQuantity > 0)
            {
                book.AvailableQuantity--;
                await _repository.UpdateAsync(book);
            }
        }
    }
}
