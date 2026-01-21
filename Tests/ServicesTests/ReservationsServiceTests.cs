using PaperBooks.Data;
using PaperBooks.Models;
using PaperBooks.Services;

namespace Tests.ServiceTests
{
    public sealed class ReservationsServiceTests
    {
        [Fact]
        public void GetReadersReservedBook_ReturnsReaders()
        {
            var repository = new ReservationsRepositoryStub();
            var service = new ReservationsService(repository);

            var book = new Book { Id = 1 };

            var readers = service.GetReadersReservedBook(book).ToList();

            Assert.Single(readers);
            Assert.Equal("Иванов", readers[0].Name);
        }

        [Fact]
        public void RemoveReservation_RemovesItem()
        {
            var repository = new ReservationsRepositoryStub();
            var service = new ReservationsService(repository);

            var book = new Book { Id = 1 };
            var reader = new Reader { Id = 1 };

            service.RemoveReservation(book, reader);

            var readers = service.GetReadersReservedBook(book);

            Assert.Empty(readers);
        }
    }
}
