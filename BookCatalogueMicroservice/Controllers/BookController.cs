using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookCatalogueMicroservice.Repository;
using BookCatalogueMicroservice.Model;
using System.Transactions;

namespace BookCatalogueMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var books = _bookRepository.GetBooks();
            return new OkObjectResult(books);
        }
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var book = _bookRepository.GetBookByID(id);
            return new OkObjectResult(book);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
              using (var scope = new TransactionScope())
                {
                    _bookRepository.InsertBook(book);
                    scope.Complete();
                    return CreatedAtAction(nameof(Get), new { id = book.Id }, book
                    );
                }           
  
        }

        [HttpPut]
        public IActionResult Put([FromBody] Book book)
        {
            if (book != null)
            {
                using (var scope = new TransactionScope())
                {
                    _bookRepository.UpdateBook(book);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookRepository.DeleteBook(id);
            return new OkResult();
        }
    }
}
