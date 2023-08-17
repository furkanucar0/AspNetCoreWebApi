using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _Manager;

        public BookManager(IRepositoryManager manager)
        {
            _Manager = manager;
        }

        public Book CreateOneBook(Book book)
        {
            if(book is null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            _Manager.Book.CreateOneBook(book);
            _Manager.Save();
            return book;

        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
           var entity = _Manager.Book.GetOneBookById(id, trackChanges);
            if(entity is null)
            {
               throw new Exception($"Book With id:{id} could not found.");
            }
            _Manager.Book.DeleteOneBook(entity);
            _Manager.Save();

        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {   
            return _Manager.Book.GetAllBooks(trackChanges);
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            return _Manager.Book.GetOneBookById(id,trackChanges);    
        }

        public void UpdateOneBook(int id, Book book, bool trackChanges)
        {
            //varligin kontrol edilmesi islemi yapiliyor var ise parametreler kontrol ediliyor
            var entity = _Manager.Book.GetOneBookById(id, trackChanges);
            if (entity is null)
            {
                throw new Exception($"Book With id:{id} could not found.");
            }

            // parametrelerin kontrol edilmesi gelen parametreler dogru ise islem gerceklestiriliyor 
            if (book is null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            
            entity.Title = book.Title;
            entity.Price = book.Price;

            _Manager.Book.Update(entity);
            _Manager.Save();
        }
    }
}
