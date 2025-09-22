using LibraryProject.Domain.Entities;

namespace LibraryProject.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(Guid id);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
    }
}
