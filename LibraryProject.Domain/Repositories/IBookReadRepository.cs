using LibraryProject.Domain.Entities;

namespace LibraryProject.Domain.Repositories
{
    public interface IBookReadRepository
    {
        Task<BookReadModel> GetByIdAsync(Guid id);
        Task<IEnumerable<BookReadModel>> ListAllAsync();
        Task AddAsync(BookReadModel book);
        Task UpdateAsync(BookReadModel book);
    }
}
