﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.34209
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BachelorLibAPI.Data.Course_DB
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Course_DB")]
	public partial class TransitsDataClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Определения метода расширяемости
    partial void OnCreated();
    partial void InsertCity(City instance);
    partial void UpdateCity(City instance);
    partial void DeleteCity(City instance);
    partial void InsertTransitStady(TransitStady instance);
    partial void UpdateTransitStady(TransitStady instance);
    partial void DeleteTransitStady(TransitStady instance);
    partial void InsertConsignment(Consignment instance);
    partial void UpdateConsignment(Consignment instance);
    partial void DeleteConsignment(Consignment instance);
    partial void InsertContact(Contact instance);
    partial void UpdateContact(Contact instance);
    partial void DeleteContact(Contact instance);
    partial void InsertDriver(Driver instance);
    partial void UpdateDriver(Driver instance);
    partial void DeleteDriver(Driver instance);
    partial void InsertRegion(Region instance);
    partial void UpdateRegion(Region instance);
    partial void DeleteRegion(Region instance);
    partial void InsertTransit(Transit instance);
    partial void UpdateTransit(Transit instance);
    partial void DeleteTransit(Transit instance);
    #endregion
		
		public TransitsDataClassesDataContext() : 
				base(global::BachelorLibAPI.Properties.Settings.Default.Course_DBConnectionString2, mappingSource)
		{
			OnCreated();
		}
		
		public TransitsDataClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TransitsDataClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TransitsDataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TransitsDataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<City> City
		{
			get
			{
				return this.GetTable<City>();
			}
		}
		
		public System.Data.Linq.Table<TransitStady> TransitStady
		{
			get
			{
				return this.GetTable<TransitStady>();
			}
		}
		
		public System.Data.Linq.Table<Consignment> Consignment
		{
			get
			{
				return this.GetTable<Consignment>();
			}
		}
		
		public System.Data.Linq.Table<Contact> Contact
		{
			get
			{
				return this.GetTable<Contact>();
			}
		}
		
		public System.Data.Linq.Table<Driver> Driver
		{
			get
			{
				return this.GetTable<Driver>();
			}
		}
		
		public System.Data.Linq.Table<Region> Region
		{
			get
			{
				return this.GetTable<Region>();
			}
		}
		
		public System.Data.Linq.Table<Route> Route
		{
			get
			{
				return this.GetTable<Route>();
			}
		}
		
		public System.Data.Linq.Table<Transit> Transit
		{
			get
			{
				return this.GetTable<Transit>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Cities")]
	public partial class City : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private int _RegionID;
		
		private string _Name;
		
		private int _ParkingTime;
		
		private EntitySet<TransitStady> _TransitStady;
		
		private EntityRef<Region> _Region;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnRegionIDChanging(int value);
    partial void OnRegionIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnParkingTimeChanging(int value);
    partial void OnParkingTimeChanged();
    #endregion
		
		public City()
		{
			this._TransitStady = new EntitySet<TransitStady>(new Action<TransitStady>(this.attach_TransitStady), new Action<TransitStady>(this.detach_TransitStady));
			this._Region = default(EntityRef<Region>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RegionID", DbType="Int NOT NULL")]
		public int RegionID
		{
			get
			{
				return this._RegionID;
			}
			set
			{
				if ((this._RegionID != value))
				{
					if (this._Region.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRegionIDChanging(value);
					this.SendPropertyChanging();
					this._RegionID = value;
					this.SendPropertyChanged("RegionID");
					this.OnRegionIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParkingTime", DbType="Int NOT NULL")]
		public int ParkingTime
		{
			get
			{
				return this._ParkingTime;
			}
			set
			{
				if ((this._ParkingTime != value))
				{
					this.OnParkingTimeChanging(value);
					this.SendPropertyChanging();
					this._ParkingTime = value;
					this.SendPropertyChanged("ParkingTime");
					this.OnParkingTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="City_TransitStady", Storage="_TransitStady", ThisKey="ID", OtherKey="CityID")]
		public EntitySet<TransitStady> TransitStady
		{
			get
			{
				return this._TransitStady;
			}
			set
			{
				this._TransitStady.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Region_City", Storage="_Region", ThisKey="RegionID", OtherKey="ID", IsForeignKey=true)]
		public Region Region
		{
			get
			{
				return this._Region.Entity;
			}
			set
			{
				Region previousValue = this._Region.Entity;
				if (((previousValue != value) 
							|| (this._Region.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Region.Entity = null;
						previousValue.City.Remove(this);
					}
					this._Region.Entity = value;
					if ((value != null))
					{
						value.City.Add(this);
						this._RegionID = value.ID;
					}
					else
					{
						this._RegionID = default(int);
					}
					this.SendPropertyChanged("Region");
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
		
		private void attach_TransitStady(TransitStady entity)
		{
			this.SendPropertyChanging();
			entity.City = this;
		}
		
		private void detach_TransitStady(TransitStady entity)
		{
			this.SendPropertyChanging();
			entity.City = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TransitStadies")]
	public partial class TransitStady : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _TransID;
		
		private int _CityID;
		
		private System.DateTime _LocationTime;
		
		private EntityRef<City> _City;
		
		private EntityRef<Transit> _Transit;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnTransIDChanging(int value);
    partial void OnTransIDChanged();
    partial void OnCityIDChanging(int value);
    partial void OnCityIDChanged();
    partial void OnLocationTimeChanging(System.DateTime value);
    partial void OnLocationTimeChanged();
    #endregion
		
		public TransitStady()
		{
			this._City = default(EntityRef<City>);
			this._Transit = default(EntityRef<Transit>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TransID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int TransID
		{
			get
			{
				return this._TransID;
			}
			set
			{
				if ((this._TransID != value))
				{
					if (this._Transit.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnTransIDChanging(value);
					this.SendPropertyChanging();
					this._TransID = value;
					this.SendPropertyChanged("TransID");
					this.OnTransIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CityID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int CityID
		{
			get
			{
				return this._CityID;
			}
			set
			{
				if ((this._CityID != value))
				{
					if (this._City.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCityIDChanging(value);
					this.SendPropertyChanging();
					this._CityID = value;
					this.SendPropertyChanged("CityID");
					this.OnCityIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LocationTime", DbType="DateTime2 NOT NULL", IsPrimaryKey=true)]
		public System.DateTime LocationTime
		{
			get
			{
				return this._LocationTime;
			}
			set
			{
				if ((this._LocationTime != value))
				{
					this.OnLocationTimeChanging(value);
					this.SendPropertyChanging();
					this._LocationTime = value;
					this.SendPropertyChanged("LocationTime");
					this.OnLocationTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="City_TransitStady", Storage="_City", ThisKey="CityID", OtherKey="ID", IsForeignKey=true)]
		public City City
		{
			get
			{
				return this._City.Entity;
			}
			set
			{
				City previousValue = this._City.Entity;
				if (((previousValue != value) 
							|| (this._City.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._City.Entity = null;
						previousValue.TransitStady.Remove(this);
					}
					this._City.Entity = value;
					if ((value != null))
					{
						value.TransitStady.Add(this);
						this._CityID = value.ID;
					}
					else
					{
						this._CityID = default(int);
					}
					this.SendPropertyChanged("City");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Transit_TransitStady", Storage="_Transit", ThisKey="TransID", OtherKey="ID", IsForeignKey=true)]
		public Transit Transit
		{
			get
			{
				return this._Transit.Entity;
			}
			set
			{
				Transit previousValue = this._Transit.Entity;
				if (((previousValue != value) 
							|| (this._Transit.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Transit.Entity = null;
						previousValue.TransitStady.Remove(this);
					}
					this._Transit.Entity = value;
					if ((value != null))
					{
						value.TransitStady.Add(this);
						this._TransID = value.ID;
					}
					else
					{
						this._TransID = default(int);
					}
					this.SendPropertyChanged("Transit");
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Consignments")]
	public partial class Consignment : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _Name;
		
		private int _Danger_degree;
		
		private string _After_crash;
		
		private EntitySet<Transit> _Transit;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnDanger_degreeChanging(int value);
    partial void OnDanger_degreeChanged();
    partial void OnAfter_crashChanging(string value);
    partial void OnAfter_crashChanged();
    #endregion
		
		public Consignment()
		{
			this._Transit = new EntitySet<Transit>(new Action<Transit>(this.attach_Transit), new Action<Transit>(this.detach_Transit));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Danger_degree", DbType="Int NOT NULL")]
		public int Danger_degree
		{
			get
			{
				return this._Danger_degree;
			}
			set
			{
				if ((this._Danger_degree != value))
				{
					this.OnDanger_degreeChanging(value);
					this.SendPropertyChanging();
					this._Danger_degree = value;
					this.SendPropertyChanged("Danger_degree");
					this.OnDanger_degreeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_After_crash", DbType="NVarChar(MAX)")]
		public string After_crash
		{
			get
			{
				return this._After_crash;
			}
			set
			{
				if ((this._After_crash != value))
				{
					this.OnAfter_crashChanging(value);
					this.SendPropertyChanging();
					this._After_crash = value;
					this.SendPropertyChanged("After_crash");
					this.OnAfter_crashChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Consignment_Transit", Storage="_Transit", ThisKey="ID", OtherKey="ConsID")]
		public EntitySet<Transit> Transit
		{
			get
			{
				return this._Transit;
			}
			set
			{
				this._Transit.Assign(value);
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
		
		private void attach_Transit(Transit entity)
		{
			this.SendPropertyChanging();
			entity.Consignment = this;
		}
		
		private void detach_Transit(Transit entity)
		{
			this.SendPropertyChanging();
			entity.Consignment = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Contacts")]
	public partial class Contact : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _DriverID;
		
		private string _ContactNumber;
		
		private EntityRef<Driver> _Driver;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnDriverIDChanging(int value);
    partial void OnDriverIDChanged();
    partial void OnContactNumberChanging(string value);
    partial void OnContactNumberChanged();
    #endregion
		
		public Contact()
		{
			this._Driver = default(EntityRef<Driver>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DriverID", DbType="Int NOT NULL")]
		public int DriverID
		{
			get
			{
				return this._DriverID;
			}
			set
			{
				if ((this._DriverID != value))
				{
					if (this._Driver.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnDriverIDChanging(value);
					this.SendPropertyChanging();
					this._DriverID = value;
					this.SendPropertyChanged("DriverID");
					this.OnDriverIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContactNumber", DbType="NVarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string ContactNumber
		{
			get
			{
				return this._ContactNumber;
			}
			set
			{
				if ((this._ContactNumber != value))
				{
					this.OnContactNumberChanging(value);
					this.SendPropertyChanging();
					this._ContactNumber = value;
					this.SendPropertyChanged("ContactNumber");
					this.OnContactNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Driver_Contact", Storage="_Driver", ThisKey="DriverID", OtherKey="ID", IsForeignKey=true)]
		public Driver Driver
		{
			get
			{
				return this._Driver.Entity;
			}
			set
			{
				Driver previousValue = this._Driver.Entity;
				if (((previousValue != value) 
							|| (this._Driver.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Driver.Entity = null;
						previousValue.Contact.Remove(this);
					}
					this._Driver.Entity = value;
					if ((value != null))
					{
						value.Contact.Add(this);
						this._DriverID = value.ID;
					}
					else
					{
						this._DriverID = default(int);
					}
					this.SendPropertyChanged("Driver");
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Drivers")]
	public partial class Driver : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _LName;
		
		private string _Name;
		
		private string _MName;
		
		private EntitySet<Contact> _Contact;
		
		private EntitySet<Transit> _Transit;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnLNameChanging(string value);
    partial void OnLNameChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnMNameChanging(string value);
    partial void OnMNameChanged();
    #endregion
		
		public Driver()
		{
			this._Contact = new EntitySet<Contact>(new Action<Contact>(this.attach_Contact), new Action<Contact>(this.detach_Contact));
			this._Transit = new EntitySet<Transit>(new Action<Transit>(this.attach_Transit), new Action<Transit>(this.detach_Transit));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string LName
		{
			get
			{
				return this._LName;
			}
			set
			{
				if ((this._LName != value))
				{
					this.OnLNameChanging(value);
					this.SendPropertyChanging();
					this._LName = value;
					this.SendPropertyChanged("LName");
					this.OnLNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MName", DbType="NVarChar(50)")]
		public string MName
		{
			get
			{
				return this._MName;
			}
			set
			{
				if ((this._MName != value))
				{
					this.OnMNameChanging(value);
					this.SendPropertyChanging();
					this._MName = value;
					this.SendPropertyChanged("MName");
					this.OnMNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Driver_Contact", Storage="_Contact", ThisKey="ID", OtherKey="DriverID")]
		public EntitySet<Contact> Contact
		{
			get
			{
				return this._Contact;
			}
			set
			{
				this._Contact.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Driver_Transit", Storage="_Transit", ThisKey="ID", OtherKey="DriverID")]
		public EntitySet<Transit> Transit
		{
			get
			{
				return this._Transit;
			}
			set
			{
				this._Transit.Assign(value);
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
		
		private void attach_Contact(Contact entity)
		{
			this.SendPropertyChanging();
			entity.Driver = this;
		}
		
		private void detach_Contact(Contact entity)
		{
			this.SendPropertyChanging();
			entity.Driver = null;
		}
		
		private void attach_Transit(Transit entity)
		{
			this.SendPropertyChanging();
			entity.Driver = this;
		}
		
		private void detach_Transit(Transit entity)
		{
			this.SendPropertyChanging();
			entity.Driver = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Regions")]
	public partial class Region : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _Name;
		
		private EntitySet<City> _City;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public Region()
		{
			this._City = new EntitySet<City>(new Action<City>(this.attach_City), new Action<City>(this.detach_City));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Region_City", Storage="_City", ThisKey="ID", OtherKey="RegionID")]
		public EntitySet<City> City
		{
			get
			{
				return this._City;
			}
			set
			{
				this._City.Assign(value);
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
		
		private void attach_City(City entity)
		{
			this.SendPropertyChanging();
			entity.Region = this;
		}
		
		private void detach_City(City entity)
		{
			this.SendPropertyChanging();
			entity.Region = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Routes")]
	public partial class Route
	{
		
		private int _TransID;
		
		private System.DateTime _StartTime;
		
		private System.Nullable<System.DateTime> _ArrTime;
		
		private string _CitiesList;
		
		private bool _Status;
		
		public Route()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TransID", DbType="Int NOT NULL")]
		public int TransID
		{
			get
			{
				return this._TransID;
			}
			set
			{
				if ((this._TransID != value))
				{
					this._TransID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StartTime", DbType="DateTime2 NOT NULL")]
		public System.DateTime StartTime
		{
			get
			{
				return this._StartTime;
			}
			set
			{
				if ((this._StartTime != value))
				{
					this._StartTime = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ArrTime", DbType="DateTime2")]
		public System.Nullable<System.DateTime> ArrTime
		{
			get
			{
				return this._ArrTime;
			}
			set
			{
				if ((this._ArrTime != value))
				{
					this._ArrTime = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CitiesList", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string CitiesList
		{
			get
			{
				return this._CitiesList;
			}
			set
			{
				if ((this._CitiesList != value))
				{
					this._CitiesList = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="Bit NOT NULL")]
		public bool Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this._Status = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Transits")]
	public partial class Transit : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private int _DriverID;
		
		private int _ConsID;
		
		private EntitySet<TransitStady> _TransitStady;
		
		private EntityRef<Consignment> _Consignment;
		
		private EntityRef<Driver> _Driver;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnDriverIDChanging(int value);
    partial void OnDriverIDChanged();
    partial void OnConsIDChanging(int value);
    partial void OnConsIDChanged();
    #endregion
		
		public Transit()
		{
			this._TransitStady = new EntitySet<TransitStady>(new Action<TransitStady>(this.attach_TransitStady), new Action<TransitStady>(this.detach_TransitStady));
			this._Consignment = default(EntityRef<Consignment>);
			this._Driver = default(EntityRef<Driver>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DriverID", DbType="Int NOT NULL")]
		public int DriverID
		{
			get
			{
				return this._DriverID;
			}
			set
			{
				if ((this._DriverID != value))
				{
					if (this._Driver.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnDriverIDChanging(value);
					this.SendPropertyChanging();
					this._DriverID = value;
					this.SendPropertyChanged("DriverID");
					this.OnDriverIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ConsID", DbType="Int NOT NULL")]
		public int ConsID
		{
			get
			{
				return this._ConsID;
			}
			set
			{
				if ((this._ConsID != value))
				{
					if (this._Consignment.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnConsIDChanging(value);
					this.SendPropertyChanging();
					this._ConsID = value;
					this.SendPropertyChanged("ConsID");
					this.OnConsIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Transit_TransitStady", Storage="_TransitStady", ThisKey="ID", OtherKey="TransID")]
		public EntitySet<TransitStady> TransitStady
		{
			get
			{
				return this._TransitStady;
			}
			set
			{
				this._TransitStady.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Consignment_Transit", Storage="_Consignment", ThisKey="ConsID", OtherKey="ID", IsForeignKey=true)]
		public Consignment Consignment
		{
			get
			{
				return this._Consignment.Entity;
			}
			set
			{
				Consignment previousValue = this._Consignment.Entity;
				if (((previousValue != value) 
							|| (this._Consignment.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Consignment.Entity = null;
						previousValue.Transit.Remove(this);
					}
					this._Consignment.Entity = value;
					if ((value != null))
					{
						value.Transit.Add(this);
						this._ConsID = value.ID;
					}
					else
					{
						this._ConsID = default(int);
					}
					this.SendPropertyChanged("Consignment");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Driver_Transit", Storage="_Driver", ThisKey="DriverID", OtherKey="ID", IsForeignKey=true)]
		public Driver Driver
		{
			get
			{
				return this._Driver.Entity;
			}
			set
			{
				Driver previousValue = this._Driver.Entity;
				if (((previousValue != value) 
							|| (this._Driver.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Driver.Entity = null;
						previousValue.Transit.Remove(this);
					}
					this._Driver.Entity = value;
					if ((value != null))
					{
						value.Transit.Add(this);
						this._DriverID = value.ID;
					}
					else
					{
						this._DriverID = default(int);
					}
					this.SendPropertyChanged("Driver");
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
		
		private void attach_TransitStady(TransitStady entity)
		{
			this.SendPropertyChanging();
			entity.Transit = this;
		}
		
		private void detach_TransitStady(TransitStady entity)
		{
			this.SendPropertyChanging();
			entity.Transit = null;
		}
	}
}
#pragma warning restore 1591