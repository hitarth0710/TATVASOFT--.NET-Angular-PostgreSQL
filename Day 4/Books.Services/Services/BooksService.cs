using System;
using System.Collections.Generic;
using Books.DataAccess.Models;
using Books.DataAccess.Repositories;

namespace Books.Services.Services
{
    public class BooksService
    {
        private readonly BooksRepository _repository;

        public BooksService(BooksRepository repository)
        {
            _repository = repository;
        }

        public List<Book> FetchAllBooks()
        {
            return _repository.GetAllBooks();
        }

        public Book FetchBookById(int id)
        {
            return _repository.FindBookById(id);
        }

        public void CreateBook(Book book)
        {
            _repository.InsertBook(book);
        }

        public int ModifyBook(Book book)
        {
            return _repository.EditBook(book);
        }

        public int DeleteBookById(int id)
        {
            return _repository.RemoveBook(id);
        }

        public List<Book> FilterBooks(string genre = null, string title = null, string author = null)
        {
            return _repository.SearchBooks(genre, title, author);
            }
    }
}
