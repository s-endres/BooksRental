using BooksRental.Models;
using System.Collections.Generic;

namespace BooksRental.POCOs
{
    public class AccountTypePOCO
    {
        public int AccountTypeId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}