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

        public ReadersServiceTests()
        {
            _service = new ReadersService(); // заглушка
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

        [Fact]
        public void GetBooksOfReader_ForValidReader_ReturnsCollection()
        {
            // arrange
            var reader = _service.GetAll().First();

            // act
            var books = _service.GetBooksOfReader(reader);

            // assert
            Assert.NotNull(books);
        }

        [Fact]
        public void GetBooksOfReader_ForUnknownReader_ReturnsEmpty()
        {
            // arrange
            var reader = new Reader { Id = -1 };

            // act
            var books = _service.GetBooksOfReader(reader);

            // assert
            Assert.NotNull(books);
            Assert.Empty(books);
        }
    }
}
