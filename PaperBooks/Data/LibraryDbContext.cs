using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using PaperBooks.Models;

using System.IO;

namespace PaperBooks.Data
{
    public sealed class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<BookCopy> BookCopies => Set<BookCopy>();
        public DbSet<Reader> Readers => Set<Reader>();
        public DbSet<Loan> Loans => Set<Loan>();
        public DbSet<Reservation> Reservations => Set<Reservation>();

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            // Book -> BookCopy (1 : many)
            model.Entity<Book>()
                .HasMany(b => b.Copies)
                .WithOne(c => c.Book)
                .IsRequired();

            // Loan -> BookCopy (many : 1)
            model.Entity<Loan>()
                .HasOne(l => l.Copy)
                .WithMany()
                .IsRequired();

            // Loan -> Reader (many : 1)
            model.Entity<Loan>()
                .HasOne(l => l.Reader)
                .WithMany()
                .IsRequired();

            // Reservation -> Book (many : 1)
            model.Entity<Reservation>()
                .HasOne(r => r.Book)
                .WithMany()
                .IsRequired();

            // Reservation -> Reader (many : 1)
            model.Entity<Reservation>()
                .HasOne(r => r.Reader)
                .WithMany()
                .IsRequired();
        }
    }

    public sealed class LibraryDbContextFactory 
        : IDesignTimeDbContextFactory<LibraryDbContext>
    {
        public LibraryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = 
                new DbContextOptionsBuilder<LibraryDbContext>();

            optionsBuilder.UseSqlServer(ConnectionString);
            return new LibraryDbContext(optionsBuilder.Options);
        }

        public static string? ConnectionString
        {
            get
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory
                        .GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                return config.GetConnectionString("LibraryDb");
            }
        }
    }
}
