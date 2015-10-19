using BookRental.Repositories;
using BooksRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksRental.Repositories
{
    public class BookGenderRepository : Repository<BookGender>
    {
        public BookGenderRepository(string pConnectionString) :  base(pConnectionString)
        {
            //Boom
        }
    }
}