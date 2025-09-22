using LibraryProject.Domain.Entities;
using LibraryProject.Domain.Events;
using LibraryProject.Infrastructure.Mongo.ReadModels;
using MongoDB.Driver;

namespace LibraryProject.Infrastructure.Mongo.EventHandlers
{
    public class LoanReturnedEventHandler : IEventHandler<LoanReturnedEvent>
    {
        private readonly IMongoCollection<LoanReadModel> _loans;

        public LoanReturnedEventHandler(IMongoDatabase database)
        {
            _loans = database.GetCollection<LoanReadModel>("Loans");
        }

        public async Task HandleAsync(LoanReturnedEvent loanReturnedEvent, CancellationToken cancellationToken = default)
        {
            var update = Builders<LoanReadModel>.Update
                .Set(e => e.ReturnDate, loanReturnedEvent.ReturnDate)
                .Set(e => e.Status, "Returned");

            await _loans.UpdateOneAsync(x => x.LoanId == loanReturnedEvent.LoanId, update, cancellationToken: cancellationToken);
        }
    }

}
