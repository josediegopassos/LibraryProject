using FluentAssertions;
using LibraryProject.API.Controllers;
using LibraryProject.Application.Query;
using LibraryProject.Domain.Entities;
using LibraryProject.Infrastructure.Mongo.ReadModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LibraryProject.Tests.API
{
    public class LoansControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly LoansController _controller;

        public LoansControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new LoansController(_mediatorMock.Object);
        }

        [Fact]
        public async Task ObterEmprestimoPorId_DeveRetornarOkSeEncontrado()
        {
            var loanId = Guid.NewGuid();
            var loanReadModel = new LoanReadModel
            {
                LoanId = loanId,
                BookId = Guid.NewGuid(),
                Status = "Active"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetLoanByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(loanReadModel);

            var result = await _controller.GetById(loanId);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(loanReadModel);
        }

        [Fact]
        public async Task ObterEmprestimoPorId_DeveRetornarNotFoundSeNaoEncontrado()
        {
            var loanId = Guid.NewGuid();

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetLoanByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync((LoanReadModel)null);

            var result = await _controller.GetById(loanId);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task ListarEmprestimos_DeveRetornarOkComLista()
        {
            var loans = new List<LoanReadModel>
            {
                new LoanReadModel { LoanId = Guid.NewGuid(), BookId = Guid.NewGuid(), Status = "Active" },
                new LoanReadModel { LoanId = Guid.NewGuid(), BookId = Guid.NewGuid(), Status = "Returned" }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllLoanQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(loans);

            var result = await _controller.GetAll();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(loans);
        }
    }
}
