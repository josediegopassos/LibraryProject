using LibraryProject.Domain.Entities;
using LibraryProject.Domain.Events;
using LibraryProject.Domain.Repositories;
using MassTransit;

namespace LibraryProject.Application.Consumers
{
    public class BookCreatedConsumer : IConsumer<BookCreatedEvent>
    {
        private readonly IBookReadRepository _repository;

        public BookCreatedConsumer(IBookReadRepository repository) => _repository = repository;

        public async Task Consume(ConsumeContext<BookCreatedEvent> context)
        {
            var message = context.Message;
            var book = new BookReadModel
            {
                BookId = message.BookId,
                Title = message.Title,
                Author = message.Author,
                YearPublication = message.YearPublication,
                AvailableQuantity = message.AvailableQuantity
            };
            await _repository.AddAsync(book);
        }
    }
}