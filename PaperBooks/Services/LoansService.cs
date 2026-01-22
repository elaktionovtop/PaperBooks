using PaperBooks.Data;
using PaperBooks.Models;

namespace PaperBooks.Services
{
    public sealed class LoansService : ILoansService
    {
        private readonly ILoansRepository _repository;
        private readonly IReservationsService _reservations;

        public LoansService(
            ILoansRepository repository,
            IReservationsService reservations
            )
        {
            _repository = repository;
            _reservations = reservations;
        }

        public IEnumerable<Loan> GetActiveLoans() =>
            _repository.GetAll()
            .Where(l => l.ReturnAt == null);

        public IEnumerable<Loan> GetReaderLoans(Reader reader) =>
            _repository.GetAll()
            .Where(l => l.Reader.Id == reader.Id &&
                l.ReturnAt == null);

        public IEnumerable<BookCopy> GetReaderBookCopies(Reader reader) =>
            GetReaderLoans(reader)
            .Select(l => l.Copy);

        public Loan IssueBook(Reader reader, Book book)
        {
            if(reader is null)
                throw new ArgumentNullException(nameof(reader));
            if(book is null)
                throw new ArgumentNullException(nameof(book));

            var freeCopy = book.Copies.FirstOrDefault(IsFree)
                ?? throw new InvalidOperationException(
                    "Нет свободных экземпляров");

            _reservations.RemoveReservation(book, reader);

            var loan = new Loan
            {
                Reader = reader,
                Copy = freeCopy,
                IssuedAt = DateTime.Now
            };

            _repository.Add(loan);
            return loan;
        }

        public void ReturnBook(Loan loan)
        {
            if(loan is null)
                return;

            loan.ReturnAt = DateTime.Now;
            _repository.Remove(loan);
        }

        // возможно, реализовать по-другому
        private bool IsFree(BookCopy copy) =>
            !(_repository.GetAll()
            .Any(l => l.Copy == copy &&
                l.ReturnAt == null));
    }
}
