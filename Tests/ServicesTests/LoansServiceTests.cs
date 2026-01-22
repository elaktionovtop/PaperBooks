using PaperBooks.Data;
using PaperBooks.Models;
using PaperBooks.Services;
/*
выдача книги
нет свободных копий
возврат книги
книги читателя

? создание сервиса
*/
namespace Tests.ServiceTests
{
    public sealed class LoansServiceTests
    {
        [Fact]
        public void IssueBook_CreatesLoan()
        {
            var reader = new Reader { Id = 1, Name = "Иванов" };

            var book = new Book { Id = 1, Title = "CLR" };
            var copy = new BookCopy { Book = book };
            book.Copies.Add(copy);

            var repository = new LoansRepositoryStub();
            var reservations =
                new InMemoryReservationsService([]);

            var service =
                new LoansService(repository, reservations);

            var loan = service.IssueBook(reader, book);

            Assert.Single(repository.GetAll());
            Assert.Equal(reader.Id, loan.Reader.Id);
            Assert.Equal(copy, loan.Copy);
        }

        [Fact]
        public void IssueBook_WithoutFreeCopies_Throws()
        {
            var reader = new Reader { Id = 1 };

            var book = new Book { Id = 1 };
            var copy = new BookCopy { Book = book };
            book.Copies.Add(copy);

            var existingLoan = new Loan
            {
                Reader = reader,
                Copy = copy,
                IssuedAt = DateTime.Now
            };

            var repository =
                new LoansRepositoryStub([existingLoan]);

            var reservations =
                new InMemoryReservationsService([]);

            var service =
                new LoansService(repository, reservations);

            Assert.Throws<InvalidOperationException>(
                () => service.IssueBook(reader, book));
        }

        [Fact]
        public void ReturnBook_RemovesLoan()
        {
            var reader = new Reader { Id = 1 };

            var book = new Book { Id = 1 };
            var copy = new BookCopy { Book = book };
            book.Copies.Add(copy);

            var loan = new Loan
            {
                Reader = reader,
                Copy = copy,
                IssuedAt = DateTime.Now
            };

            var repository =
                new LoansRepositoryStub([loan]);

            var reservations =
                new InMemoryReservationsService([]);

            var service =
                new LoansService(repository, reservations);

            service.ReturnBook(loan);

            Assert.Empty(repository.GetAll());
            Assert.NotNull(loan.ReturnAt);
        }

        [Fact]
        public void GetReaderLoans_ReturnsOnlyReadersLoans()
        {
            var reader1 = new Reader { Id = 1 };
            var reader2 = new Reader { Id = 2 };

            var book = new Book { Id = 1 };
            var copy1 = new BookCopy { Book = book };
            var copy2 = new BookCopy { Book = book };
            book.Copies.Add(copy1);
            book.Copies.Add(copy2);

            var loan1 = new Loan
            {
                Reader = reader1,
                Copy = copy1,
                IssuedAt = DateTime.Now
            };

            var loan2 = new Loan
            {
                Reader = reader2,
                Copy = copy2,
                IssuedAt = DateTime.Now
            };

            var repository =
                new LoansRepositoryStub([loan1, loan2]);

            var reservations =
                new InMemoryReservationsService([]);

            var service =
                new LoansService(repository, reservations);

            var loans = service.GetReaderLoans(reader1).ToList();

            Assert.Single(loans);
            Assert.Equal(reader1.Id, loans[0].Reader.Id);
        }
    }
}
