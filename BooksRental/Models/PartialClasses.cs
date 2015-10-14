using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BooksRental.Models
{
    [MetadataType(typeof(AccountMetaData))]
    public partial class Account
    {
    }
    [MetadataType(typeof(BookMetaData))]
    public partial class Book
    {
    }
    [MetadataType(typeof(BookGenderMetaData))]
    public partial class BookGender
    {
    }
    [MetadataType(typeof(AccountTypeMetaData))]
    public partial class AccountType
    {
    }
}