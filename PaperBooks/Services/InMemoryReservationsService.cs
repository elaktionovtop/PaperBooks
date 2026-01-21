using PaperBooks.Models;

namespace PaperBooks.Services
{
    public sealed class InMemoryReservationsService
        : IReservationsService
    {
        private readonly List<Reservation> _reservations = [];

        public InMemoryReservationsService
            (IEnumerable<Reservation> reservations)
        {
            _reservations.AddRange(reservations);
        }

        public IEnumerable<Reader> GetReadersReservedBook(Book book)
            => _reservations.Where(r => r.Book == book)
                .Select(r => r.Reader);

        public void RemoveReservation(Book book, Reader reader)
       => _reservations.RemoveAll(r => r.Book == book && r.Reader == reader);
    }
}

