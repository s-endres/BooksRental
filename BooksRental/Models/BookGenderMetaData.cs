using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BooksRental.Models
{
    public class BookGenderMetaData
    {
        public int BookGenderId { get; set; }
        [StringLength(50, MinimumLength = 10, ErrorMessage = "The length must be 50 max D:!")]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}