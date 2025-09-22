using LibraryProject.Domain.Entities;
using LibraryProject.Domain.Events;
using LibraryProject.Infrastructure.Mongo.ReadModels;
using MongoDB.Driver;

namespace LibraryProject.Infrastructure.Mongo.EventHandlers
{
    public class BookLoanedEventHandler : IEventHandler<BookLoanedEvent>
    {
        private readonly IMongoCollection<BookReadModel> _book;

        public BookLoanedEventHandler(IMongoDatabase database)
        {
            _book = database.GetCollection<BookReadModel>("Books");
        }

        public async Task HandleAsync(BookLoanedEvent bookLoanedEvent, CancellationToken cancellationToken = default)
        {
            var filter = Builders<BookReadModel>.Filter.Eq(x => x.BookId, bookLoanedEvent.BookId);
            var update = Builders<BookReadModel>.Update.Inc(x => x.AvailableQuantity, -1);

            await _book.UpdateOneAsync(filter, update);
        }
    }
}
