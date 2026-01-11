using PaperBooks.Models;
using PaperBooks.Services;
using PaperBooks.ViewModels;

namespace Tests.ViewModels.Workspaces
{
    public sealed class ReadersWorkspaceVMTests
    {
        [Fact]
        public void Load_books_when_reader_selected()
        {
            var books = new[]
            {
                new Book { Title = "B1" },
                new Book { Title = "B2" }
            };

            var service = new InMemoryReadersService(books);
            var vm = new ReadersWorkspaceVM(service);
            var reader = new Reader { Name = string.Empty };

            vm.CurrentReader = reader;

            Assert.Equal(2, vm.ReaderBooks.Count);
        }

        [Fact]
        public void Clear_books_when_reader_has_no_books()
        {
            var service = new InMemoryReadersService([]);
            var vm = new ReadersWorkspaceVM(service);

            var reader = new Reader { Name = "EmptyReader" };

            vm.CurrentReader = reader;

            Assert.Empty(vm.ReaderBooks);
        }

        [Fact]
        public void Clear_books_when_current_reader_is_null()
        {
            var books = new[]
            {
                new Book { Title = "B1" },
                new Book { Title = "B2" }
            };

            var service = new InMemoryReadersService();
            var vm = new ReadersWorkspaceVM(service);

            vm.CurrentReader = null;

            Assert.Empty(vm.ReaderBooks);
        }
    }
}
