using PaperBooks.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Data
{
    public class ReadersRepositoryStub : IReadersRepository
    {
        private readonly List<Reader> _readers =
        [
            new Reader { Id = 1, Name = "Иванов" },
            new Reader { Id = 2, Name = "Петров" }
        ];

        private readonly Dictionary<int, List<Book>> _booksByReader =
            new()
            {
                [1] =
                [
                    new Book { Id = 1, Title = "CLR via C#" },
                new Book { Id = 2, Title = "Clean Code" }
                ],
                [2] =
                [
                    new Book { Id = 3, Title = "Design Patterns" }
                ]
            };

        public IEnumerable<Reader> GetAll()
            => _readers;

        public Reader? GetById(int id)
            => _readers.Find(r => r.Id == id);
        /*
        public IEnumerable<Book> GetBooksByReaderId(int readerId)
            => _booksByReader.TryGetValue(readerId, out var books)
                ? books
                : Enumerable.Empty<Book>();
        */
    }
}
