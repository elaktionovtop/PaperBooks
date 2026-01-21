using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using PaperBooks.Models;
using PaperBooks.Services;

using System.Collections.ObjectModel;

namespace PaperBooks.ViewModels
{
    public sealed partial class MainVM : ObservableObject
    {
        private readonly IReadersService _readersService;
        private readonly IBooksService _booksService;
        private readonly ILoansService _loansService;
        private readonly IReservationsService _reservationsService;

        public ObservableCollection<Reader> Readers { get; } = [];
        public ObservableCollection<BookCopy> ReaderBookCopies { get; } = [];

        public ObservableCollection<Book> Books { get; } = [];
        public ObservableCollection<Reader> ReadersReservedBook { get; } = [];

        public ObservableCollection<Loan> ReaderLoans { get; } = [];

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
            ILoansService loansService
            )
        {
            _readersService = readersService;
            _booksService = booksService;
            _loansService = loansService;
            _reservationsService = reservationsService;

            LoadInitialData();
        }

        private void LoadInitialData()
        {
            // Пока заглушки — реальные источники подключишь позже
            // Readers / Books могут быть загружены извне
        }

        partial void OnCurrentReaderChanged(Reader? value)
        {
            RefreshReaderBookCopies(value);
            RefreshReaderLoans(value);
        }

        private void RefreshReaderBookCopies(Reader? reader)
        {
            ReaderBookCopies.Clear();

            if(reader is null)
                return;

            foreach(var bookCopy in _loansService.GetReaderBookCopies(reader))
            {
                ReaderBookCopies.Add(bookCopy);
            }
        }

        private void RefreshReaderLoans(Reader? reader)
        {
            ReaderLoans.Clear();

            if(reader is null)
                return;

            foreach(var loan in _loansService.GetReaderLoans(reader))
            {
                ReaderLoans.Add(loan);
            }

            CurrentLoan = ReaderLoans.FirstOrDefault();
        }

        partial void OnCurrentBookChanged(Book? value)
        {
            RefreshReadersReservedBook(value);
        }

        private void RefreshReadersReservedBook(Book? book)
        {
            ReadersReservedBook.Clear();

            if(book is null)
                return;

            foreach(var reader in _reservationsService.GetReadersReservedBook(book))
            {
                ReadersReservedBook.Add(reader);
            }
        }

        [RelayCommand(CanExecute = nameof(CanIssueBook))]
        private void IssueBook()
        {
            var loan = _loansService.IssueBook(CurrentReader!, CurrentBook!);
            RefreshReaderLoans(CurrentReader);
            CurrentLoan = loan;
        }

        private bool CanIssueBook() =>
            CurrentReader != null &&
            CurrentBook != null;

        [RelayCommand(CanExecute = nameof(CanReturnBook))]
        private void ReturnBook()
        {
            _loansService.ReturnBook(CurrentLoan!);
            RefreshReaderLoans(CurrentReader);
        }

        private bool CanReturnBook() =>
            CurrentLoan != null;
    }
}


