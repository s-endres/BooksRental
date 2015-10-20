using BooksRental.Models;
using System.Collections.Generic;

namespace BooksRental.POCOs
{
    public class BookGenderPOCO
    {
        public int BookGenderId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}