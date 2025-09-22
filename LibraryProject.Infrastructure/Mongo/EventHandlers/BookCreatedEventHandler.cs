using LibraryProject.Domain.Entities;
using LibraryProject.Domain.Events;
using LibraryProject.Infrastructure.Mongo.ReadModels;
using MongoDB.Driver;

namespace LibraryProject.Infrastructure.Mongo.EventHandlers
{
    public class BookCreatedEventHandler : IEventHandler<BookCreatedEvent>
    {
        private readonly IMongoCollection<BookReadModel> _books;

        public BookCreatedEventHandler(IMongoDatabase database)
        {
            _books = database.GetCollection<BookReadModel>("Books");
        }

        public async Task HandleAsync(BookCreatedEvent bookCreatedEvent, CancellationToken cancellationToken = default)
        {
            var book = new BookReadModel
            {
                BookId = bookCreatedEvent.BookId,
                Title = bookCreatedEvent.Title,
                Author = bookCreatedEvent.Author,
                YearPublication = bookCreatedEvent.YearPublication,
                AvailableQuantity = bookCreatedEvent.AvailableQuantity
            };

            await _books.InsertOneAsync(book, null, cancellationToken);
        }
    }

}
