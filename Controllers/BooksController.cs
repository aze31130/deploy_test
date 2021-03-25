﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using deploy_test.Data;
using deploy_test.DTO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using deploy_test.Models;
using DTO;


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
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            var book = from books in _context.Book
                       join book_descriptions in _context.Book_Description on books.id equals book_descriptions.book_id
                       select new BookDTO
                       {
                           Book_id = books.id,
                           Book_price = books.price,
                           ISBN = books.isbn,
                           Book_name = book_descriptions.book_name,
                           Book_description = book_descriptions.book_description
                       };

            return await book.ToListAsync();
        }
		[HttpPost]
        public async Task<ActionResult<AddBook>> Add_Books(AddBook bookDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = new Book()
            {
                isbn = bookDTO.ISBN,
                price = bookDTO.Book_price
            };
            await _context.Book.AddAsync(book);
            await _context.SaveChangesAsync();

            var book_description = new Book_description()
            {
                book_id = book.id,
                book_name = bookDTO.Book_name,
                book_description = bookDTO.Book_description
            };
            await _context.AddAsync(book_description);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooks", new { id = book.id}, bookDTO);
        }
    }
}