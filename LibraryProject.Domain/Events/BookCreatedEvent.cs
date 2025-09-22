namespace LibraryProject.Domain.Events
{
    public class BookCreatedEvent
    {
        public Guid BookId { get; }
        public string Title { get; }
        public string Author { get; }
        public int YearPublication { get; }
        public int AvailableQuantity { get; }

        public BookCreatedEvent(Guid bookId, string title, string author, int yearPublication, int availableQuantity)
        {
            BookId = bookId;
            Title = title;
            Author = author;
            YearPublication = yearPublication;
            AvailableQuantity = availableQuantity;
        }
    }
}
