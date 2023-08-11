using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Repositories.EFCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public BooksController(RepositoryContext context)
        {
            _context = context;
        }
        //http get ile butun kitaplari cekme
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _context.Books.ToList();
                return Ok(books);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        //httpGet ile 1 kitap cekme
        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var book = _context
                .Books
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();
                if (book == null)
                {
                    return NotFound(); //404 not Found 
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        //httppost ile 1 yeni kitap olusturma 
        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                {
                    return BadRequest(); //400
                }
                _context.Books.Add(book);
                _context.SaveChanges();

                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        
        //httpput ile 1 kitap icerik guncelleme
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] Book book)
        {
            try
            {
                var entity = _context
                .Books
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();
                if (entity == null)
                {
                    return NotFound();
                }
                if (id != book.Id)
                {
                    return BadRequest();
                }

                entity.Title = book.Title;
                entity.Price = book.Price;

                _context.SaveChanges();

                return Ok(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //httpdelete ile 1 kitap silme 
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var entity = _context
                    .Books
                    .Where(b => b.Id.Equals(id))
                    .SingleOrDefault();

                if (entity == null)
                {
                    return NotFound(
                        new
                        {
                            StatusCode = 404,
                            message = $"Book with id: {id}  could not found."
                        }); //404
                }
                _context.Remove(entity);
                _context.SaveChanges();

                return NoContent();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //httpPatch Nesnenin veya kaynagin belirli bir kismini guncelleme 
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Book> bookpatch) 
        {
            try
            {
                var entity = _context
                    .Books
                    .Where(b =>b.Id.Equals(id))
                    .SingleOrDefault();
                if(entity == null)
                {
                    return NotFound(); //404 hatasi firlatir 
                }

                bookpatch.ApplyTo(entity);
                _context.SaveChanges();

                return NoContent(); //204 Hatasi Firlatir
            }
            catch (Exception ex)
            { 
                throw new Exception (ex.Message);
            }
        }
    }
}
