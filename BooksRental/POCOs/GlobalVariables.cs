using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksRental.POCOs
{
    public static class GlobalVariables
    {
        // read-write variable
        public static string ConnectionString
        {
            get
            {
                return HttpContext.Current.Application["ConnectionString"] as string;
            }
            set
            {
                HttpContext.Current.Application["ConnectionString"] = value;
            }
        }
    }
}