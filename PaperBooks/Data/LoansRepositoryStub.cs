using PaperBooks.Models;

namespace PaperBooks.Data
{
    public sealed class LoansRepositoryStub : ILoansRepository
    {
        private readonly List<Loan> _loans = [];

        public LoansRepositoryStub(IEnumerable<Loan>? seed = null)
        {
            if(seed != null)
                _loans.AddRange(seed);
        }

        public IEnumerable<Loan> GetAll()
            => _loans;

        public void Add(Loan loan)
            => _loans.Add(loan);

        public void Remove(Loan loan)
            => _loans.Remove(loan);
    }
}
