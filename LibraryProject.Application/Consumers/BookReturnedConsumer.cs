using LibraryProject.Domain.Events;
using LibraryProject.Domain.Repositories;
using MassTransit;

namespace LibraryProject.Application.Consumers
{
    public class BookReturnedConsumer : IConsumer<BookReturnedEvent>
    {
        private readonly IBookReadRepository _repository;

        public BookReturnedConsumer(IBookReadRepository repository) => _repository = repository;

        public async Task Consume(ConsumeContext<BookReturnedEvent> context)
        {
            var message = context.Message;
            var book = await _repository.GetByIdAsync(message.BookId);
            if (book != null)
            {
                book.AvailableQuantity++;
                await _repository.UpdateAsync(book);
            }
        }
    }
}
