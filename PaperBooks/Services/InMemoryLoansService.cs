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
    }
}
