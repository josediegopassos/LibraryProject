using FluentAssertions;
using LibraryProject.Application.Commands;
using LibraryProject.Application.Handlers.Command;
using LibraryProject.Domain.Entities;
using LibraryProject.Domain.Repositories;
using LibraryProject.Infrastructure.EntityFramework;
using MassTransit;
using MediatR;
using Moq;
using Xunit;

namespace LibraryProject.Tests.Application
{
    public class BookCreateCommandHandlerTests
    {
        [Fact]
        public async Task DeveCriarLivroComSucesso()
        {
            // Arrange
            var BookRepositoryMock = new Mock<IBookRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var busMock = new Mock<IPublishEndpoint>();

            unitOfWorkMock
                .Setup(u => u.CommitAsync(new CancellationToken())).ReturnsAsync(1);

            var handler = new BookCreatedCommandHandler(
                unitOfWorkMock.Object,
                BookRepositoryMock.Object,                
                busMock.Object);

            var command = new BookCreateCommand("Titulo Teste", "Autor Teste", 2020, 5);

            // Act
            var id = await handler.Handle(command, CancellationToken.None);

            // Assert
            id.Should().NotBe(Guid.Empty);
            BookRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Once);
            unitOfWorkMock.Verify(u => u.CommitAsync(new CancellationToken()), Times.Once);
            busMock.Verify(m => m.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
