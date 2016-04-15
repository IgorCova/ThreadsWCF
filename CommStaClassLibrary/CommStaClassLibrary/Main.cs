using CommStaClassLibrary.CommSta;
using System;

namespace CommStaClassLibrary
{
    public class Main
    {
        public static void VKontakte_Sta(long groupID)
        {
            using (CommStaService svc = new CommStaService())
            {
                wsRequest req = new wsRequest();
                req.groupID = groupID;
                req.groupIDSpecified = true;
                svc.VKontakte_Sta(req);
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
