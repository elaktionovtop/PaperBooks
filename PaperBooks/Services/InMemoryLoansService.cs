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

        public Loan IssueLoan(Reader reader, Book book)   //
        {
            var loan = new Loan
            {
                Reader = reader,
                Book = book,
                IssuedAt = DateTime.Now
            };

            _loans.Add(loan);
            return loan;
        }

        public void ReturnLoan(Loan loan)         //
        {
            loan.ReturnAt = DateTime.Now;
        }
    }
}
