using LibraryProject.Application.Query;
using LibraryProject.Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace LibraryProject.Application.Handlers.Query
{
    public class GetAllLoanQueryHandler : IRequestHandler<GetAllLoanQuery, IEnumerable<LoanReadModel>>
    {
        private readonly IMongoCollection<LoanReadModel> _loans;

        public GetAllLoanQueryHandler(IMongoDatabase database)
        {
            _loans = database.GetCollection<LoanReadModel>("Loans");
        }

        public async Task<IEnumerable<LoanReadModel>> Handle(GetAllLoanQuery request, CancellationToken cancellationToken)
        {
            return await _loans.Find(_ => true).ToListAsync(cancellationToken);
        }
    }
}
