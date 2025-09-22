namespace LibraryProject.Domain.Events
{
    public class BookReturnedEvent
    {
        public Guid BookId { get; }

        public Guid LoanId { get; }

        public DateTime ReturnDate { get; }

        public BookReturnedEvent(Guid bookId, Guid loanId, DateTime returnDate)
        {
            BookId = bookId;
            LoanId = loanId;
            ReturnDate = returnDate;
        }
    }
}
