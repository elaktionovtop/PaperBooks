using PaperBooks.Models;

namespace PaperBooks.Data
{
    public sealed class BooksRepositoryStub : IBooksRepository
    {
        private readonly List<Book> _books =
        [
            Create("CLR via C#", 1, 2),
            Create("WPF", 2, 1)
        ];

        private static Book Create(string title, int id, int copiesCount)
        {
            var book = new Book
            {
                Id = id,
                Title = title
            };

            for(int i = 1; i <= copiesCount; i++)
            {
                var copy = new BookCopy
                {
                    InventoryNumber = $"{id}-{i}",
                    Book = book
                };
                book.Copies.Add(copy);
            }

            return book;
        }

        public IEnumerable<Book> GetAll()
            => _books;

        public Book? GetById(int id)
            => _books.Find(b => b.Id == id);
    }
}
