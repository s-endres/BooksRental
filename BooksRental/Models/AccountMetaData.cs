using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksRental.Models
{
    public class AccountMetaData
    {
        public int AccountId { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [EmailAddress]
        [Display(Name = "Email")]
        [StringLength(500)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int AccountTypeId { get; set; }

        public virtual AccountType AccountType { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}