using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using PaperBooks.Models;
using PaperBooks.Services;

using System.Collections.ObjectModel;
using System.Windows.Data;

namespace PaperBooks.ViewModels
{
    public sealed partial class MainVM : ObservableObject
    {
        private readonly IReadersService _readersService;
        private readonly IBooksService _booksService;
        private readonly ILoansService _loansService;
        private readonly IReservationsService _reservationsService;

        public ObservableCollection<Reader> Readers { get; } = new();
        public ObservableCollection<Book> Books { get; } = new();
        public ObservableCollection<Loan> ReaderLoans { get; } = new();
        public ObservableCollection<Reader> ReadersReservedBook { get; } = new();

        [ObservableProperty]
        private Reader? _currentReader;

        [ObservableProperty]
        private Book? _currentBook;

        [ObservableProperty]
        private Loan? _currentLoan;

        public MainVM(
            IReadersService readersService,
            IBooksService booksService,
            IReservationsService reservationsService,
            ILoansService loansService)
        {
            _readersService = readersService;
            _booksService = booksService;
            _loansService = loansService;
            _reservationsService = reservationsService;

            LoadInitialData();
        }

        private void LoadInitialData()
        {
            Readers.Clear();
            foreach(var reader in _readersService.GetAll())
                Readers.Add(reader);

            Books.Clear();
            foreach(var book in _booksService.GetAll())
                Books.Add(book);

            CurrentReader = Readers.FirstOrDefault();
            CurrentBook = Books.FirstOrDefault();
        }

        partial void OnCurrentReaderChanged(Reader? value)
        {
            RefreshReaderLoans(value);
            IssueBookCommand.NotifyCanExecuteChanged();
            ReturnBookCommand.NotifyCanExecuteChanged();
        }

        private void RefreshReaderLoans(Reader? reader)
        {
            ReaderLoans.Clear();

            if(reader is null)
                return;

            foreach(var loan in _loansService.GetReaderLoans(reader))
                ReaderLoans.Add(loan);

            CurrentLoan = ReaderLoans.FirstOrDefault();
        }

        partial void OnCurrentBookChanged(Book? value)
        {
            RefreshReadersReservedBook(value);
            IssueBookCommand.NotifyCanExecuteChanged();
            ReturnBookCommand.NotifyCanExecuteChanged();
        }

        private void RefreshReadersReservedBook(Book? book)
        {
            ReadersReservedBook.Clear();

            if(book is null)
                return;

            foreach(var reader in _reservationsService.GetReadersReservedBook(book))
                ReadersReservedBook.Add(reader);
        }

        [RelayCommand(CanExecute = nameof(CanIssueBook))]
        private void IssueBook()
        {
            if(CurrentReader is null || CurrentBook is null)
                return;

            var loan = _loansService.IssueBook(CurrentReader, CurrentBook);
            RefreshReaderLoans(CurrentReader);
            RefreshReadersReservedBook(CurrentBook);
            CurrentLoan = loan;
            IssueBookCommand.NotifyCanExecuteChanged();
            ReturnBookCommand.NotifyCanExecuteChanged();
        }

        private bool CanIssueBook() =>
            CurrentReader != null &&
            CurrentBook != null &&
            _loansService.IsAnyCopyFree(CurrentBook);

        [RelayCommand(CanExecute = nameof(CanReturnBook))]
        private void ReturnBook()
        {
            if(CurrentLoan is null)
                return;

            _loansService.ReturnBook(CurrentLoan);
            RefreshReaderLoans(CurrentReader);
            IssueBookCommand.NotifyCanExecuteChanged();
            ReturnBookCommand.NotifyCanExecuteChanged();
        }

        private bool CanReturnBook() =>
            CurrentLoan != null;
    }
}


