using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCatalogueMicroservice.Model;

namespace BookCatalogueMicroservice.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
        Book GetBookByID(int bookId);
        void InsertBook(Book book);
        void DeleteBook(int bookId);
        void UpdateBook(int bookId, Book book);
        void Save();
    }
}
