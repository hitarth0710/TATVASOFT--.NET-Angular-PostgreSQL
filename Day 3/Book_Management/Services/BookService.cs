using Book_Management.Models;
using System.Collections.Generic;
using System.Linq;

namespace Book_Management.Services
{

    public class BookService
    {
        private readonly List<Book> _bookList = new()
        {
            new Book { Id = 1, Title = "Test1", Author = "James Bond", Genre = "Fiction", Rating = 0, PublishedYear = 0 },
            new Book { Id = 2, Title = "Test2", Author = "Ruskin Bond", Genre = "Non fiction", Rating = 0, PublishedYear = 0 }
        };


        public List<Book> GetBooks() => _bookList.ToList();

        public Book? GetBookById(int id) => _bookList.FirstOrDefault(b => b.Id == id);

        public void AddBook(Book book)
        {
            book.Id = _bookList.Count > 0 ? _bookList.Max(b => b.Id) + 1 : 1;
            _bookList.Add(book);
        }


        public int UpdateBook(Book updatedBook)
        {
            var existingBook = GetBookById(updatedBook.Id);
            if (existingBook is null)
                return -1;

            UpdateBookProperties(existingBook, updatedBook);
            return 1;
        }


        public int DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book is null)
                return -1;

            _bookList.Remove(book);
            return 1;
        }
        private static void UpdateBookProperties(Book target, Book source)
        {
            target.Title = source.Title;
            target.Author = source.Author;
            target.Genre = source.Genre;
            target.Rating = source.Rating;
            target.PublishedYear = source.PublishedYear;
        }
    }
}