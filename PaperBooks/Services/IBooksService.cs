using PaperBooks.Models;

namespace PaperBooks.Services
{
    public interface IBooksService
    {
        IEnumerable<Reader> GetBookedBookReaders(Book book);
    }
}
