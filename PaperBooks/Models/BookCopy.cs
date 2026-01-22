using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Models
{
    public sealed class BookCopy
    {
        public int Id { get; init; }
        public string InventoryNumber { get; set; } = string.Empty;
        public Book Book { get; set; } = null!;
    }
}
