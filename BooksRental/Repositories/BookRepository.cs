using BookRental.Repositories;
using BooksRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksRental.Repositories
{
    public class BookRepository : Repository<Book>
    {
        public BookRepository(string pConnectionString) :  base(pConnectionString)
        {
            //Boom
        }
        public List<BookGender> getAllBookGenders()
        {
            return context.BookGenders.OrderBy(a => a.Name).ToList();
        }
    }
}