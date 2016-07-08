use Hub
--alter database Hub set trustworthy on

drop procedure ws.VKontakte_Sta
go

drop procedure ws.VKontakte_Sta_forNew
go

drop procedure ws.VK_UpdateComm
go

drop procedure ws.VKontakte_Sta_Graph
go

drop procedure ws.OK_Sta
go

drop procedure ws.OK_Sta_ForNew
go

drop procedure ws.OK_UpdateComm
go

drop procedure ws.Send_SMS
go

-- CommStaCLR
if exists ( select * from sys.assemblies where [name] = N'CommStaCLR')
  drop assembly CommStaCLR
go

-- CommStaClassLibrary
if exists ( select * from sys.assemblies where [name] = N'CommStaClassLibrary')
  drop assembly [CommStaClassLibrary]
go

-- CommStaClassLibrary
create assembly [CommStaClassLibrary]  
  from 'C:\CommStaService\bin\CommStaClassLibrary.dll'  
  with permission_set = unsafe;
go

--CommStaCLR
create assembly CommStaCLR  
  from 'C:\CommStaService\bin\CommStaCLR.dll'  
  with permission_set = unsafe;
go

create procedure ws.VKontakte_Sta
with execute as caller
as external name CommStaCLR.StoredProcedures.sp_ws_VKontakte_Sta
go

create procedure ws.VKontakte_Sta_ForNew
with execute as caller
as external name CommStaCLR.StoredProcedures.sp_ws_VKontakte_Sta_ForNew
go

create procedure ws.VK_UpdateComm
with execute as caller
as external name CommStaCLR.StoredProcedures.sp_ws_VK_UpdateComm
go

create procedure ws.VKontakte_Sta_Graph
with execute as caller
as external name CommStaCLR.StoredProcedures.sp_ws_VKontakte_Sta_Graph
go

create procedure ws.OK_Sta
with execute as caller
as external name CommStaCLR.StoredProcedures.sp_ws_OK_Sta
go

create procedure ws.OK_Sta_ForNew
with execute as caller
as external name CommStaCLR.StoredProcedures.sp_ws_OK_Sta_ForNew
go

create procedure ws.OK_UpdateComm
with execute as caller
as external name CommStaCLR.StoredProcedures.sp_ws_OK_UpdateComm
go


create procedure ws.Send_SMS (@message nvarchar(256), @phone nvarchar(32)) 
with execute as caller
as external name CommStaCLR.StoredProcedures.sp_ws_Send_SMS
go

/*
DECLARE @Command VARCHAR(MAX) = 'ALTER AUTHORIZATION ON DATABASE::[<<Hub>>] TO 
[<<kova>>]' 

SELECT @Command = REPLACE(REPLACE(@Command 
            , '<<Hub>>', SD.Name)
            , '<<kova>>', SL.Name)
FROM master..sysdatabases SD 
JOIN master..syslogins SL ON  SD.SID = SL.SID
WHERE  SD.Name = DB_NAME()

PRINT @Command
EXEC(@Command)
*/