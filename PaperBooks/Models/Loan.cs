using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Models
{
    public sealed class Loan
    {
        public int Id { get; init; }
        public Reader Reader { get; init; } = null!;
        public BookCopy Copy { get; set; } = null!;
        public DateOnly IssuedAt { get; init; }
        public DateOnly? ReturnAt { get; set; }
    }
}
