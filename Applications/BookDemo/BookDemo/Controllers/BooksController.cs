using BookDemo.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookDemo.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = AplicationContext.Books;
            return Ok(books);

        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id ")]int id)
        {
            var book = AplicationContext
                .Books
                .Where (b => b.Id.Equals(id))
                .SingleOrDefault();

            if (book is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(book);
            }


        }
    }
}
