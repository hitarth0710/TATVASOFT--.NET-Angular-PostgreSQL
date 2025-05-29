using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccess.Repositories
{
    public class BooksRepository
    {
        private readonly BooksDbContext _context;

        public BooksRepository(BooksDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public Book FindBookById(int bookId)
        {
            return _context.Books.FirstOrDefault(b => b.Id == bookId);
        }

        public void InsertBook(Book newBook)
        {
            _context.Books.Add(newBook);
            _context.SaveChanges();
        }

        public int EditBook(Book updatedBook)
        {
            var existingBook = FindBookById(updatedBook.Id);
            if (existingBook == null)
            {
                return -1;
            }
            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.Genre = updatedBook.Genre;
            _context.SaveChanges();
            return 1;
        }

        public int RemoveBook(int bookId)
        {
            var book = FindBookById(bookId);
            if (book == null)
            {
                return -1;
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return 1;
        }

        public List<Book> SearchBooks(string genre = null, string title = null, string author = null)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(genre))
                query = query.Where(b => b.Genre.Contains(genre));

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(b => b.Title.Contains(title));

            if (!string.IsNullOrWhiteSpace(author))
                query = query.Where(b => b.Author.Contains(author));

            return query.ToList();
        }
    }
}