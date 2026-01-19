using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Models
{
    public sealed class Loan
    {
        public Reader Reader { get; init; } = null!;
        public BookCopy Copy { get; set; } = null!;
        public DateTime IssuedAt { get; init; }
        // Дата возврата, null если книга ещё на руках
        public DateTime? ReturnAt { get; set; }
    }
}
