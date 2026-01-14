using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Models
{
    public sealed class Reservation
    {
        public Book Book { get; init; } = null!;
        public Reader Reader { get; init; } = null!;
        public DateTime CreatedAt { get; init; }
    }
}
