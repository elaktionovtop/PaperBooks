using PaperBooks.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Services
{
    public class InMemoryBooksService : IBooksService
    {
        public InMemoryBooksService()
        {
        }

        public IEnumerable<Book> GetAll()
        {
            return [];
        }

        public Book? GetById(int id)
        {
            return null;
        }
    }
}
