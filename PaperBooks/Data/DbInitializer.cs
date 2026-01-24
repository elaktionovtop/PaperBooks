using PaperBooks.Models;

using System;
using System.Linq;
/*
Ч   1   2   3

К   11  12  23  34  35

Ж   ч1 - к11    ч2 - к23

Б   к1 - ч1 к1 - ч3 к2 - ч3
*/
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
            var reader1 = new Reader 
            {
                Name = "Читатель 1",
                BirthYear = 2001
            };
            var reader2= new Reader
            {
                Name = "Читатель 2",
                BirthYear = 2002
            };
            var reader3 = new Reader
            {
                Name = "Читатель 3",
                BirthYear = 2003
            };
            context.Readers.AddRange(reader1, reader2, reader3);

            // 3. Книги + копии + авторы

            var author1 = new Author { Name = "Автор 1" };
            var author2 = new Author { Name = "Автор 2" };
            var author3 = new Author { Name = "Автор 3" };

            var book1 = new Book { Title = "Книга 1", PublishYear = 2001 };
            book1.Copies.Add(new BookCopy { Book = book1, InventoryNumber = "1" });
            book1.Copies.Add(new BookCopy { Book = book1, InventoryNumber = "2" });
            book1.Authors.Add(author1);

            var book2 = new Book { Title = "Книга 2", PublishYear = 2002 };
            book2.Copies.Add(new BookCopy { Book = book2, InventoryNumber = "3" });
            book2.Authors.Add(author2);
            book2.Authors.Add(author3);

            var book3 = new Book { Title = "Книга 3", PublishYear = 2003 };
            book3.Copies.Add(new BookCopy { Book = book3, InventoryNumber = "4" });
            book3.Copies.Add(new BookCopy { Book = book3, InventoryNumber = "5" });
            book3.Authors.Add(author1);

            context.Books.AddRange(book1, book2, book3);

            // 4. Взятые книги (Loans)
            var loan1 = new Loan
            {
                Reader = reader1,
                Copy = book1.Copies[0],
                IssuedAt = DateOnly.FromDateTime(DateTime.Now)
            };

            var loan2 = new Loan
            {
                Reader = reader2,
                Copy = book2.Copies[0],
                IssuedAt = DateOnly.FromDateTime(DateTime.Now)
            };

            context.Loans.AddRange(loan1, loan2);

            // 5. Резервации
            var reservation1 = new Reservation
            {
                Book = book1,
                Reader = reader1,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now)
            };

            var reservation2 = new Reservation
            {
                Book = book1,
                Reader = reader3,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now)
            };

            var reservation3 = new Reservation
            {
                Book = book2,
                Reader = reader3,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now)
            };

            context.Reservations.AddRange(reservation1, 
                reservation2, reservation3);

            context.SaveChanges();
        }
    }
}
