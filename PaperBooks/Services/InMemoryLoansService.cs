using PaperBooks.Models;

namespace PaperBooks.Services
{
    public sealed class InMemoryLoansService : ILoansService
    {
        private readonly IReservationsService _reservationsService;

        private readonly List<Loan> _loans = [];
        private readonly List<BookCopy> _copies = [];

        public InMemoryLoansService(IReservationsService reservationsService,
            IEnumerable<Loan> loans,
            IEnumerable<BookCopy> copies)
        {
            _reservationsService = reservationsService;
            _loans.AddRange(loans);
            _copies.AddRange(copies);
        }

        public IEnumerable<Loan> GetActiveLoans()
            => _loans;

        public IEnumerable<Loan> GetReaderLoans(Reader reader)
            => _loans.Where(l => l.Reader == reader);

        public IEnumerable<BookCopy> GetReaderBookCopies(Reader reader)
            => _copies;

        public Loan IssueBook(Reader reader, Book book)   
        {
            var freeCopy = book.Copies
                .FirstOrDefault(c => IsFree(c)) 
                ?? throw new InvalidOperationException
                    ("Нет свободных экземпляров");

            _reservationsService.RemoveReservation(book, reader);

            var loan = new Loan
            {
                Reader = reader,
                Copy = freeCopy,
                IssuedAt = DateTime.Now
            };

            _loans.Add(loan);
            return loan;
        }

        public void ReturnBook(Loan loan)
        {
            loan.ReturnAt = DateTime.Now;
            _loans.Remove(loan);
        }

        public bool IsFree(BookCopy copy)
            => !_loans.Any(l => l.Copy == copy && l.ReturnAt == null);
    }
}
