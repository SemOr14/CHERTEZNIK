using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace svg_test1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static System.Drawing.Size IconSize { get;}
        [STAThread]
        static void Main()
        {
            if (Directory.Exists("C:\\Чертёжник") == false) //проверка наличии папки. если нет, то создает
            {
                Directory.CreateDirectory("C:\\Чертёжник");
            }

            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDPIAware();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]

        private static extern bool SetProcessDPIAware();
    }
}
