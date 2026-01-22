using PaperBooks.Data;
using PaperBooks.Models;

namespace PaperBooks.Services
{
    public class ReadersService : IReadersService
    {
        private readonly IReadersRepository _repository;

        public ReadersService(IReadersRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Reader> GetAll() =>
            _repository.GetAll();

        public Reader? GetById(int id) => 
            _repository.GetById(id);
    }
}
