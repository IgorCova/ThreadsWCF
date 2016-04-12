using CommStaClassLibrary.CommSta;

namespace CommStaClassLibrary
{
    public class Main
    {
        public static void CallWcfService(long groupID)
        {
            using (CommStaService svc = new CommStaService())
            {
                wsRequest req = new wsRequest();
                req.groupID = groupID;
                req.groupIDSpecified = true;
                svc.VKontakte_Sta(req);
            }
        }
    }
}
