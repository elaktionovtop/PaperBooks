using CommunityToolkit.Mvvm.ComponentModel;

using PaperBooks.Models;
using PaperBooks.Services;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PaperBooks.ViewModels
{
    public sealed partial class BooksWorkspaceVM : WorkspaceVM
    {
        private readonly IBooksService _service;

        public ObservableCollection<Book> Books { get; } = new();
        public ObservableCollection<Reader> BookBookedReaders { get; } = new();

        [ObservableProperty]
        private Book? _currentBook;

        public BooksWorkspaceVM(IBooksService service)
        {
            _service = service;
        }

        partial void OnCurrentBookChanged(Book? value)
        {
            BookBookedReaders.Clear();

            if(value is null)
                return;

            foreach(var reader in _service.GetBookedBookReaders(value))
                BookBookedReaders.Add(reader);
        }
    }
}
