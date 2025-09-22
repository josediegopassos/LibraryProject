using LibraryProject.Domain.Entities;
using LibraryProject.Domain.Repositories;
using LibraryProject.Infrastructure.Mongo.ReadModels;
using MongoDB.Driver;

namespace LibraryProject.Infrastructure.Mongo.Repositories
{
    public class BookReadRepository : IBookReadRepository
    {
        private readonly IMongoCollection<BookReadModel> _collection;

        public BookReadRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<BookReadModel>("Books");
        }

        public async Task AddAsync(BookReadModel book)
        {
            await _collection.InsertOneAsync(book);
        }

        public async Task<BookReadModel> GetByIdAsync(Guid id)
        {
            var filter = Builders<BookReadModel>.Filter.Eq(l => l.BookId, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BookReadModel>> ListAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task UpdateAsync(BookReadModel book)
        {
            var filter = Builders<BookReadModel>.Filter.Eq(l => l.BookId, book.BookId);
            await _collection.ReplaceOneAsync(filter, book, new ReplaceOptions { IsUpsert = true });
        }
    }
}
