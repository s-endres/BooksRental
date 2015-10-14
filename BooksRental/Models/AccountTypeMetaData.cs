using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BooksRental.Models
{
    public class AccountTypeMetaData
    {
        public int AccountTypeId { get; set; }
        [Required]
        [Display(Name = "Role")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}