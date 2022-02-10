using System;
using System.Windows.Forms;

namespace OcrToText
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmOcrToText ocrForm = new frmOcrToText();
            Application.Run();
        }
    }
}
