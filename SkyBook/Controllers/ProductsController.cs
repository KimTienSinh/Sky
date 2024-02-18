using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyBook.Helpers;
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
        [Authorize(Roles = AppRole.Customer)]
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
        [Authorize(Roles = AppRole.Manager)]
        public async Task<IActionResult> GetBookId(int id)
        {
            var book = await _bookRepo.getBookAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> UpdateBook(int id, BookModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            await _bookRepo.UpdateBookAsync(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
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
