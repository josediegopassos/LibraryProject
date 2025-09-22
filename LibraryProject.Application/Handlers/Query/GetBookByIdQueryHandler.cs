using LibraryProject.Application.Query;
using LibraryProject.Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace LibraryProject.Application.Handlers.Query
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookReadModel>
    {
        private readonly IMongoCollection<BookReadModel> _books;

        public GetBookByIdQueryHandler(IMongoDatabase database)
        {
            _books = database.GetCollection<BookReadModel>("Books");
        }

        public async Task<BookReadModel> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<BookReadModel>.Filter.Eq(x => x.BookId, request.BookId);
            return await _books.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
