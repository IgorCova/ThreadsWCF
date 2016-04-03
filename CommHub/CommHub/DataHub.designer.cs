﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CommHub
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Hub")]
	public partial class DataHubDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public DataHubDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["HubConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataHubDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataHubDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataHubDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataHubDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SubjectComm_Save")]
		public int SubjectComm_Save([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> id, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> ownerHubID, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(256)")] string name)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, ownerHubID, name);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.Comm_Read")]
		public ISingleResult<Comm_ReadResult> Comm_Read([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> id, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> ownerHubID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, ownerHubID);
			return ((ISingleResult<Comm_ReadResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.Comm_Save")]
		public ISingleResult<Comm_SaveResult> Comm_Save([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] ref System.Nullable<long> id, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> ownerHubID, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> subjectCommID, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> areaCommID, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(256)")] string name, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> adminCommID, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(512)")] string link)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, ownerHubID, subjectCommID, areaCommID, name, adminCommID, link);
			id = ((System.Nullable<long>)(result.GetParameterValue(0)));
			return ((ISingleResult<Comm_SaveResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.OwnerHub_Read")]
		public ISingleResult<OwnerHub_ReadResult> OwnerHub_Read([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> id)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id);
			return ((ISingleResult<OwnerHub_ReadResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.OwnerHub_Save")]
		public ISingleResult<OwnerHub_SaveResult> OwnerHub_Save([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] ref System.Nullable<long> id, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(512)")] string firstName, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(512)")] string lastName, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(32)")] string phone, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(512)")] string linkFB)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, firstName, lastName, phone, linkFB);
			id = ((System.Nullable<long>)(result.GetParameterValue(0)));
			return ((ISingleResult<OwnerHub_SaveResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SubjectComm_Del")]
		public int SubjectComm_Del([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> id, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> ownerHubID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, ownerHubID);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SubjectComm_ReadDict")]
		public ISingleResult<SubjectComm_ReadDictResult> SubjectComm_ReadDict([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> ownerHubID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), ownerHubID);
			return ((ISingleResult<SubjectComm_ReadDictResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.ErrorLog_Save")]
		public ISingleResult<ErrorLog_SaveResult> ErrorLog_Save([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Session", DbType="VarChar(64)")] string session, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="FuncName", DbType="NVarChar(128)")] string funcName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Params", DbType="VarChar(MAX)")] string @params, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ErrorText", DbType="VarChar(MAX)")] string errorText)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), session, funcName, @params, errorText);
			return ((ISingleResult<ErrorLog_SaveResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.AdminComm_ReadDict")]
		public ISingleResult<AdminComm_ReadDictResult> AdminComm_ReadDict([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> ownerHubID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), ownerHubID);
			return ((ISingleResult<AdminComm_ReadDictResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SessionReq_Save")]
		public ISingleResult<SessionReq_SaveResult> SessionReq_Save([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(64)")] string dID, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(64)")] string phone)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), dID, phone);
			return ((ISingleResult<SessionReq_SaveResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.Session_Save")]
		public ISingleResult<Session_SaveResult> Session_Save([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> sessionReqID, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(64)")] string dID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sessionReqID, dID);
			return ((ISingleResult<Session_SaveResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.AdminComm_Save")]
		public ISingleResult<AdminComm_SaveResult> AdminComm_Save([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> id, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> ownerHubID, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(512)")] string firstName, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(512)")] string lastName, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(32)")] string phone, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(512)")] string linkFB)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, ownerHubID, firstName, lastName, phone, linkFB);
			return ((ISingleResult<AdminComm_SaveResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.StatsCommVK_Save")]
		public int StatsCommVK_Save([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> commID, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> commViews, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> commVisitors, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> commReach, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> commReachSubscribers, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> commSubscribed, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> commUnsubscribed, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> commLikes, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> commComments, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> commReposts, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> commPostCount)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), commID, commViews, commVisitors, commReach, commReachSubscribers, commSubscribed, commUnsubscribed, commLikes, commComments, commReposts, commPostCount);
			return ((int)(result.ReturnValue));
		}
	}
	
	public partial class Comm_ReadResult
	{
		
		private long _id;
		
		private System.Nullable<long> _ownerHubID;
		
		private System.Nullable<long> _subjectCommID;
		
		private System.Nullable<int> _areaCommID;
		
		private string _name;
		
		private System.Nullable<long> _adminCommID;
		
		public Comm_ReadResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="BigInt NOT NULL")]
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ownerHubID", DbType="BigInt")]
		public System.Nullable<long> ownerHubID
		{
			get
			{
				return this._ownerHubID;
			}
			set
			{
				if ((this._ownerHubID != value))
				{
					this._ownerHubID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_subjectCommID", DbType="BigInt")]
		public System.Nullable<long> subjectCommID
		{
			get
			{
				return this._subjectCommID;
			}
			set
			{
				if ((this._subjectCommID != value))
				{
					this._subjectCommID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_areaCommID", DbType="Int")]
		public System.Nullable<int> areaCommID
		{
			get
			{
				return this._areaCommID;
			}
			set
			{
				if ((this._areaCommID != value))
				{
					this._areaCommID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="VarChar(256)")]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_adminCommID", DbType="BigInt")]
		public System.Nullable<long> adminCommID
		{
			get
			{
				return this._adminCommID;
			}
			set
			{
				if ((this._adminCommID != value))
				{
					this._adminCommID = value;
				}
			}
		}
	}
	
	public partial class Comm_SaveResult
	{
		
		private long _id;
		
		private System.Nullable<long> _ownerHubID;
		
		private System.Nullable<long> _subjectCommID;
		
		private System.Nullable<int> _areaCommID;
		
		private string _name;
		
		private System.Nullable<long> _adminCommID;
		
		public Comm_SaveResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="BigInt NOT NULL")]
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ownerHubID", DbType="BigInt")]
		public System.Nullable<long> ownerHubID
		{
			get
			{
				return this._ownerHubID;
			}
			set
			{
				if ((this._ownerHubID != value))
				{
					this._ownerHubID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_subjectCommID", DbType="BigInt")]
		public System.Nullable<long> subjectCommID
		{
			get
			{
				return this._subjectCommID;
			}
			set
			{
				if ((this._subjectCommID != value))
				{
					this._subjectCommID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_areaCommID", DbType="Int")]
		public System.Nullable<int> areaCommID
		{
			get
			{
				return this._areaCommID;
			}
			set
			{
				if ((this._areaCommID != value))
				{
					this._areaCommID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="VarChar(256)")]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_adminCommID", DbType="BigInt")]
		public System.Nullable<long> adminCommID
		{
			get
			{
				return this._adminCommID;
			}
			set
			{
				if ((this._adminCommID != value))
				{
					this._adminCommID = value;
				}
			}
		}
	}
	
	public partial class OwnerHub_ReadResult
	{
		
		private long _id;
		
		private string _firstName;
		
		private string _lastName;
		
		private string _phone;
		
		private string _linkFB;
		
		public OwnerHub_ReadResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="BigInt NOT NULL")]
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_firstName", DbType="VarChar(512)")]
		public string firstName
		{
			get
			{
				return this._firstName;
			}
			set
			{
				if ((this._firstName != value))
				{
					this._firstName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lastName", DbType="VarChar(512)")]
		public string lastName
		{
			get
			{
				return this._lastName;
			}
			set
			{
				if ((this._lastName != value))
				{
					this._lastName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_phone", DbType="VarChar(32)")]
		public string phone
		{
			get
			{
				return this._phone;
			}
			set
			{
				if ((this._phone != value))
				{
					this._phone = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_linkFB", DbType="VarChar(512)")]
		public string linkFB
		{
			get
			{
				return this._linkFB;
			}
			set
			{
				if ((this._linkFB != value))
				{
					this._linkFB = value;
				}
			}
		}
	}
	
	public partial class OwnerHub_SaveResult
	{
		
		private long _id;
		
		private string _firstName;
		
		private string _lastName;
		
		private string _phone;
		
		private string _linkFB;
		
		public OwnerHub_SaveResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="BigInt NOT NULL")]
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_firstName", DbType="VarChar(512)")]
		public string firstName
		{
			get
			{
				return this._firstName;
			}
			set
			{
				if ((this._firstName != value))
				{
					this._firstName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lastName", DbType="VarChar(512)")]
		public string lastName
		{
			get
			{
				return this._lastName;
			}
			set
			{
				if ((this._lastName != value))
				{
					this._lastName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_phone", DbType="VarChar(32)")]
		public string phone
		{
			get
			{
				return this._phone;
			}
			set
			{
				if ((this._phone != value))
				{
					this._phone = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_linkFB", DbType="VarChar(512)")]
		public string linkFB
		{
			get
			{
				return this._linkFB;
			}
			set
			{
				if ((this._linkFB != value))
				{
					this._linkFB = value;
				}
			}
		}
	}
	
	public partial class SubjectComm_ReadDictResult
	{
		
		private long _id;
		
		private System.Nullable<long> _ownerHubID;
		
		private string _name;
		
		public SubjectComm_ReadDictResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="BigInt NOT NULL")]
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ownerHubID", DbType="BigInt")]
		public System.Nullable<long> ownerHubID
		{
			get
			{
				return this._ownerHubID;
			}
			set
			{
				if ((this._ownerHubID != value))
				{
					this._ownerHubID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="VarChar(256)")]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
	}
	
	public partial class ErrorLog_SaveResult
	{
		
		private System.Nullable<long> _Column1;
		
		public ErrorLog_SaveResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="", Storage="_Column1", DbType="BigInt")]
		public System.Nullable<long> Column1
		{
			get
			{
				return this._Column1;
			}
			set
			{
				if ((this._Column1 != value))
				{
					this._Column1 = value;
				}
			}
		}
	}
	
	public partial class AdminComm_ReadDictResult
	{
		
		private long _id;
		
		private string _firstName;
		
		private string _lastName;
		
		private string _phone;
		
		private string _linkFB;
		
		public AdminComm_ReadDictResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="BigInt NOT NULL")]
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_firstName", DbType="VarChar(512)")]
		public string firstName
		{
			get
			{
				return this._firstName;
			}
			set
			{
				if ((this._firstName != value))
				{
					this._firstName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lastName", DbType="VarChar(512)")]
		public string lastName
		{
			get
			{
				return this._lastName;
			}
			set
			{
				if ((this._lastName != value))
				{
					this._lastName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_phone", DbType="VarChar(32)")]
		public string phone
		{
			get
			{
				return this._phone;
			}
			set
			{
				if ((this._phone != value))
				{
					this._phone = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_linkFB", DbType="VarChar(512)")]
		public string linkFB
		{
			get
			{
				return this._linkFB;
			}
			set
			{
				if ((this._linkFB != value))
				{
					this._linkFB = value;
				}
			}
		}
	}
	
	public partial class SessionReq_SaveResult
	{
		
		private System.Nullable<long> _id;
		
		private long _ownerHubID;
		
		public SessionReq_SaveResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="BigInt")]
		public System.Nullable<long> id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ownerHubID", DbType="BigInt NOT NULL")]
		public long ownerHubID
		{
			get
			{
				return this._ownerHubID;
			}
			set
			{
				if ((this._ownerHubID != value))
				{
					this._ownerHubID = value;
				}
			}
		}
	}
	
	public partial class Session_SaveResult
	{
		
		private System.Nullable<System.Guid> _sessionID;
		
		private System.Nullable<long> _ownerHubID;
		
		private System.Nullable<bool> _isNewMember;
		
		public Session_SaveResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_sessionID", DbType="UniqueIdentifier")]
		public System.Nullable<System.Guid> sessionID
		{
			get
			{
				return this._sessionID;
			}
			set
			{
				if ((this._sessionID != value))
				{
					this._sessionID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ownerHubID", DbType="BigInt")]
		public System.Nullable<long> ownerHubID
		{
			get
			{
				return this._ownerHubID;
			}
			set
			{
				if ((this._ownerHubID != value))
				{
					this._ownerHubID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_isNewMember", DbType="Bit")]
		public System.Nullable<bool> isNewMember
		{
			get
			{
				return this._isNewMember;
			}
			set
			{
				if ((this._isNewMember != value))
				{
					this._isNewMember = value;
				}
			}
		}
	}
	
	public partial class AdminComm_SaveResult
	{
		
		private long _id;
		
		private System.Nullable<long> _ownerHubId;
		
		private string _firstName;
		
		private string _lastName;
		
		private string _phone;
		
		private string _linkFB;
		
		public AdminComm_SaveResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="BigInt NOT NULL")]
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ownerHubId", DbType="BigInt")]
		public System.Nullable<long> ownerHubId
		{
			get
			{
				return this._ownerHubId;
			}
			set
			{
				if ((this._ownerHubId != value))
				{
					this._ownerHubId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_firstName", DbType="VarChar(512)")]
		public string firstName
		{
			get
			{
				return this._firstName;
			}
			set
			{
				if ((this._firstName != value))
				{
					this._firstName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lastName", DbType="VarChar(512)")]
		public string lastName
		{
			get
			{
				return this._lastName;
			}
			set
			{
				if ((this._lastName != value))
				{
					this._lastName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_phone", DbType="VarChar(32)")]
		public string phone
		{
			get
			{
				return this._phone;
			}
			set
			{
				if ((this._phone != value))
				{
					this._phone = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_linkFB", DbType="VarChar(512)")]
		public string linkFB
		{
			get
			{
				return this._linkFB;
			}
			set
			{
				if ((this._linkFB != value))
				{
					this._linkFB = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
