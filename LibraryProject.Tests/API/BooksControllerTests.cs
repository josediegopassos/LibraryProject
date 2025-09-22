using FluentAssertions;
using LibraryProject.API.Controllers;
using LibraryProject.Application.Commands;
using LibraryProject.Application.Query;
using LibraryProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LibraryProject.Tests.API
{
    public class BooksControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly BooksController _controller;

        public BooksControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new BooksController(_mediatorMock.Object);
        }

        [Fact]
        public async Task CriarLivro_DeveRetornarCreated()
        {
            var command = new BookCreateCommand("Titulo", "Autor", 2020, 5);
            var bookId = Guid.NewGuid();

            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(bookId);

            var result = await _controller.Create(command);

            var createdResult = result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be(201);
            createdResult.RouteValues["id"].Should().Be(bookId);
        }

        [Fact]
        public async Task ObterLivroPorId_DeveRetornarOkSeEncontrado()
        {
            var bookId = Guid.NewGuid();
            var bookReadModel = new BookReadModel { BookId = bookId, Title = "Titulo", Author = "Autor" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(bookReadModel);

            var result = await _controller.GetById(bookId);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(bookReadModel);
        }

        [Fact]
        public async Task ObterLivroPorId_DeveRetornarNotFoundSeNaoEncontrado()
        {
            var bookId = Guid.NewGuid();

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync((BookReadModel)null);

            var result = await _controller.GetById(bookId);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task ListarLivros_DeveRetornarOkComLista()
        {
            var books = new List<BookReadModel>
            {
                new BookReadModel { BookId = Guid.NewGuid(), Title = "Livro 1", Author = "Autor 1" },
                new BookReadModel { BookId = Guid.NewGuid(), Title = "Livro 2", Author = "Autor 2" }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllBookQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(books);

            var result = await _controller.GetAll();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(books);
        }
    }
}
