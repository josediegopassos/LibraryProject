using LibraryProject.Application.Query;
using LibraryProject.Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace LibraryProject.Application.Handlers.Query
{
    public class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, LoanReadModel>
    {
        private readonly IMongoCollection<LoanReadModel> _loans;

        public GetLoanByIdQueryHandler(IMongoDatabase database)
        {
            _loans = database.GetCollection<LoanReadModel>("Loans");
        }

        public async Task<LoanReadModel> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<LoanReadModel>.Filter.Eq(x => x.LoanId, request.LoanId);
            return await _loans.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
