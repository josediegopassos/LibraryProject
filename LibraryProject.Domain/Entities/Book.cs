using LibraryProject.Domain.Validation;

namespace LibraryProject.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public int YearPublication { get; private set; }
        public int AvailableQuantity { get; private set; }

        private Book() { }

        public Book(string title, string author, int yearPublication, int availableQuantity)
        {
            if (string.IsNullOrWhiteSpace(title)) 
                throw new DomainException("Título é obrigatório.");

            if (string.IsNullOrWhiteSpace(author)) 
                throw new DomainException("Autor é obrigatório.");

            if (availableQuantity < 0) 
                throw new DomainException("Quantidade não pode ser negativa.");

            Title = title.Trim();
            Author = author.Trim();
            YearPublication = yearPublication;
            AvailableQuantity = availableQuantity;
        }

        public void SetId(Guid id)
        {
            Id = id;
        }

        public void ReduceAvailable()
        {
            if (AvailableQuantity <= 0)
                throw new DomainException("Não há exemplares disponíveis.");

            AvailableQuantity--;
        }

        public void IncreaseAvailable()
        {
            AvailableQuantity++;
        }
    }
}
