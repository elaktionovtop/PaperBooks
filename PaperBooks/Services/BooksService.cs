using PaperBooks.Data;
using PaperBooks.Models;

namespace PaperBooks.Services
{
    public sealed class BooksService : IBooksService
    {
        private readonly IBooksRepository _repository;

        public BooksService(IBooksRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Book> GetAll()
            => _repository.GetAll();

        public Book? GetById(int id)
        {
            if(id <= 0)
                return null;

            return _repository.GetById(id);
        }
    }
}
