using System.ComponentModel.DataAnnotations;

namespace deploy_test.Models
{
    public class Book_description
    {
        [Key]
        public int id { get; set; }
        public int book_id { get; set; }
        public string book_name { get; set; }
        public string book_description { get; set; }
		
		[HttpGet("{id}")]
        public ActionResult<BookDTO> GetBooks_byId(int id)
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

            var book_by_id = book.ToList().Find(x => x.Book_id == id);

            if (book_by_id == null)
            {
                return NotFound();
            }
            return book_by_id;
        }
    }
}