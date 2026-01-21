using PaperBooks.Models;

namespace PaperBooks.Services
{
    public interface IReservationsService
    {
        IEnumerable<Reader> GetReadersReservedBook(Book book);
        void RemoveReservation(Book book, Reader reader);
    }
}
