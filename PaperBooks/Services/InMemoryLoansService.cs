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
                IssuedAt = DateOnly.FromDateTime(DateTime.Now)
            };

            _loans.Add(loan);
            return loan;
        }

        public void ReturnBook(Loan loan)
        {
            loan.ReturnAt = DateOnly.FromDateTime(DateTime.Now);
            _loans.Remove(loan);
        }

        public bool IsAnyCopyFree(Book book)
        {
            if(book is null) return false;

            // Получаем все активные копии один раз
            var activeCopies = GetActiveLoans()
                .Select(l => l.Copy)
                .ToHashSet();

            // Проверяем, есть ли копия книги, которой нет среди активных
            return book.Copies.Any(c => !activeCopies.Contains(c));
        }

        public bool IsFree(BookCopy copy)
            => !_loans.Any(l => l.Copy == copy && l.ReturnAt == null);
    }
}
