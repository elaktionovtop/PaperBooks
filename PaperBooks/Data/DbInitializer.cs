using PaperBooks.Models;

using System;
using System.Linq;

namespace PaperBooks.Data
{
    public static class DbInitializer
    {
        public static void Initialize(LibraryDbContext context)
        {
            context.Database.EnsureCreated(); // Создаёт БД, если ещё нет

            // 1. Если есть читатели — пропускаем
            if(context.Readers.Any())
                return;

            // 2. Читатели
            var readers = new[]
            {
                new Reader { Name = "Иванов" },
                new Reader { Name = "Петров" },
                new Reader { Name = "Сидоров" }
            };
            context.Readers.AddRange(readers);

            // 3. Книги + копии
            var book1 = new Book { Title = "CLR via C#" };
            book1.Copies.Add(new BookCopy { Book = book1 });
            book1.Copies.Add(new BookCopy { Book = book1 });

            var book2 = new Book { Title = "WPF Unleashed" };
            book2.Copies.Add(new BookCopy { Book = book2 });

            context.Books.AddRange(book1, book2);

            // 4. Резервации
            var reservation = new Reservation
            {
                Book = book1,
                Reader = readers[1],
                CreatedAt = DateTime.Now
            };
            context.Reservations.Add(reservation);

            // 5. Занятые книги (Loans)
            var loan = new Loan
            {
                Reader = readers[0],
                Copy = book1.Copies[0],
                IssuedAt = DateTime.Now
            };
            context.Loans.Add(loan);

            context.SaveChanges();
        }
    }
}
