using BooksRental.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksRental.Controllers
{
    public class ConnectionController : Controller
    {
        // GET: Connection
        public JsonResult setConnection(string DataBaseName)
        {
            if (!string.IsNullOrEmpty(DataBaseName.Trim())) { 
            GlobalVariables.ConnectionString = "Data Source=SFECLT\\SQLEXPRESS;Initial Catalog="+ DataBaseName + ";Integrated Security=True";
            }
            return Json("data: true", JsonRequestBehavior.AllowGet);
        }
    }
}