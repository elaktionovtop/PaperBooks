using Microsoft.EntityFrameworkCore;

using PaperBooks.Models;

namespace PaperBooks.Data
{
    public sealed class ReadersRepositoryEF : IReadersRepository
    {
        private readonly LibraryDbContext _db;

        public ReadersRepositoryEF(LibraryDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Reader> GetAll() =>
            _db.Readers.ToList();

        public Reader? GetById(int id) =>
            _db.Readers.Find(id);
    }
}
