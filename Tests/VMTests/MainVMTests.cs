using PaperBooks.Models;
using PaperBooks.Services;
using PaperBooks.ViewModels;

using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.VMTests
{
    public sealed class MainVMTests
    {
        [Fact]
        public void SelectingReader_LoadsBooksAndLoans()
        {
            var reader = new Reader { Name = "Ivan" };
            var book = new Book { Title = "CLR via C#" };
            var copy = new BookCopy(book);
            book.Copies.Add(copy);

            var loan = new Loan
            {
                Reader = reader,
                Copy = copy,
                IssuedAt = DateTime.Now
            };

            var readersService = new InMemoryReadersService(new[] { book });
            var booksService = new InMemoryBooksService();
            var loansService = new InMemoryLoansService(new[] { loan }, []);

            var vm = new MainVM(readersService, booksService, loansService);

            vm.CurrentReader = reader;

            Assert.Single(vm.Books);
            Assert.Single(vm.Loans);
        }

        [Fact]
        public void IssueBook_AddsLoan()
        {
            var reader = new Reader { Name = "Ivan" };
            var book = new Book { Title = "WPF" };
            var copy = new BookCopy(book);
            book.Copies.Add(copy);

            var readersService = new InMemoryReadersService(new[] { book });
            var booksService = new InMemoryBooksService();
            var loansService = new InMemoryLoansService();

            var vm = new MainVM(readersService, booksService, loansService)
            {
                CurrentReader = reader,
                CurrentBook = book
            };

            vm.IssueBookCommand.Execute(null);

            Assert.Single(vm.Loans);
            Assert.NotNull(vm.CurrentLoan);
        }

        [Fact]
        public void ReturnBook_RemovesLoan()
        {
            var reader = new Reader { Name = "Ivan" };
            var book = new Book { Title = "LINQ" };
            var copy = new BookCopy(book);
            book.Copies.Add(copy);

            var loan = new Loan
            {
                Reader = reader,
                Copy = copy,
                IssuedAt = DateTime.Now
            };

            var readersService = new InMemoryReadersService(new[] { book });
            var booksService = new InMemoryBooksService();
            var loansService = new InMemoryLoansService(new[] { loan }, []);

            var vm = new MainVM(readersService, booksService, loansService)
            {
                CurrentReader = reader
            };

            vm.CurrentLoan = vm.Loans.Single();
            vm.ReturnBookCommand.Execute(null);

            Assert.Empty(vm.Loans);
        }

        [Fact]
        public void CannotIssueBook_WithoutReaderOrBook()
        {
            var vm = new MainVM(
                new InMemoryReadersService(),
                new InMemoryBooksService(),
                new InMemoryLoansService());

            Assert.False(vm.IssueBookCommand.CanExecute(null));

            vm.CurrentReader = new Reader();
            Assert.False(vm.IssueBookCommand.CanExecute(null));

            vm.CurrentBook = new Book();
            Assert.True(vm.IssueBookCommand.CanExecute(null));
        }
    }
}
