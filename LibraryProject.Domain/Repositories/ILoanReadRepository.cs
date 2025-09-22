using LibraryProject.Domain.Entities;

namespace LibraryProject.Domain.Repositories
{
    public interface ILoanReadRepository
    {
        Task<LoanReadModel> GetByIdAsync(Guid id);
        Task<IEnumerable<LoanReadModel>> ListAllAsync();
        Task AddAsync(LoanReadModel loan);
        Task UpdateAsync(LoanReadModel loan);
    }
}
