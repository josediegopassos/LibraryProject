using FluentAssertions;
using LibraryProject.Application.Commands;
using LibraryProject.Application.Handlers.Command;
using LibraryProject.Domain.Entities;
using LibraryProject.Domain.Events;
using LibraryProject.Domain.Repositories;
using LibraryProject.Infrastructure.EntityFramework;
using MassTransit;
using MediatR;
using Moq;
using Xunit;

namespace LibraryProject.Tests.Application
{
    public class RequestLoanCommandHandlerTests
    {
        [Fact]
        public async Task DeveCriarEmprestimoSeLivroDisponivel()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var loanId = Guid.NewGuid();

            var book = new Book("Titulo Teste", "Autor Teste", 2020, 1);
            book.SetId(bookId);

            var bookRepositoryMock = new Mock<IBookRepository>();
            var loanRepositoryMock = new Mock<ILoanRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var busMock = new Mock<IPublishEndpoint>();

            bookRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(book);
            unitOfWorkMock.Setup(u => u.CommitAsync(new CancellationToken())).ReturnsAsync(1);

            var handler = new RequestLoanCommandHandler(
                bookRepositoryMock.Object,
                loanRepositoryMock.Object,
                unitOfWorkMock.Object,
                busMock.Object
            );

            var command = new RequestLoanCommand(bookId);

            // Act
            var resultId = await handler.Handle(command, CancellationToken.None);

            // Assert
            resultId.Should().NotBe(Guid.Empty);
            loanRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Loan>()), Times.Once);
            unitOfWorkMock.Verify(u => u.CommitAsync(new CancellationToken()), Times.Once);
            busMock.Verify(m => m.Publish(It.IsAny<BookLoanedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
