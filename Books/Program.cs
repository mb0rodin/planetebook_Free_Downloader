using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Books
{
    public static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            F = new MainForm();
            Application.Run(F);
        }
        public static MainForm F;
    }
    public class ST
    {
        public ST()
        {

        }
        public void Invoke(SC.d del)
        {
            del.InvokeSC();
        }
    }
    public static class SC
    {
        static SC()
        {
            _form = Books.Program.F;
        }
        public static Form _form
        {
            get;
            set;
        }
        public delegate void d();
        public static void Invoke(d del)
        {
            if (_form.Created)
                _form.Invoke(del);
        }
        public static void InvokeSC(this Action t)
        {
            if (_form.Created)
                _form.Invoke(t);
        }
        public static void InvokeSC(this d del)
        {
            if (_form.Created)
                _form.Invoke(del);
        }
        public static string Parse(this string url, string one, string two)
        {
            return Regex.Match(url, one + "(.*?)" + two).Groups[1].Value;
        }
        public static List<string> RegexParses(this string url, string one, string two)
        {
            var match = Regex.Matches(url, one + "(.*?)" + two);

            return match.Cast<Match>().Select(text => text.Groups[1].Value).Cast<string>().ToList();
        }
    }
}
