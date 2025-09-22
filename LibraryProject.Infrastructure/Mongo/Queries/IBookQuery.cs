using LibraryProject.Domain.Entities;
using LibraryProject.Infrastructure.Mongo.ReadModels;

namespace LibraryProject.Infrastructure.Mongo.Queries
{
    public interface IBookQuery
    {
        Task<BookReadModel> GetByIdAsync(Guid id);
        Task<IEnumerable<BookReadModel>> GetAllAsync();
    }
}
