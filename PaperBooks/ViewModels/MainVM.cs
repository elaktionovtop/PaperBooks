using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using PaperBooks.Models;
using PaperBooks.Services;

using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.ViewModels
{
    public sealed partial class MainVM : ObservableObject
    {
        [ObservableProperty]
        private WorkspaceVM? _currentWorkspace;

        private readonly ReadersWorkspaceVM readersVM =
            new(new InMemoryReadersService());

        private readonly BooksWorkspaceVM booksVM = 
            new(new InMemoryBooksService());

        private readonly LoansWorkspaceVM loansVM = 
            new(new InMemoryLoansService());

        [ObservableProperty]
        private Reader? _currentReader;

        public MainVM()
        {
            ActivateReaders();
        }

        [RelayCommand]
        private void ActivateReaders() =>
            CurrentWorkspace = readersVM;

        [RelayCommand]
        private void ActivateBooks() =>
            CurrentWorkspace = booksVM;

        [RelayCommand]
        private void ActivateLoans() =>
            CurrentWorkspace = loansVM;
    }
}
