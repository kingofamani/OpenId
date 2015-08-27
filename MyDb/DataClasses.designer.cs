﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.34014
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyDb
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="TESTDB")]
	public partial class DataClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 擴充性方法定義
    partial void OnCreated();
    partial void InsertRole(Role instance);
    partial void UpdateRole(Role instance);
    partial void DeleteRole(Role instance);
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    partial void InsertRoleUser(RoleUser instance);
    partial void UpdateRoleUser(RoleUser instance);
    partial void DeleteRoleUser(RoleUser instance);
    partial void InsertSchool(School instance);
    partial void UpdateSchool(School instance);
    partial void DeleteSchool(School instance);
    #endregion
		
		public DataClassesDataContext() : 
				base(global::MyDb.Properties.Settings.Default.TESTDBConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Role> Role
		{
			get
			{
				return this.GetTable<Role>();
			}
		}
		
		public System.Data.Linq.Table<User> User
		{
			get
			{
				return this.GetTable<User>();
			}
		}
		
		public System.Data.Linq.Table<RoleUser> RoleUser
		{
			get
			{
				return this.GetTable<RoleUser>();
			}
		}
		
		public System.Data.Linq.Table<School> School
		{
			get
			{
				return this.GetTable<School>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Role")]
	public partial class Role : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _role_id;
		
		private string _name;
		
		private string _cname;
		
		private EntitySet<RoleUser> _RoleUser;
		
    #region 擴充性方法定義
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Onrole_idChanging(int value);
    partial void Onrole_idChanged();
    partial void OnnameChanging(string value);
    partial void OnnameChanged();
    partial void OncnameChanging(string value);
    partial void OncnameChanged();
    #endregion
		
		public Role()
		{
			this._RoleUser = new EntitySet<RoleUser>(new Action<RoleUser>(this.attach_RoleUser), new Action<RoleUser>(this.detach_RoleUser));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_role_id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int role_id
		{
			get
			{
				return this._role_id;
			}
			set
			{
				if ((this._role_id != value))
				{
					this.Onrole_idChanging(value);
					this.SendPropertyChanging();
					this._role_id = value;
					this.SendPropertyChanged("role_id");
					this.Onrole_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
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
					this.OnnameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("name");
					this.OnnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cname", DbType="NVarChar(50)")]
		public string cname
		{
			get
			{
				return this._cname;
			}
			set
			{
				if ((this._cname != value))
				{
					this.OncnameChanging(value);
					this.SendPropertyChanging();
					this._cname = value;
					this.SendPropertyChanged("cname");
					this.OncnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Role_RoleUser", Storage="_RoleUser", ThisKey="role_id", OtherKey="role_id")]
		public EntitySet<RoleUser> RoleUser
		{
			get
			{
				return this._RoleUser;
			}
			set
			{
				this._RoleUser.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_RoleUser(RoleUser entity)
		{
			this.SendPropertyChanging();
			entity.Role = this;
		}
		
		private void detach_RoleUser(RoleUser entity)
		{
			this.SendPropertyChanging();
			entity.Role = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.[User]")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _sn;
		
		private string _user_id;
		
		private string _pwd;
		
		private string _name;
		
		private string _email;
		
		private EntitySet<RoleUser> _RoleUser;
		
    #region 擴充性方法定義
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnsnChanging(int value);
    partial void OnsnChanged();
    partial void Onuser_idChanging(string value);
    partial void Onuser_idChanged();
    partial void OnpwdChanging(string value);
    partial void OnpwdChanged();
    partial void OnnameChanging(string value);
    partial void OnnameChanged();
    partial void OnemailChanging(string value);
    partial void OnemailChanged();
    #endregion
		
		public User()
		{
			this._RoleUser = new EntitySet<RoleUser>(new Action<RoleUser>(this.attach_RoleUser), new Action<RoleUser>(this.detach_RoleUser));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_sn", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int sn
		{
			get
			{
				return this._sn;
			}
			set
			{
				if ((this._sn != value))
				{
					this.OnsnChanging(value);
					this.SendPropertyChanging();
					this._sn = value;
					this.SendPropertyChanged("sn");
					this.OnsnChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_user_id", DbType="NVarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string user_id
		{
			get
			{
				return this._user_id;
			}
			set
			{
				if ((this._user_id != value))
				{
					this.Onuser_idChanging(value);
					this.SendPropertyChanging();
					this._user_id = value;
					this.SendPropertyChanged("user_id");
					this.Onuser_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_pwd", DbType="NVarChar(50)")]
		public string pwd
		{
			get
			{
				return this._pwd;
			}
			set
			{
				if ((this._pwd != value))
				{
					this.OnpwdChanging(value);
					this.SendPropertyChanging();
					this._pwd = value;
					this.SendPropertyChanged("pwd");
					this.OnpwdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="NVarChar(50)")]
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
					this.OnnameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("name");
					this.OnnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_email", DbType="NVarChar(50)")]
		public string email
		{
			get
			{
				return this._email;
			}
			set
			{
				if ((this._email != value))
				{
					this.OnemailChanging(value);
					this.SendPropertyChanging();
					this._email = value;
					this.SendPropertyChanged("email");
					this.OnemailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_RoleUser", Storage="_RoleUser", ThisKey="user_id", OtherKey="user_id")]
		public EntitySet<RoleUser> RoleUser
		{
			get
			{
				return this._RoleUser;
			}
			set
			{
				this._RoleUser.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_RoleUser(RoleUser entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void detach_RoleUser(RoleUser entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.RoleUser")]
	public partial class RoleUser : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ru_id;
		
		private string _user_id;
		
		private int _role_id;
		
		private EntityRef<Role> _Role;
		
		private EntityRef<User> _User;
		
    #region 擴充性方法定義
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Onru_idChanging(int value);
    partial void Onru_idChanged();
    partial void Onuser_idChanging(string value);
    partial void Onuser_idChanged();
    partial void Onrole_idChanging(int value);
    partial void Onrole_idChanged();
    #endregion
		
		public RoleUser()
		{
			this._Role = default(EntityRef<Role>);
			this._User = default(EntityRef<User>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ru_id", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int ru_id
		{
			get
			{
				return this._ru_id;
			}
			set
			{
				if ((this._ru_id != value))
				{
					this.Onru_idChanging(value);
					this.SendPropertyChanging();
					this._ru_id = value;
					this.SendPropertyChanged("ru_id");
					this.Onru_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_user_id", DbType="NVarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string user_id
		{
			get
			{
				return this._user_id;
			}
			set
			{
				if ((this._user_id != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.Onuser_idChanging(value);
					this.SendPropertyChanging();
					this._user_id = value;
					this.SendPropertyChanged("user_id");
					this.Onuser_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_role_id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int role_id
		{
			get
			{
				return this._role_id;
			}
			set
			{
				if ((this._role_id != value))
				{
					if (this._Role.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.Onrole_idChanging(value);
					this.SendPropertyChanging();
					this._role_id = value;
					this.SendPropertyChanged("role_id");
					this.Onrole_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Role_RoleUser", Storage="_Role", ThisKey="role_id", OtherKey="role_id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public Role Role
		{
			get
			{
				return this._Role.Entity;
			}
			set
			{
				Role previousValue = this._Role.Entity;
				if (((previousValue != value) 
							|| (this._Role.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Role.Entity = null;
						previousValue.RoleUser.Remove(this);
					}
					this._Role.Entity = value;
					if ((value != null))
					{
						value.RoleUser.Add(this);
						this._role_id = value.role_id;
					}
					else
					{
						this._role_id = default(int);
					}
					this.SendPropertyChanged("Role");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_RoleUser", Storage="_User", ThisKey="user_id", OtherKey="user_id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.RoleUser.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.RoleUser.Add(this);
						this._user_id = value.user_id;
					}
					else
					{
						this._user_id = default(string);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.School")]
	public partial class School : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _sch_id;
		
		private string _name;
		
		private string _alias;
		
		private string _domain;
		
		private string _tel;
		
    #region 擴充性方法定義
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Onsch_idChanging(string value);
    partial void Onsch_idChanged();
    partial void OnnameChanging(string value);
    partial void OnnameChanged();
    partial void OnaliasChanging(string value);
    partial void OnaliasChanged();
    partial void OndomainChanging(string value);
    partial void OndomainChanged();
    partial void OntelChanging(string value);
    partial void OntelChanged();
    #endregion
		
		public School()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_sch_id", DbType="NVarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string sch_id
		{
			get
			{
				return this._sch_id;
			}
			set
			{
				if ((this._sch_id != value))
				{
					this.Onsch_idChanging(value);
					this.SendPropertyChanging();
					this._sch_id = value;
					this.SendPropertyChanged("sch_id");
					this.Onsch_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="NVarChar(20) NOT NULL", CanBeNull=false)]
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
					this.OnnameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("name");
					this.OnnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_alias", DbType="NVarChar(20)")]
		public string alias
		{
			get
			{
				return this._alias;
			}
			set
			{
				if ((this._alias != value))
				{
					this.OnaliasChanging(value);
					this.SendPropertyChanging();
					this._alias = value;
					this.SendPropertyChanged("alias");
					this.OnaliasChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_domain", DbType="NVarChar(100)")]
		public string domain
		{
			get
			{
				return this._domain;
			}
			set
			{
				if ((this._domain != value))
				{
					this.OndomainChanging(value);
					this.SendPropertyChanging();
					this._domain = value;
					this.SendPropertyChanged("domain");
					this.OndomainChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_tel", DbType="NVarChar(50)")]
		public string tel
		{
			get
			{
				return this._tel;
			}
			set
			{
				if ((this._tel != value))
				{
					this.OntelChanging(value);
					this.SendPropertyChanging();
					this._tel = value;
					this.SendPropertyChanged("tel");
					this.OntelChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591