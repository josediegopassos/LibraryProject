using LibraryProject.Domain.Entities;
using LibraryProject.Domain.Repositories;

namespace LibraryProject.Infrastructure.EntityFramework.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryProjectDbContext _context;

        public LoanRepository(LibraryProjectDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
        }

        public async Task<Loan> GetByIdAsync(int id)
        {
            return await _context.Loans.FindAsync(id);
        }

        public async Task UpdateAsync(Loan loan)
        {
            _context.Loans.Update(loan);
        }
    }
}
