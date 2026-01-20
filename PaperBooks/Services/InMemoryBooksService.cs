using PaperBooks.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Services
{
    public class InMemoryBooksService : IBooksService
    {
        private readonly List<Reader> _readers = [];

        public InMemoryBooksService()
        {
        }

        public InMemoryBooksService(IEnumerable<Reader> readers)
        {
            _readers.AddRange(readers);
        }

        public IEnumerable<Reader> GetReadersReservedBook(Book book)
            => _readers;
    }
}
