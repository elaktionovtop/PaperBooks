using PaperBooks.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Services
{
    public class ReadersService : IReadersService
    {
        public IEnumerable<Reader> GetAll()
        {
            return [];
        }

        public Reader? GetById(int id)
        {
            return null;
        }

        public IEnumerable<Book> GetBooksOfReader(Reader reader)
        {
            return [];
        }
}
}
