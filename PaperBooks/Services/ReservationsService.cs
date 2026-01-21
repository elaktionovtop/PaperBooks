using PaperBooks.Data;
using PaperBooks.Models;

namespace PaperBooks.Services
{
    public sealed class ReservationsService : IReservationsService
    {
        private readonly IReservationsRepository _repository;

        public ReservationsService(IReservationsRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Reader> GetReadersReservedBook(Book book)
        {
            if(book is null)
                return [];

            return _repository.GetAll()
                .Where(r => r.Book.Id == book.Id)
                .Select(r => r.Reader);
        }

        public void RemoveReservation(Book book, Reader reader)
        {
            if(book is null || reader is null)
                return;

            _repository.Remove(book, reader);
        }
    }
}
