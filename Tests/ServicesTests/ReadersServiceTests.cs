using PaperBooks.Data;
using PaperBooks.Models;
using PaperBooks.Services;

using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.ServicesTests
{
    public class ReadersServiceTests
    {
        private readonly IReadersService _service;         
        private readonly IReadersRepository _repository;

        public ReadersServiceTests()
        {
            _repository = new ReadersRepositoryStub();
            _service = new ReadersService(_repository); // заглушка
        }

        [Fact]
        public void GetAllReaders_Returns_NotEmpty()
        {
            // act
            var readers = _service.GetAll();

            // assert
            Assert.NotNull(readers);
            Assert.NotEmpty(readers);
        }
    }
}
