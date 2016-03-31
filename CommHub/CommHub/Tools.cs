namespace CommHub
{
    public class Tools
    {
       
        public static void ErrorLog_Save(wsRequest req, string paramList, string funcName, string errorText)
        {
            var dc = new DataHubDataContext();
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