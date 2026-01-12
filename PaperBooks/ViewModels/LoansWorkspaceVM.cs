using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using PaperBooks.Models;
using PaperBooks.Services;
using PaperBooks.ViewModels;

using System.Collections.ObjectModel;

public sealed partial class LoansWorkspaceVM : WorkspaceVM
{
    private readonly ILoansService _loansService;
    private readonly List<Loan> _allLoans;

    public ObservableCollection<Loan> Loans { get; } = new();

    [ObservableProperty]
    private Loan? _currentLoan;

    [ObservableProperty]
    private Reader? _currentReader;

    [ObservableProperty]
    private Book? _currentBook;

    public LoansWorkspaceVM(ILoansService loansService)
    {
        _loansService = loansService;
        _allLoans = _loansService.GetActiveLoans().ToList();

        ApplyFilter();
    }

    partial void OnCurrentReaderChanged(Reader? value)
        => ApplyFilter();

    private void ApplyFilter()
    {
        Loans.Clear();
        CurrentLoan = null;

        if (CurrentReader != null)
        {
            var query = _allLoans.Where(loan => loan.Reader == CurrentReader);
            foreach (var loan in query)
                Loans.Add(loan);
            
            if(Loans.Count > 0)
            {
                CurrentLoan = Loans.First();
            }
        }
    }

    [RelayCommand(CanExecute = nameof(CanIssueBook))]       //
    private void IssueBook()
    {
        var loan = _loansService.IssueLoan(CurrentReader!, CurrentBook!);
        _allLoans.Add(loan);
        ApplyFilter();
        CurrentLoan = loan;
    }

    //
    private bool CanIssueBook() =>                      
        CurrentReader != null &&
        CurrentBook != null;
}
