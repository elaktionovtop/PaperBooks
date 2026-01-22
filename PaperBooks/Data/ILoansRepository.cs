using PaperBooks.Models;

namespace PaperBooks.Data
{
    public interface ILoansRepository
    {
        IEnumerable<Loan> GetAll();
        void Add(Loan loan);
        void Remove(Loan loan);
    }
}
