using FluentAssertions;
using LibraryProject.Domain.Entities;
using Xunit;

namespace LibraryProject.Tests.Domain
{
    public class BookTests
    {
        [Fact]
        public void NaoDevePermitirCriarLivroSemTitulo()
        {
            Action act = () => new Book("", "Autor Teste", 2020, 5);
            act.Should().Throw<ArgumentException>().WithMessage("Título é obrigatório");
        }

        [Fact]
        public void NaoDevePermitirCriarLivroSemAutor()
        {
            Action act = () => new Book("Titulo Teste", "", 2020, 5);
            act.Should().Throw<ArgumentException>().WithMessage("Autor é obrigatório");
        }

        [Fact]
        public void DeveReduzirQuantidadeDisponivelAoEmprestar()
        {
            var book = new Book("Titulo Teste", "Autor Teste", 2020, 2);
            book.ReduceAvailable();

            book.AvailableQuantity.Should().Be(1);
        }

        [Fact]
        public void NaoDeveEmprestarSeNaoHouverExemplares()
        {
            var book = new Book("Titulo Teste", "Autor Teste", 2020, 0);

            Action act = () => book.ReduceAvailable();

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Não há exemplares disponíveis para empréstimo");
        }

        [Fact]
        public void DeveAumentarQuantidadeDisponivelAoDevolver()
        {
            var book = new Book("Titulo Teste", "Autor Teste", 2020, 1);
            book.ReduceAvailable();
            book.IncreaseAvailable();

            book.AvailableQuantity.Should().Be(1);
        }
    }
}
