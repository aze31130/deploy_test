using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using deploy_test.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using deploy_test.Models;


namespace deploy_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly Context _context;
        public BooksController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.books.ToListAsync();
        }
		
		[HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBooksById(int id)
        {
            var books = await _context.books.FindAsync(id);
            if (books != null)
            {
                return books;
            }
            else
            {
                return NotFound();
            }
        }
		
		[HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            _context.books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBooks", new { id = book.id}, book);
        }
		
		[HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _context.books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.books.Remove(book);
            await _context.SaveChangesAsync();
            return book;
        }
		
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, Book book)
        {
            if (!id.Equals(book.id) || !_context.books.Any(x => x.id.Equals(id)))
            {
                return BadRequest();
            }
            else
            {
                _context.Entry(book).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetBooks", new { id = book.id }, book);
            }
        }
    }
}