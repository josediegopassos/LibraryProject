namespace LibraryProject.Domain.Events
{
    public class BookLoanedEvent
    {
        public Guid BookId { get; }
        public Guid LoanId { get; }
        public DateTime LoanDate { get; }

        public BookLoanedEvent(Guid bookId, Guid loanId)
        {
            BookId = bookId;
            LoanDate = DateTime.UtcNow;
            LoanId = loanId;
        }
    }
}
