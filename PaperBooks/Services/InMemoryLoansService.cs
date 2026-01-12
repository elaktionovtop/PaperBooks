using PaperBooks.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Services
{
    public sealed class InMemoryLoansService : ILoansService
    {
        private readonly List<Loan> _loans = [];

        public InMemoryLoansService()
        {
        }

        public InMemoryLoansService(IEnumerable<Loan> loans)
        {
            _loans.AddRange(loans);
        }

        public IEnumerable<Loan> GetActiveLoans()
            => _loans;

        public Loan IssueBook(Reader reader, Book book)   
        {
            var freeCopy = book.Copies
                .FirstOrDefault(c => IsFree(c));

            if(freeCopy == null)
                throw new InvalidOperationException("Нет свободных экземпляров");

            var loan = new Loan
            {
                Reader = reader,
                //Book = book,
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
        }

        public bool IsFree(BookCopy copy)
            => !_loans.Any(l => l.Copy == copy && l.ReturnAt == null);
    }
}
