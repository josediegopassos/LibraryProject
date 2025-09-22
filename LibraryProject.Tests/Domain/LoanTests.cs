using FluentAssertions;
using LibraryProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryProject.Tests.Domain
{
    public class LoanTests
    {
        [Fact]
        public void DeveCriarEmprestimoAtivo()
        {
            var loan = new Loan(new Guid("a14c8f9a-a25e-4a4c-80af-35d67cd3650a"));

            loan.Status.Should().Be(LoanStatus.Active);
            loan.LoanDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        }

        [Fact]
        public void NaoDevePermitirDevolverEmprestimoJaDevolvido()
        {
            var loan = new Loan(new Guid("a14c8f9a-a25e-4a4c-80af-35d67cd3650a"));
            loan.Return();

            Action act = () => loan.Return();

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("O empréstimo já foi devolvido");
        }

        [Fact]
        public void DeveDevolverEmprestimoComSucesso()
        {
            var loan = new Loan(new Guid("a14c8f9a-a25e-4a4c-80af-35d67cd3650a"));
            loan.Return();

            loan.Status.Should().Be(LoanStatus.Returned);
            loan.ReturnDate.Should().NotBeNull();
        }
    }
}
