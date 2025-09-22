using LibraryProject.Domain.Entities;
using LibraryProject.Infrastructure.Mongo.ReadModels;
using MediatR;

namespace LibraryProject.Application.Query
{
    public class GetAllLoanQuery : IRequest<IEnumerable<LoanReadModel>>
    {
    }
}
