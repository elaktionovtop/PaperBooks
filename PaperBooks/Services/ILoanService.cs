using PaperBooks.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Services
{
    public interface ILoansService
    {
        IEnumerable<Loan> GetActiveLoans();
        Loan IssueBook(Reader reader, Book book);   //
        void ReturnBook(Loan loan);                 //
    }
}
