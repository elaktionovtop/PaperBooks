using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Models
{
    public sealed class Book
    {
        public int Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public int PublishYear { get; init; }
        public List<Author> Authors { get; } = new();
        public List<BookCopy> Copies { get; } = new();
    }
}
