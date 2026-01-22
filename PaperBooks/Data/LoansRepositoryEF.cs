using Microsoft.EntityFrameworkCore;

using PaperBooks.Models;

namespace PaperBooks.Data
{
    public sealed class LoansRepositoryEF : ILoansRepository
    {
        private readonly LibraryDbContext _db;

        public LoansRepositoryEF(LibraryDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Loan> GetAll() =>
            _db.Loans
            .Include(l => l.Reader)
            .Include(l => l.Copy)
                .ThenInclude(c => c.Book)
            .ToList();

        public void Add(Loan loan)
        {
            _db.Loans.Add(loan);
            _db.SaveChanges();
        }

        public void Remove(Loan loan)
        {
            _db.Loans.Remove(loan);
            _db.SaveChanges();
        }
    }
}
