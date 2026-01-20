using PaperBooks.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBooks.Data
{
    public interface IReadersRepository
    {
        IEnumerable<Reader> GetAll();
        Reader? GetById(int id);
    }
}
