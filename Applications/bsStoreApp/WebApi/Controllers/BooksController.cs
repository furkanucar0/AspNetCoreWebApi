using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Repositories.EFCore;
using Repositories.Contracts;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepositoryManager _manager;

        public BooksController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        //http get ile butun kitaplari cekme
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _manager.Book.GetAllBooks(false);
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
                var book = _manager
                .Book.GetOneBookById(id, false);

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
                
                _manager.Book.CreateOneBook(book);
                _manager.Save();

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
                var entity = _manager
                    .Book
                    .GetOneBookById (id, true);


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

                _manager.Save();

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
                var entity = _manager.Book
                    .GetOneBookById(id, true);

                if (entity == null)
                {
                    return NotFound(
                        new
                        {
                            StatusCode = 404,
                            message = $"Book with id: {id}  could not found."
                        }); //404
                }

                _manager.Book.DeleteOneBook(entity);
                _manager.Save();

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
                var entity = _manager
                    .Book
                    .GetOneBookById(id, true);

                if(entity == null)
                {
                    return NotFound(); //404 hatasi firlatir 
                }

                bookpatch.ApplyTo(entity);
                
                _manager.Book.UpdateOneBook(entity);

                return NoContent(); //204 Hatasi Firlatir
            }
            catch (Exception ex)
            { 
                throw new Exception (ex.Message);
            }
        }
    }
}
