using LibraryProject.Domain.Entities;
using LibraryProject.Domain.Events;
using LibraryProject.Infrastructure.Mongo.ReadModels;
using MongoDB.Driver;

namespace LibraryProject.Infrastructure.Mongo.EventHandlers
{
    public class BookReturnedEventHandlers
    {
        private readonly IMongoCollection<BookReadModel> _books;

        public BookReturnedEventHandlers(IMongoDatabase database)
        {
            _books = database.GetCollection<BookReadModel>("Books");
        }

        public async Task HandleAsync(BookReturnedEvent bookReturnedEvent)
        {
            var filter = Builders<BookReadModel>.Filter.Eq(x => x.BookId, bookReturnedEvent.BookId);
            var update = Builders<BookReadModel>.Update.Inc(x => x.AvailableQuantity, 1);

            await _books.UpdateOneAsync(filter, update);
        }
    }
}
