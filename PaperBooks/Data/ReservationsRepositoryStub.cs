using PaperBooks.Models;

namespace PaperBooks.Data
{
    public sealed class ReservationsRepositoryStub
        : IReservationsRepository
    {
        private readonly List<Reservation> _reservations =
        [
            new Reservation
            {
                Book = new Book { Id = 1, Title = "CLR via C#" },
                Reader = new Reader { Id = 1, Name = "Иванов" },
                CreatedAt = DateOnly.FromDateTime(DateTime.Now)
            }
        ];

        public IEnumerable<Reservation> GetAll()
            => _reservations;

        public void Remove(Book book, Reader reader)
        {
            _reservations.RemoveAll(r =>
                r.Book.Id == book.Id &&
                r.Reader.Id == reader.Id);
        }
    }
}
