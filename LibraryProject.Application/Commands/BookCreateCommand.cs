using MediatR;

namespace LibraryProject.Application.Commands
{
    public class BookCreateCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearPublication { get; set; }
        public int AvailableQuantity { get; set; }

        public BookCreateCommand(string title, string author, int yearPublication, int availableQuantity)
        {
            Title = title;
            Author = author;
            YearPublication = yearPublication;
            AvailableQuantity = availableQuantity;
        }
    }
}
