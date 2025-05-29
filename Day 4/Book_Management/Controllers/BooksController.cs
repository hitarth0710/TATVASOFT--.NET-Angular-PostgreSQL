using Books.DataAccess.Models;
using Books.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Book_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _service;

        public BooksController(BooksService service)
        {
            _service = service;
        }

        [HttpGet("All")]
        public ActionResult<List<Book>> AllBooks()
        {
            var books = _service.FetchAllBooks();
            if (books == null || books.Count == 0)
                return NotFound("No books available.");
            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var book = _service.FetchBookById(id);
            if (book == null)
                return NotFound("Book not found.");
            return Ok(book);
        }

        [HttpPost("Add")]
        public ActionResult Add([FromBody] Book book)
        {
            _service.CreateBook(book);
            return Ok("Book successfully added.");
        }

        [HttpPut("Update")]
        public ActionResult Update([FromBody] Book book)
        {
            var result = _service.ModifyBook(book);
            if (result == -1)
                return NotFound("Book not found.");
            return Ok("Book updated.");
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            var result = _service.DeleteBookById(id);
            if (result == -1)
                return NotFound("Book not found.");
            return Ok("Book deleted.");
        }

        [HttpGet("Search")]
        public ActionResult Search([FromQuery] string genre, [FromQuery] string title, [FromQuery] string author)
        {
            var books = _service.FilterBooks(genre, title, author);
            if (books == null || books.Count == 0)
                return NotFound("No matching books found.");
            return Ok(books);
        }
    }
}
