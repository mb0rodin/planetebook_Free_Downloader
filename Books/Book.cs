using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet;

namespace Books
{
    public class Book
    {
        private HttpRequest _Request;
        private string _BookName;
        private byte[] _Book;
        private byte[] _Image;
        public Book(HttpRequest req)
        {
            _Request = req;
        }
        public Book(HttpRequest req,string book)
        {
            _Request = req;
            _BookName = book;
            SetBook(book);
        }
        public bool SetBook(string s)
        {
            _BookName = s;

            try
            {
                string urlBook = "http://www.planetebook.com/" + _BookName;
                string get = _Request.Get(urlBook).ToString();
                urlBook = "http://www.planetebook.com/ebooks/" + get.Parse("<a href=\"ebooks/", "\"");
                _Book = _Request.Get(urlBook).ToBytes();

                string urlImage = "http://www.planetebook.com/" + get.Parse("<div class=\"span5 offset1\"><img src=\"", "\"");
                string nameImage = get.Parse("images/cover/full-size/Book-Cover-", "\"");
                _Image = _Request.Get(urlImage).ToBytes();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void SaveURL(string Path)
        {
            string ur = "http://www.planetebook.com/" + _BookName;
            File.WriteAllText(Path + _BookName.Replace(".asp", "") + ".txt", ur);
        }
        public void SaveBook(string Path)
        {
            File.WriteAllBytes(Path.Replace(": ", " ") + _BookName.Replace(".asp", "") + ".pdf", _Book);
        }
        public void SaveImage(string Path)
        {
            File.WriteAllBytes(Path.Replace(": ", " ") + _BookName.Replace(".asp", "") + ".jpg", _Image);
        }
    }
}
