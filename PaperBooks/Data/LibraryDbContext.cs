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
        public DbSet<Author> Authors => Set<Author>();
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

            // Book -> Author (many : many)
            model.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithMany(c => c.Books);

            // Configure Loan entity
            model.Entity<Loan>(entity =>
            {
                // Loan -> BookCopy (many : 1)
                entity.HasOne(l => l.Copy)
                    .WithMany()
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                // Loan -> Reader (many : 1)
                entity.HasOne(l => l.Reader)
                    .WithMany()
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                // DateOnly -> date
                entity.Property(l => l.IssuedAt)
                    .HasColumnType("date");

                entity.Property(l => l.ReturnAt)
                    .HasColumnType("date");
            });

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
