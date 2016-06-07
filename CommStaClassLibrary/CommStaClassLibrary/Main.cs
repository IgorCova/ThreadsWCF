using CommStaClassLibrary.CommSta;

namespace CommStaClassLibrary {
    public class Main {
        public static void VKontakte_Sta() {
            using (BasicHttpBinding_IService svc = new BasicHttpBinding_IService()) {
                svc.VKontakte_Sta();
            }
        }

        public static void VKontakte_Sta_ForNew() {
            using (BasicHttpBinding_IService svc = new BasicHttpBinding_IService()) {
                svc.VKontakte_Sta_ForNew();
            }
        }

        public static void OK_Sta() {
            using (BasicHttpBinding_IService svc = new BasicHttpBinding_IService()) {
                svc.OK_Sta();
            }
        }

        public static void OK_Sta_ForNew() {
            using (BasicHttpBinding_IService svc = new BasicHttpBinding_IService()) {
                svc.OK_Sta_ForNew();
            }
        }
    }
}