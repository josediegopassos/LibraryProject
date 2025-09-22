namespace LibraryProject.Infrastructure.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryProjectDbContext _context;

        public UnitOfWork(LibraryProjectDbContext context)
        {
            _context = context;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
