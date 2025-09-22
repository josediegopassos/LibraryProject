using LibraryProject.Domain.Validation;

namespace LibraryProject.Domain.Entities
{
    public enum LoanStatus { Active, Returned }

    public class Loan
    {
        public Guid Id { get; private set; }
        public Guid BookId { get; private set; }
        public DateTime LoanDate { get; private set; }
        public DateTime? ReturnDate { get; private set; }
        public LoanStatus Status { get; private set; }

        private Loan() { }

        public Loan(Guid bookId)
        {
            BookId = bookId;
            LoanDate = DateTime.UtcNow;
            Status = LoanStatus.Active;
        }

        public void Return()
        {
            if (Status == LoanStatus.Returned) 
                throw new DomainException("Empréstimo já devolvido.");

            Status = LoanStatus.Returned;
            ReturnDate = DateTime.UtcNow;
        }
    }
}
