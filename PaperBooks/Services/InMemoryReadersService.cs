using PaperBooks.Models;

namespace PaperBooks.Services
{
    public class InMemoryReadersService : IReadersService
    {
        private readonly List<Book> _books = [];

        public InMemoryReadersService()
        {
        }

        public InMemoryReadersService(IEnumerable<Book> books)
        {
            _books.AddRange(books);
        }

        public IEnumerable<Reader> GetAll()
        {
            return [];
        }

        public Reader? GetById(int id)
        {
            return null;
        }

        public IEnumerable<Book> GetBooksOfReader(Reader reader)
            => _books;
    }
}
