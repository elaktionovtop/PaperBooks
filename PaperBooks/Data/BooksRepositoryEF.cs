using Microsoft.EntityFrameworkCore;

using PaperBooks.Models;

namespace PaperBooks.Data
{
    public sealed class BooksRepositoryEF : IBooksRepository
    {
        private readonly LibraryDbContext _db;

        public BooksRepositoryEF(LibraryDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> GetAll() =>
            _db.Books
            .Include(b => b.Copies)
            .ToList();

        public Book? GetById(int id) =>
            _db.Books
            .Include(b => b.Copies)
            .FirstOrDefault(b => b.Id == id);
    }
}
