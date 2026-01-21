using PaperBooks.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Services
{
    public interface ILoansService
    {
        IEnumerable<Loan> GetActiveLoans();
        IEnumerable<Loan> GetReaderLoans(Reader reader);
        IEnumerable<BookCopy> GetReaderBookCopies(Reader reader);
        Loan IssueBook(Reader reader, Book book);
        void ReturnBook(Loan loan);
    }
}
