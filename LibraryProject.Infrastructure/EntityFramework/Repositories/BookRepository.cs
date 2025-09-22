using LibraryProject.Domain.Entities;
using LibraryProject.Domain.Repositories;

namespace LibraryProject.Infrastructure.EntityFramework.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryProjectDbContext _context;

        public BookRepository(LibraryProjectDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public async Task<Book> GetByIdAsync(Guid id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
        }
    }
}
