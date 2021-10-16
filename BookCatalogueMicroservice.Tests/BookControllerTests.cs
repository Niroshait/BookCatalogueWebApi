using System;
using System.Collections.Generic;
using System.Text;
using BookCatalogueMicroservice.Repository;
using BookCatalogueMicroservice.Model;
using BookCatalogueMicroservice.Controllers;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FluentAssertions;

namespace BookCatalogueMicroservice.Tests
{
    public class BookControllerTests
    {
        private readonly Mock<IBookRepository> service;
        public BookControllerTests()
        {
            service = new Mock<IBookRepository>();
        }
        [Fact]
        public void GetBook_BookExistsInRepo()
        {
            //arrange
           // var book = GetSampleBook();
            service.Setup(x => x.GetBooks())
                .Returns(GetSampleBook);
            var controller = new BookController(service.Object);

            //act
            var actionResult = controller.Get();
            var result = actionResult as OkObjectResult;
            var actual = result.Value as IEnumerable<Book>;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleBook().Count(), actual.Count());
        }
        [Fact]
        public void GetBookById_BookwithSpecificIdExists()
        {
            //arrange
            var books = GetSampleBook();
            var firstBook = books[0];
            service.Setup(x => x.GetBookByID((int)1))
                .Returns(firstBook);
            var controller = new BookController(service.Object);

            //act
            var actionResult = controller.Get((int)1);
            var result = actionResult as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo(firstBook);
        }
        [Fact]
        public void GetBookById_BookWithIDNotExists()
        {
            //arrange
            var books = GetSampleBook();
            var firstbook = books[0];
            service.Setup(x => x.GetBookByID((int)18))
                .Returns(firstbook);
            var controller = new BookController(service.Object);

            //act
            var actionResult = controller.Get((int)8);

            //assert
            var result = actionResult;
            Assert.IsType<NotFoundObjectResult>(result);
        }
        private List<Book> GetSampleBook()
        {
            List<Book> output = new List<Book>
            {
                new Book
                {
                     Title = "Asp.NET Core",
                    Id = 1,
                    Authors = "Jhon",
                    Isbn = "11-0001-12-17",
                    PublicationDate = "23-08-2020"
                },
                new Book
                {
                  Title = "Asp.NET Web API",
                    Id = 2,
                    Authors = "Alexis",
                    Isbn = "11-0001-12-45",
                    PublicationDate = "28-08-2021"
                }
            };
            return output;

        }
       
    }
}
