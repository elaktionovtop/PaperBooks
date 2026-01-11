using PaperBooks.Models;

namespace PaperBooks.Services
{
    public interface IReadersService
    {
        IEnumerable<Book> GetBooksOfReader(Reader reader);
    }
}
