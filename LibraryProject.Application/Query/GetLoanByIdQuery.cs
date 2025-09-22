using LibraryProject.Domain.Entities;
using MediatR;

namespace LibraryProject.Application.Query
{
    public class GetLoanByIdQuery : IRequest<LoanReadModel>
    {
        public Guid LoanId { get; }

        public GetLoanByIdQuery(Guid loanId)
        {
            LoanId = loanId;
        }
    }
}
