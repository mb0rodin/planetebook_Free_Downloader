using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xNet;

namespace Books
{
    public class Working
    {
        public Working( string p)
        {
            _Path = p;
        }
        private Thread _Thread;
        private string _Path;
        public int _Count = 0, _Remaints = 0;
        public void StartWirking()
        {
            if(_Thread!=null)
            {
                _Thread.Resume();
                return;
            }
            _Thread = new Thread(DoWork);
            _Thread.IsBackground = true;
            _Thread.Start();
        }
        public void StopWirking()
        {
            _Thread.Abort();
        }
        public void PauseWirking()
        {
            _Thread.Suspend();
        }
        private void DoWork()
        {
            using (var Request = new HttpRequest())
            {
                if (!Directory.Exists(_Path))
                    Directory.CreateDirectory(_Path);
                Request.AllowAutoRedirect = false;
                Request.Cookies = new CookieDictionary();
                Request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                string get = Request.Get("http://www.planetebook.com/free-ebooks.asp").ToString();

                List<string> Z = get.RegexParses("<li><a href=\"", "</a><br/>");
                _Count = Z.Count;
                for(int z = 0;z<Z.Count;z++)
                {

                    Book book = new Book(Request);
                    
                    string[] s = Z[z].Replace("\"","").Replace(": "," ").Split('>');

                    if (book.SetBook(s[0]))
                    {

                        string path = _Path + s[1] + "\\";
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        book.SaveURL(path);
                        book.SaveBook(path);
                        book.SaveImage(path);

                        ((MainForm)SC._form).AddList(s[1]);
                        _Remaints++;
                    }
                    else
                    {
                        z--;
                    }
                }

            }
        }
    }
}
