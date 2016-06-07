using System;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures {
    [SqlProcedure]
    public static void sp_ws_VKontakte_Sta() {
        try {
            CommStaClassLibrary.Main.VKontakte_Sta();
        } catch (Exception ex) {
            SqlContext.Pipe.Send(ex.Message);
        }
    }

    [SqlProcedure]
    public static void sp_ws_VKontakte_Sta_ForNew() {
        try {
            CommStaClassLibrary.Main.VKontakte_Sta_ForNew();
        } catch (Exception ex) {
            SqlContext.Pipe.Send(ex.Message);
        }
    }

    [SqlProcedure]
    public static void sp_ws_OK_Sta() {
        try {
            CommStaClassLibrary.Main.OK_Sta();
        } catch (Exception ex) {
            SqlContext.Pipe.Send(ex.Message);
        }
    }

    [SqlProcedure]
    public static void sp_ws_OK_Sta_ForNew() {
        try {
            CommStaClassLibrary.Main.OK_Sta_ForNew();
        } catch (Exception ex) {
            SqlContext.Pipe.Send(ex.Message);
        }
    }
}
