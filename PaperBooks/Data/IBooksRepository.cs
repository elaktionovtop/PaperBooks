using PaperBooks.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Data
{
    public interface IBooksRepository
    {
        IEnumerable<Book> GetAll();
        Book? GetById(int id);
        IEnumerable<BookCopy> GetCopiesByReaderId(int readerId);
    }
}
