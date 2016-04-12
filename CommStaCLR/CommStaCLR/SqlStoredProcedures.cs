using System;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [SqlProcedure]
    public static void spCalWCFService(long groupID)
    {
        try
        {
            CommStaClassLibrary.Main.CallWcfService(groupID);
        }
        catch (Exception ex)
        {
            SqlContext.Pipe.Send(ex.Message);
        }
    }
}
