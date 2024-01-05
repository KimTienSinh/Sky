using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkyBook.Data;
using SkyBook.Models;

namespace SkyBook.Reponsitories
{
    public class BookRepository : IBookRepository
    {
        private BookStoreContext _context;
        private IMapper _mapper;

        public BookRepository(BookStoreContext context, IMapper mapper)
        {  
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddBookAsync(BookModel model)
        {
            var newBook = _mapper.Map<Book>(model);
            _context.Books!.Add(newBook);
            await _context.SaveChangesAsync();

            return newBook.Id;
        }

        public async Task DeleteBookAsync(int id)
        {
            var deleteBook = _context.Books!.SingleOrDefault(book => book.Id == id);
            if(deleteBook != null)
            {
                _context.Books!.Remove(deleteBook);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<BookModel>> getAllBookAsync()
        {
            var books = await _context.Books!.ToListAsync();
            return _mapper.Map<List<BookModel>>(books);
        }

        public async Task<BookModel> getBookAsync(int id)
        {
            //var book = await _context.Books!.SingleOrDefaultAsync(x => x.Id == id);
            var book = await _context.Books!.FindAsync(id);
            return _mapper.Map<BookModel>(book);
        }

        public async Task UpdateBookAsync(int id, BookModel model)
        {
            var exsitingBook = await _context.Books!.FindAsync(id);
            if(exsitingBook != null)
            {
                //1
                /*exsitingBook.Title = model.Title;
                exsitingBook.Description = model.Description;
                exsitingBook.Price = model.Price;
                exsitingBook.Quantity = model.Quantity;


                *//*var updateBook = _mapper.Map<Book>(model);
                _context.Books!.Update(updateBook);*//*

                _context.Entry(exsitingBook).State = EntityState.Modified;
                await _context.SaveChangesAsync();*/

                //2
               /* _context.Entry(exsitingBook).State = EntityState.Detached; // Detach the existing entity

                var updatedBook = _mapper.Map<Book>(model);
                updatedBook.Id = id; // Ensure the Id is set correctly

                _context.Books!.Update(updatedBook);
                await _context.SaveChangesAsync();*/

                //3
                _mapper.Map(model, exsitingBook); // Update only the changed properties

                _context.Books!.Update(exsitingBook);
                await _context.SaveChangesAsync();
            }
        }
    }
}
