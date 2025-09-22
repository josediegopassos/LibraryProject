using LibraryProject.Domain.Entities;
using MediatR;

namespace LibraryProject.Application.Query
{
    public class GetBookByIdQuery : IRequest<BookReadModel>
    {
        public Guid BookId { get; }

        public GetBookByIdQuery(Guid bookId)
        {
            BookId = bookId;
        }
    }
}
