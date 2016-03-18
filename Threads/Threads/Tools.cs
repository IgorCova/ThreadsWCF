using System;
using System.IO;

namespace Threads
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

        public static void ErrorLog_Save(wsRequest req, string paramList, string funcName, string errorText)
        {
            var dc = new DataThreadsDataContext();
            var session = req.Session;
            dc.ErrorLog_Save(session, funcName, paramList, errorText);
        }

        public static string GetErrorTextByCode(int Code)
        {
            var errorText = "";

            switch (Code)
            {
                case 200:
                    errorText = "No params";
                    break;
                case 280:
                    errorText = "Sql";
                    break;
                case 400:
                    errorText = "WebService";
                    break;
                case 515:
                    errorText = "SmsService";
                    break;
                default:
                    errorText = "";
                    break;
            };
            return errorText;
        }
    }
}