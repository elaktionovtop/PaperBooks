using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Models
{
    public sealed class Book
    {
        public int Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public List<BookCopy> Copies { get; } = new();
    }
}
