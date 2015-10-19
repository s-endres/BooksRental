using BookRental.Repositories;
using BooksRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksRental.Repositories
{
    public class AccountRepository : Repository<Account>
    {

        public AccountRepository(string pConnectionString) :  base(pConnectionString)
        {
            //Boom
        }
        public List<Account> GetByEmail(String pEmail)
        {
            return DbSet.Where(a => a.Email.Equals(pEmail)).ToList();
        }
        public List<ShoppingCart> getShoppingCart()
        {
            return context.ShoppingCarts.ToList();
        }
        public List<AccountType> GetAccountTypes()
        {
            return context.AccountTypes.ToList();
        }
    }
}