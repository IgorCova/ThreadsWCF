using CommStaClassLibrary.CommSta;
using System;

namespace CommStaClassLibrary
{
    public class Main
    {
        public static void VKontakte_Sta()
        {
            using (CommStaService svc = new CommStaService())
            {
                svc.VKontakte_Sta();
            }
        }

        public static void VKontakte_Sta_ForNew()
        {
            using (CommStaService svc = new CommStaService())
            {
                svc.VKontakte_Sta_ForNew();
            }
        }       
        
        public static void VKontakte_Sta_ByDate(long groupID, DateTime dateFrom, DateTime dateTo)
        {
            using (CommStaService svc = new CommStaService())
            {
                wsRequestByDate req = new wsRequestByDate();
                req.groupID = groupID;
                req.groupIDSpecified = true;

                req.dateFrom = dateFrom;
                req.dateFromSpecified = true;

                req.dateTo = dateTo;
                req.dateToSpecified = true;

                svc.VKontakte_Sta_ByDate(req);
            }
        }
    }
}
