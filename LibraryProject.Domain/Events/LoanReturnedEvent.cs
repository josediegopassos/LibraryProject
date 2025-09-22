namespace LibraryProject.Domain.Events
{
    public class LoanReturnedEvent
    {
        public Guid LoanId { get; }
        public DateTime ReturnDate { get; }

        public LoanReturnedEvent(Guid loanId, DateTime returnDate)
        {
            LoanId = loanId;
            ReturnDate = returnDate;
        }
    }
}
