using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Threads.wsClasses
{
    public class Tools
    {
         public static void ObjectFileSaveToLocalHDD(string FileName, string Data)
        {
            // полный путь к фоткам
            string FullName = AppDomain.CurrentDomain.BaseDirectory + "\\Logos\\Member\\";

            // если нет директории, то создаем
            if (!(Directory.Exists(FullName)))
                Directory.CreateDirectory(FullName);

            // полное имя файла
            FullName = FullName + "\\" + FileName;

            //сохраняем в файл
            Base64ToFile(Data, FullName);
        }

        public static void Base64ToFile(string Source, string FileName)
        {
            byte[] filebytes = Convert.FromBase64String(Source);
            using (FileStream fs = new FileStream(FileName, FileMode.Create /*New*/, FileAccess.Write, FileShare.None))
            {
                fs.Write(filebytes, 0, filebytes.Length);
                fs.Close();
            }
        }
    }
}