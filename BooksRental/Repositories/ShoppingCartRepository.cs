using BookRental.Repositories;
using BooksRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksRental.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCart>
    {

        public ShoppingCartRepository(string pConnectionString) :  base(pConnectionString)
        {
            //Boom
        }
    }
}