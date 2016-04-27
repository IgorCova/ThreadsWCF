use Hub
--alter database Hub set trustworthy on

exec dbo.DropIfExists 'dbo.sp_ws_VKontakte_Sta_ByDate'
go

exec dbo.DropIfExists 'sp_ws_VKontakte_Sta'
go

exec dbo.DropIfExists 'dbo.sp_ws_VKontakte_Sta_forNew'
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

create procedure ws.VKontakte_Sta_ByDate
   @groupID  bigint
  ,@dateFrom datetime
  ,@dateTo   datetime
with execute as caller
as external name CommStaCLR.StoredProcedures.sp_ws_VKontakte_Sta_ByDate
go

