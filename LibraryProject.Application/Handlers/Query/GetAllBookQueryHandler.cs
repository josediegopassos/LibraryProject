using LibraryProject.Application.Query;
using LibraryProject.Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace LibraryProject.Application.Handlers.Query
{
    public class GetAllBookQueryHandler : IRequestHandler<GetAllBookQuery, IEnumerable<BookReadModel>>
    {
        private readonly IMongoCollection<BookReadModel> _books;

        public GetAllBookQueryHandler(IMongoDatabase database)
        {
            _books = database.GetCollection<BookReadModel>("Books");
        }

        public async Task<IEnumerable<BookReadModel>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
        {
            return await _books.Find(_ => true).ToListAsync(cancellationToken);
        }
    }
}
