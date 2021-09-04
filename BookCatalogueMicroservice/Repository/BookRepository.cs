using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCatalogueMicroservice.Model;
using BookCatalogueMicroservice.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace BookCatalogueMicroservice.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _dbContext;

        public BookRepository(BookContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteBook(int bookId)
        {
            var book = _dbContext.Books.Find(bookId);
            _dbContext.Books.Remove(book);
            Save();
        }

        public Book GetBookByID(int bookId)
        {
            return _dbContext.Books.Find(bookId);
        }

        public IEnumerable<Book> GetBooks()
        {
            return _dbContext.Books.ToList();
        }

        public void InsertBook(Book book)
        {
            _dbContext.Add(book);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _dbContext.Entry(book).State = EntityState.Modified;
            Save();
        }
    }
}
