using PaperBooks.Models;
using PaperBooks.Services;
using PaperBooks.ViewModels;

namespace Tests.ViewModels.Workspaces
{
    public sealed class BooksWorkspaceVMTests
    {
        [Fact]
        public void Load_readers_when_book_selected()
        {
            var readers = new[]
            {
                new Reader { Name = "R1" },
                new Reader { Name = "R2" }
            };

            var service = new InMemoryBooksService(readers);
            var vm = new BooksWorkspaceVM(service);
            var book = new Book { Title = string.Empty };

            vm.CurrentBook = book;

            Assert.Equal(2, vm.BookBookedReaders.Count);
        }

        [Fact]
        public void Load_readers_when_book_has_no_booked_readers()
        {
            var service = new InMemoryBooksService([]);
            var vm = new BooksWorkspaceVM(service);
            var book = new Book { Title = "EmptyBooking" };

            vm.CurrentBook = book;

            Assert.Empty(vm.BookBookedReaders);
        }

        [Fact]
        public void Load_readers_when_current_book_is_null()
        {
            var readers = new[]
            {
                new Reader { Name = "R1" },
                new Reader { Name = "R2" }
            };

            var service = new InMemoryBooksService();
            var vm = new BooksWorkspaceVM(service);

            vm.CurrentBook = null;

            Assert.Empty(vm.BookBookedReaders);
        }
    }
}
