using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Models
{
    public sealed class Reservation
    {
        public int Id { get; init; }
        public Book Book { get; init; } = null!;
        public Reader Reader { get; init; } = null!;
        public DateOnly CreatedAt { get; init; }
    }
}
