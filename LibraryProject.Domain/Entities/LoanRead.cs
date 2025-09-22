namespace LibraryProject.Domain.Entities
{
    public class LoanReadModel
    {
        public Guid LoanId { get; set; }

        public Guid BookId { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public string Status { get; set; }
    }
}
