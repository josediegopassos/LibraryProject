using LibraryProject.Domain.Entities;

namespace LibraryProject.Domain.Repositories
{
    public interface ILoanRepository
    {
        Task<Loan> GetByIdAsync(int id);
        Task AddAsync(Loan loan);
        Task UpdateAsync(Loan loan);
    }
}
