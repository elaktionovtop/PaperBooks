using PaperBooks.Models;

using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace PaperBooks.Services
{
    public sealed class InMemoryLoansService : ILoansService
    {
        private readonly List<Loan> _loans = [];
        private readonly List<Reservation> _reservations = [];       //

        public InMemoryLoansService()
        {
        }

        public InMemoryLoansService(IEnumerable<Loan> loans,
            IEnumerable<Reservation> reservations)
        {
            _loans.AddRange(loans);               
            _reservations.AddRange(reservations);               //
        }

        public IEnumerable<Loan> GetActiveLoans()
            => _loans;

        public Loan IssueBook(Reader reader, Book book)   
        {
            var freeCopy = book.Copies
                .FirstOrDefault(c => IsFree(c)) 
                ?? throw new InvalidOperationException
                    ("Нет свободных экземпляров");
            RemoveReservation(book, reader);                      //

            var loan = new Loan
            {
                Reader = reader,
                Copy = freeCopy,
                IssuedAt = DateTime.Now
            };

            _loans.Add(loan);
            return loan;
        }

        public void ReturnBook(Loan loan)         //
        {
            loan.ReturnAt = DateTime.Now;
            _loans.Remove(loan);
            RemoveReservation(loan.Copy.Book,
                loan.Reader);
        }

        public bool IsFree(BookCopy copy)
            => !_loans.Any(l => l.Copy == copy && l.ReturnAt == null);

        private void RemoveReservation(Book book, Reader reader)           //
       => _reservations.RemoveAll(r => r.Book == book && r.Reader == reader);
    }
}
