using PaperBooks.Models;

namespace PaperBooks.Data
{
    public class ReadersRepositoryStub : IReadersRepository
    {
        private readonly List<Reader> _readers =
        [
            new Reader { Id = 1, Name = "Иванов" },
            new Reader { Id = 2, Name = "Петров" }
        ];

        public IEnumerable<Reader> GetAll()
            => _readers;

        public Reader? GetById(int id)
            => _readers.Find(r => r.Id == id);
    }
}
