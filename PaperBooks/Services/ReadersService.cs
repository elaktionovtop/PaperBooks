using PaperBooks.Data;
using PaperBooks.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Services
{
    public class ReadersService : IReadersService
    {
        private readonly IReadersRepository _repository;

        public ReadersService(IReadersRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Reader> GetAll()
            => _repository.GetAll();

        public Reader? GetById(int id)
        {
            if (id <= 0)
                return null;

            return _repository.GetById(id);
        }
    }
}
