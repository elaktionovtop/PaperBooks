using PaperBooks.Models;

namespace PaperBooks.Services
{
    public class InMemoryReadersService : IReadersService
    {
        public InMemoryReadersService()
        {
        }

        public IEnumerable<Reader> GetAll()
        {
            return [];
        }

        public Reader? GetById(int id)
        {
            return null;
        }
    }
}
