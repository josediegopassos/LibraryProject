using LibraryProject.Domain.Entities;
using LibraryProject.Infrastructure.Mongo.ReadModels;
using MongoDB.Driver;

namespace LibraryProject.Infrastructure.Mongo.Queries
{
    public class BookQuery : IBookQuery
    {
        private readonly IMongoCollection<BookReadModel> _books;

        public BookQuery(IMongoDatabase database)
        {
            _books = database.GetCollection<BookReadModel>("Books");
        }

        public async Task<BookReadModel> GetByIdAsync(Guid id)
        {
            var filter = Builders<BookReadModel>.Filter.Eq(x => x.BookId, id);
            return await _books.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BookReadModel>> GetAllAsync()
        {
            return await _books.Find(_ => true).ToListAsync();
        }
    }
}
