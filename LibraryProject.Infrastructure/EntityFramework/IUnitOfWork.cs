namespace LibraryProject.Infrastructure.EntityFramework
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
