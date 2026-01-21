using PaperBooks.Models;
using PaperBooks.Services;
using PaperBooks.ViewModels;

namespace Tests.VMTests
{
    public sealed class MainVMTests
    {
        [Fact]
        public void SelectingReader_LoadsCopiesAndLoans()
        {
            var reader = new Reader { Name = "Ivan" };

            var book = new Book { Title = "CLR via C#" };
            var copy = new BookCopy { Book = book };
            book.Copies.Add(copy);

            var loan = new Loan
            {
                Reader = reader,
                Copy = copy,
                IssuedAt = DateTime.Now
            };

            var reservationsService =
                new InMemoryReservationsService([]);

            var loansService =
                new InMemoryLoansService(
                    reservationsService,
                    new[] { loan },
                    new[] { copy });

            var vm = new MainVM(
                new InMemoryReadersService(),
                new InMemoryBooksService(),
                reservationsService,
                loansService)
            {
            };
            vm.CurrentReader = reader;

            Assert.Single(vm.ReaderBookCopies);
            Assert.Single(vm.ReaderLoans);
        }

        [Fact]
        public void IssueBook_AddsLoan()
        {
            var reader = new Reader { Name = "Ivan" };

            var book = new Book { Title = "WPF" };
            var copy = new BookCopy { Book = book };
            book.Copies.Add(copy);

            var reservationsService =
                new InMemoryReservationsService([]);

            var loansService =
                new InMemoryLoansService(
                    reservationsService,
                    [],
                    new[] { copy });

            var vm = new MainVM(
                new InMemoryReadersService(),
                new InMemoryBooksService(),
                reservationsService,
                loansService)
            {
                CurrentReader = reader,
                CurrentBook = book
            };

            vm.IssueBookCommand.Execute(null);

            Assert.Single(vm.ReaderLoans);
            Assert.NotNull(vm.CurrentLoan);
        }

        [Fact]
        public void ReturnBook_RemovesLoan()
        {
            var reader = new Reader { Name = "Ivan" };

            var book = new Book { Title = "LINQ" };
            var copy = new BookCopy { Book = book };
            book.Copies.Add(copy);

            var loan = new Loan
            {
                Reader = reader,
                Copy = copy,
                IssuedAt = DateTime.Now
            };

            var reservationsService =
                new InMemoryReservationsService([]);

            var loansService =
                new InMemoryLoansService(
                    reservationsService,
                    new[] { loan },
                    new[] { copy });

            var vm = new MainVM(
                new InMemoryReadersService(),
                new InMemoryBooksService(),
                reservationsService,
                loansService)
            {
                CurrentReader = reader,
                CurrentLoan = loan
            };

            vm.ReturnBookCommand.Execute(null);

            Assert.Empty(vm.ReaderLoans);
        }

        [Fact]
        public void CannotIssueBook_WithoutReaderOrBook()
        {
            var reservationsService =
                new InMemoryReservationsService([]);

            var loansService =
                new InMemoryLoansService(
                    reservationsService,
                    [],
                    []);

            var vm = new MainVM(
                new InMemoryReadersService(),
                new InMemoryBooksService(),
                reservationsService,
                loansService);

            Assert.False(vm.IssueBookCommand.CanExecute(null));

            vm.CurrentReader = new Reader();
            Assert.False(vm.IssueBookCommand.CanExecute(null));

            vm.CurrentBook = new Book();
            Assert.True(vm.IssueBookCommand.CanExecute(null));
        }
    }
}
