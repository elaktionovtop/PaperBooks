using Microsoft.EntityFrameworkCore;

using PaperBooks.Models;

namespace PaperBooks.Data
{
    public sealed class ReservationsRepositoryEF
        : IReservationsRepository
    {
        private readonly LibraryDbContext _db;

        public ReservationsRepositoryEF(LibraryDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Reservation> GetAll() =>
            _db.Reservations
            .Include(r => r.Book)
            .Include(r => r.Reader)
            .ToList();

        public void Remove(Book book, Reader reader)
        {
            var items = _db.Reservations
                .Where(r => r.Book.Id == book.Id &&
                            r.Reader.Id == reader.Id);

            _db.Reservations.RemoveRange(items);
            _db.SaveChanges();
        }
    }
}
