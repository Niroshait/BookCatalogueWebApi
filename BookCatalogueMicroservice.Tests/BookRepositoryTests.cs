using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using BookCatalogueMicroservice.DBContexts;
using BookCatalogueMicroservice.Model;
using BookCatalogueMicroservice.Repository;

namespace BookCatalogueMicroservice.Tests
{
    public class BookRepositoryTests
    {
        private DbContextOptions<BookContext> options;
        public BookRepositoryTests()
        {
            options = new DbContextOptionsBuilder<BookContext>()
           .UseInMemoryDatabase(databaseName: "BookDB")
           .Options;
            using (var context = new BookContext(options))
            {
                context.Books.Add(new Book
                {
                    Title = "Asp.NET Core",
                    Id = 1,
                    Authors = "Jhon",
                    Isbn = "11-0001-12-17",
                    PublicationDate = "23-08-2020"
                });
                context.Books.Add(new Book
                {
                    Title = "Asp.NET Web API",
                    Id = 2,
                    Authors = "Alexis",
                    Isbn = "11-0001-12-45",
                    PublicationDate = "28-08-2021"
                });
                context.SaveChanges();
            }
        }
        [Fact]
        public void GetBooksTest()
        {
            using (var context = new BookContext(options))
            {
                var repo = new BookRepository(context);
                var books = repo.GetBooks();
                Assert.Equal(2, books.Count());
            }
        }
        [Fact]
        public void GetBookByIDTest()
        {
            using (var context = new BookContext(options))
            {
                var repo = new BookRepository(context);
                Book bok = repo.GetBookByID((int)2);
                Assert.Equal("Alexis", bok.Authors);
            }
        }
        [Fact]
        public void InsertBookTest()
        {
            Book bok = new Book { Id = 3, Title = "WPF", Authors = "Smith", Isbn = "11-0001-12-45", PublicationDate= "28-08-2021" };
            using (var context = new BookContext(options))
            {
                var repo = new BookRepository(context);
                repo.InsertBook(bok);
                var allBooks = repo.GetBooks();
                Assert.Equal(3, allBooks.Count());
            }
        }

        [Fact]
        public void UpdateBookTest()
        {
            Book bok = new Book { Id = 3, Title = "WPF Volumn2", Authors = "Smith", Isbn = "11-0001-12-45", PublicationDate = "28-08-2021" };
            using (var context = new BookContext(options))
            {
                var repo = new BookRepository(context);
                repo.UpdateBook(bok);
                Book book = repo.GetBookByID((int)3);
                Assert.Equal("WPF Volumn2", book.Title);
            }
        }

        [Fact]
        public void DeleteBookTest()
        {
            using (var context = new BookContext(options))
            {
                var repo = new BookRepository(context);
                repo.DeleteBook(3);
                var books = repo.GetBooks();
                Assert.Equal(2, books.Count());
            }
        }
    }
}
