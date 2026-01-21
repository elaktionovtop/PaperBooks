using PaperBooks.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Services
{
    public interface IReservationsService
    {
        IEnumerable<Reader> GetReadersReservedBook(Book book);
        void RemoveReservation(Book book, Reader reader);
    }
}
