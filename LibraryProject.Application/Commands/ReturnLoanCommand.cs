using MediatR;

namespace LibraryProject.Application.Commands
{
    public class ReturnLoanCommand : IRequest
    {
        public int LoanId { get; set; }
    }
}
