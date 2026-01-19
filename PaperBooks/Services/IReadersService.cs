using PaperBooks.Models;

namespace PaperBooks.Services
{
    public interface IReadersService
    {
        IEnumerable<Reader> GetAll();
        Reader? GetById(int id);

        IEnumerable<Book> GetBooksOfReader(Reader reader);
    }
}
