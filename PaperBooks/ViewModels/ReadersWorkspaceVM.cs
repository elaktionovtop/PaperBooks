using CommunityToolkit.Mvvm.ComponentModel;

using PaperBooks.Models;
using PaperBooks.Services;

using System.Collections.ObjectModel;

namespace PaperBooks.ViewModels
{
    public sealed partial class ReadersWorkspaceVM : WorkspaceVM
    {
        private readonly IReadersService _service;

        public ObservableCollection<Reader> Readers { get; } = new();
        public ObservableCollection<Book> ReaderBooks { get; } = new();

        [ObservableProperty]
        private Reader? _currentReader;

        public ReadersWorkspaceVM(IReadersService service)
        {
            _service = service;
        }

        partial void OnCurrentReaderChanged(Reader? value)
        {
            ReaderBooks.Clear();

            if(value is null)
                return;

            foreach(var book in _service.GetBooksOfReader(value))
                ReaderBooks.Add(book);
        }

        public void Refresh()
        {
            //Loans.Clear();
            //foreach(var loan in _loansService.GetLoansFor(CurrentReader))
            //    Loans.Add(loan);
        }

    }
}
