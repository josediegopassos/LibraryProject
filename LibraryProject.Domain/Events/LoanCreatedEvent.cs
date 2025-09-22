namespace LibraryProject.Domain.Events
{
    public class LoanCreatedEvent
    {
        public Guid LoanId { get; }
        public Guid BookId { get; }
        public DateTime LoanDate { get; }

        public LoanCreatedEvent(Guid loanId, Guid bookId, DateTime loanDate)
        {
            LoanId = loanId;
            BookId = bookId;
            LoanDate = loanDate;
        }
    }
}
