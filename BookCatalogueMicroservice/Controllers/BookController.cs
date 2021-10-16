using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookCatalogueMicroservice.Repository;
using BookCatalogueMicroservice.Model;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                var books = _bookRepository.GetBooks();
                return new OkObjectResult(books);
            }
            catch(Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            try
            {
                var getDataById = _bookRepository.GetBookByID(id);
                if (getDataById != null)
                {
                    return new OkObjectResult(getDataById);
                }
                else
                {
                    return NotFound("Book with ID " + id + " is not found.Please enter the existing Book ID!!");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    _bookRepository.InsertBook(book);
                    scope.Complete();
                    return CreatedAtAction(nameof(Get), new { id = book.Id }, book
                    );
                }
            }
            catch(Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPut("{id}", Name = "Put")]
        public IActionResult Put(int id,[FromBody] Book book)
        {
            try
            {
                var getDataById = _bookRepository.GetBookByID(id);
                if (getDataById == null)
                {
                    return NotFound("Book with ID " + id + " is not found.Please enter the existing Book ID and Try to Update!!");
                }
                book.Id = getDataById.Id;
                _bookRepository.UpdateBook(id, book);
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var getDataById = _bookRepository.GetBookByID(id);
                if (getDataById == null)
                {
                    return NotFound("Book with ID " + id + " is not found.Please enter the existing Book ID and Try to Delete!!");
                }
                _bookRepository.DeleteBook(id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
