using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace LanguageListSeperator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            string[] nonFilteredFileNames = Directory.GetFiles("..\\..\\..\\bin\\Debug\\tessdata");
            
            FileStream fs = File.Open("langList.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write("readonly string[] langList = new string[] {");
            foreach (string fileName in nonFilteredFileNames)
            {
                FileInfo info = new FileInfo(fileName);
                if(info.Extension == ".traineddata")
                {
                    sw.Write("\"" + info.Name.Replace(info.Extension, "") + "\", ");
                    sw.Flush();
                }
            }
            sw.Write(" };");
            sw.Close();
        }
    }
}
