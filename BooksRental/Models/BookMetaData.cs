using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BooksRental.Models
{
    public class BookMetaData
    {
        public int BookId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Es Requiredo D:!")]
        [DataType(DataType.MultilineText)]
        [StringLength(200, MinimumLength = 10,ErrorMessage = "AHHH!")]
        public string Description { get; set; }
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        [Display(Name = "Gender")]
        public int BookGenderId { get; set; }

        public virtual BookGender BookGender { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}