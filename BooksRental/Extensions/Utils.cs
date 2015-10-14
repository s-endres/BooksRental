using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BooksRental.Extensions
{
    public class Utils
    {
        private static Utils instance;

        private Utils() { }

        public static Utils Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Utils();
                }
                return instance;
            }
        }


        #region "Main Logic"        
        public string Encript(string pData)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pData);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }


        public bool SendEmail(string to, string title, string content)
        {
            bool wasSend = false;
            try { 

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("noreplay@ecomtrading.com", "*ecomglobal2015!");
            MailMessage mm = new MailMessage("noreplay@ecomtrading.com", to, title, content);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
            wasSend = true;

            }
            catch
            {
                wasSend = false;
            }
            return wasSend;
        }
        

        protected void moveMyImage(HttpPostedFileBase ImageFile, string pName)
        {
            /*string[] name = ImageFile.FileName.Split('.');
            var email = User.Identity.Name;
            var fileName = pName + email.ToString() + DateTime.Now.ToString("MMddyyyyhhmmss") + "." + name[1];
            var filePath = Path.Combine(Server.MapPath("~/Images/BugImages/"), fileName);
            ImageFile.SaveAs(filePath);
            var imageFilePath = "./../../Images/BugImages/" + fileName.ToString();
            return imageFilePath.ToString();*/
        }

        #endregion Main Logic

    }
}