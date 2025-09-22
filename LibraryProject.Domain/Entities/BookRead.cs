namespace LibraryProject.Domain.Entities
{
    public class BookReadModel
    {
        public Guid BookId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public int YearPublication { get; set; }

        public int AvailableQuantity { get; set; }
    }
}
