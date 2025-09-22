namespace LibraryProject.Infrastructure.Mongo.EventHandlers
{
    public interface IEventHandler<TEvent>
    {
        Task HandleAsync(TEvent evento, CancellationToken cancellationToken = default);
    }
}
