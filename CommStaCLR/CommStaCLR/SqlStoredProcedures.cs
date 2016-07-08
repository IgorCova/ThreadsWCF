using System;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

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
    public static void sp_ws_VK_UpdateComm() {
        try {
            CommStaClassLibrary.Main.VK_UpdateComm();
        } catch (Exception ex) {
            SqlContext.Pipe.Send(ex.Message);
        }
    }

    [SqlProcedure]
    public static void sp_ws_VKontakte_Sta_Graph() {
        try {
            CommStaClassLibrary.Main.VKontakte_Sta_Graph();
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

    [SqlProcedure]
    public static void sp_ws_OK_UpdateComm() {
        try {
            CommStaClassLibrary.Main.OK_UpdateComm();
        } catch (Exception ex) {
            SqlContext.Pipe.Send(ex.Message);
        }
    }

    [SqlProcedure]
    public static void sp_ws_Send_SMS(SqlString message, SqlString phone) {
        try {
            CommStaClassLibrary.Main.Send_SMS(message.ToString(), phone.ToString());
        } catch (Exception ex) {
            SqlContext.Pipe.Send(ex.Message);
        }
    }
}
