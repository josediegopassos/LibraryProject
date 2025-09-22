using LibraryProject.Domain.Entities;
using LibraryProject.Domain.Repositories;
using LibraryProject.Infrastructure.Mongo.ReadModels;
using MongoDB.Driver;

namespace LibraryProject.Infrastructure.Mongo.Repositories
{
    public class LoanReadRepository : ILoanReadRepository
    {
        private readonly IMongoCollection<LoanReadModel> _collection;

        public LoanReadRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<LoanReadModel>("Loans");
        }

        public async Task AddAsync(LoanReadModel loan)
        {
            await _collection.InsertOneAsync(loan);
        }

        public async Task<LoanReadModel> GetByIdAsync(Guid id)
        {
            var filter = Builders<LoanReadModel>.Filter.Eq(e => e.LoanId, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<LoanReadModel>> ListAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task UpdateAsync(LoanReadModel loan)
        {
            var filter = Builders<LoanReadModel>.Filter.Eq(e => e.LoanId, loan.LoanId);
            await _collection.ReplaceOneAsync(filter, loan, new ReplaceOptions { IsUpsert = true });
        }
    }
}
