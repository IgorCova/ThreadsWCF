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
        public static void VKontakte_Sta_Graph() {
            using (BasicHttpBinding_IService svc = new BasicHttpBinding_IService()) {
                svc.VKontakte_Sta_Graph();
            }
        }

        public static void VK_UpdateComm() {
            using (BasicHttpBinding_IService svc = new BasicHttpBinding_IService()) {
                svc.VK_UpdateComm();
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
        public static void OK_UpdateComm() {
            using (BasicHttpBinding_IService svc = new BasicHttpBinding_IService()) {
                svc.OK_UpdateComm();
            }
        }

        public static void Send_SMS(string message, string phone) {
            using (BasicHttpBinding_IService svc = new BasicHttpBinding_IService()) {
                svc.Send_SMS(message, phone);
            }
        }
    }
}