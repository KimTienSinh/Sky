using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyBook.Models;
using SkyBook.Reponsitories;

namespace SkyBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IBookRepository _bookRepo;

        public ProductsController(IBookRepository repo) 
        { 
            _bookRepo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                return Ok(await _bookRepo.getAllBookAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookId(int id)
        {
            var book = await _bookRepo.getBookAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel model)
        { 
            try
            {
                var newBookId = await _bookRepo.AddBookAsync(model);
                var book = await _bookRepo.getBookAsync(newBookId);
                return book == null ? NotFound() : Ok(book);
            }
            catch 
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookModel model)
        {
            var existingBook = await _bookRepo.getBookAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }
            await _bookRepo.UpdateBookAsync(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var existingBook = await _bookRepo.getBookAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }
            await _bookRepo.DeleteBookAsync(id);
            return Ok();
        }
    }
}
