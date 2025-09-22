using LibraryProject.Domain.Entities;
using MediatR;

namespace LibraryProject.Application.Query
{
    public class GetAllBookQuery : IRequest<IEnumerable<BookReadModel>>
    {
    }
}
