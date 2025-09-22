using LibraryProject.Domain.Entities;
using LibraryProject.Domain.Events;
using LibraryProject.Infrastructure.Mongo.ReadModels;
using MongoDB.Driver;

namespace LibraryProject.Infrastructure.Mongo.EventHandlers
{
    public class LoanCreatedEventHandler : IEventHandler<LoanCreatedEvent>
    {
        private readonly IMongoCollection<LoanReadModel> _loans;

        public LoanCreatedEventHandler(IMongoDatabase database)
        {
            _loans = database.GetCollection<LoanReadModel>("Loans");
        }

        public async Task HandleAsync(LoanCreatedEvent loanCreatedEvent, CancellationToken cancellationToken = default)
        {
            var loan = new LoanReadModel
            {
                LoanId = loanCreatedEvent.LoanId,
                BookId = loanCreatedEvent.BookId,
                LoanDate = loanCreatedEvent.LoanDate,
                Status = "Active"
            };

            await _loans.InsertOneAsync(loan, null, cancellationToken);
        }
    }

}
