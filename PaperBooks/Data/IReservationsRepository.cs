using PaperBooks.Models;

namespace PaperBooks.Data
{
    public interface IReservationsRepository
    {
        IEnumerable<Reservation> GetAll();
        void Remove(Book book, Reader reader);
    }
}
