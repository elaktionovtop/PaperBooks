using PaperBooks.Models;

namespace PaperBooks.Services
{
    public interface IBooksService
    {
        IEnumerable<Book> GetAll();
        Book? GetById(int id);
    }
}
