using System;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [SqlProcedure]
    public static void sp_ws_VKontakte_Sta()
    {
        try
        {
            CommStaClassLibrary.Main.VKontakte_Sta();
        }
        catch (Exception ex)
        {
            SqlContext.Pipe.Send(ex.Message);
        }
    }

    public static void sp_ws_VKontakte_Sta_ByDate(long groupID, DateTime dateFrom, DateTime dateTo)
    {
        try
        {
            CommStaClassLibrary.Main.VKontakte_Sta_ByDate(groupID, dateFrom, dateTo);
        }
        catch (Exception ex)
        {
            SqlContext.Pipe.Send(ex.Message);
        }
    }
}
