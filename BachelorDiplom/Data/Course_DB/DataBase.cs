using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace BachelorLibAPI.Data.Course_DB
{
    /// <summary>
    /// Реализация интерфейса IDataHandler. Данные хранятся в базе данных Course_DB, созданной в Sql Server 2008
    /// </summary>
    public sealed class DataHandler : IDataHandler
    {
        [Table(Name = "Driver")]
        private class Drivers
        {
            private int _id;
            private string _lName;
            private string _fName;
            private string _mName;

            public int ID
            {
                get { return _id; }
                set { _id = value; }
            }

            public string LastName
            {
                get { return _lName; }
                set { _lName = value; }
            }

            public string FirstName
            {
                get { return _fName; }
                set { _fName = value; }
            }

            public string MiddleName
            {
                get { return _mName; }
                set { _mName = value; }
            }
        }

        [Table(Name = "Consignment")]
        private class Consignments
        {
            private int _id;
            private string _name;
            private int _dangerDegree;
            private string _afterCrash;

            public int ID
            {
                get { return _id; }
                set { _id = value; }
            }

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public int DangerDegree
            {
                get { return _dangerDegree; }
                set { _dangerDegree = value; }
            }

            public string AfterCrash
            {
                get { return _afterCrash; }
                set { _afterCrash = value; }
            }
        }

        [Table(Name = "Transit")]
        private class Transits
        {
            private int _id;
            private int _driverID;
            private int _consID;

            public int ID
            {
                get { return _id; }
                set { _id = value; }
            }

            public int DriverID
            {
                get { return _driverID; }
                set { _driverID = value; }
            }

            public int ConsignmentID
            {
                get { return _consID; }
                set { _consID = value; }
            }
        }

        [Table(Name = "City")]
        private class Cities
        {
            private int _id;
            private int _regionID;
            private string _name;
            private int _parkingTime;

            public int ID
            {
                get { return _id; }
                set { _id = value; }
            }

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public int RegionID
            {
                get { return _regionID; }
                set { _regionID = value; }
            }

            public int ParkingTime
            {
                get { return _parkingTime; }
                set { _parkingTime = value; }
            }
        }

        [Table(Name = "Region")]
        private class Regions
        {
            private int _id;
            private string _name;

            public int ID
            {
                get { return _id; }
                set { _id = value; }
            }

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }
        }

        [Table(Name = "Route")]
        private class Routes
        {
            private int _transitID;
            private System.DateTime _startTime;
            private System.DateTime _arrTime;
            private string _citiesList; 
            private bool _status;

            public int TransitID
            {
                get { return _transitID; }
                set { _transitID = value; }
            }

            public System.DateTime StartTime
            {
                get { return _startTime; }
                set { _startTime = value; }
            }

            public System.DateTime ArrivalTime
            {
                get { return _arrTime; }
                set { _arrTime = value; }
            }
            /// <summary>
            /// Хранит строку id городов через пробел
            /// </summary>
            public string CitiesList
            {
                get { return _citiesList; }
                set { _citiesList = value; }
            }

            public bool Status
            {
                get { return _status; }
                set { _status = value; }
            }
        }

        [Table(Name = "TransitStady")]
        private class TransitStadies
        {
            private int _transID;
            private int _cityID;
            private DateTime _locationTime;

            public int TransitID
            {
                get { return _transID; }
                set { _transID = value; }
            }

            public int CityID
            {
                get { return _cityID; }
                set { _cityID = value; }
            }

            public DateTime LocationTime
            {
                get { return _locationTime; }
                set { _locationTime = value; }
            }
        }

        [Table(Name = "Contact")]
        private class Contacts
        {
            private int _driverID;
            private string _contact;

            public int DriverID
            {
                get { return _driverID; }
                set { _driverID = value; }
            }

            public string ContactNumber
            {
                get { return _contact; }
                set { _contact = value; }
            }
        }

        private static readonly Lazy<DataHandler> lazy =
            new Lazy<DataHandler>(() => new DataHandler());

        public static DataHandler Instance
        {
            get { return lazy.Value; }
        }

        private TransitsDataClassesDataContext _db;

        private int _lastDriverID;
        private int _lastConsID;
        private int _lastTransID;
        private int _lastCityID;
        private int _lastRegionID;
        private List<int> _missedDriverIDs;

        public List<int> MissedDriverIDs
        {
            get { return _missedDriverIDs; }
        }

        private DataHandler()
        {
            _db = new TransitsDataClassesDataContext();

            _lastDriverID = (GetTableLength<Driver>() == 0) ? 0 : GetLastInTable<Driver>().ID;
            _lastConsID = (GetTableLength<Consignment>() == 0) ? 0 : GetLastInTable<Consignment>().ID;
            _lastTransID = (GetTableLength<Transit>() == 0) ? 0 : GetLastInTable<Transit>().ID;
            _lastCityID = (GetTableLength<City>() == 0) ? 0 : GetLastInTable<City>().ID;
            _lastRegionID = (GetTableLength<Region>() == 0) ? 0 : GetLastInTable<Region>().ID;
            _missedDriverIDs = new List<int>();
        }

        public void AddNewContact(int id, string number)
        {
            Contact newContact = new Contact();

            newContact.DriverID = id;
            newContact.ContactNumber = number;

            _db.Contact.InsertOnSubmit(newContact);
            _db.SubmitChanges();
        }

        public void DelContacts(int driverID)
        {
            var query =
                from contact in _db.Contact
                where contact.DriverID == driverID
                select contact;

            foreach (var contact in query)
            {
                _db.Contact.DeleteOnSubmit(contact);
            }

            //_db.SubmitChanges();
        }

        public int AddNewDriver(string lName, string name, string mName)
        {
            Driver newDriver = new Driver();

            newDriver.ID = ++_lastDriverID;
            newDriver.Name = name;
            newDriver.LName = lName;            
            newDriver.MName = mName;

            _missedDriverIDs.Remove(_lastDriverID);

            _db.Driver.InsertOnSubmit(newDriver);
            _db.SubmitChanges();

            return _lastDriverID;
        }

        public void DelDriver(int driverID)
        {
            var query =
                from driver in _db.Driver
                where driver.ID == driverID
                select driver;

            foreach (var driver in query)
            {
                _db.Driver.DeleteOnSubmit(driver);
            }
            MissedDriverIDs.Add(driverID);
            //_db.SubmitChanges();
        }

        public int AddNewConsignment(string name, int dangerDegree, string afterCrash)
        {
            Consignment newCons = new Consignment();

            newCons.ID = ++_lastConsID;
            newCons.Name = name;
            newCons.Danger_degree = dangerDegree;
            newCons.After_crash = afterCrash;

            _db.Consignment.InsertOnSubmit(newCons);
            _db.SubmitChanges();

            return _lastConsID;
        }

        public int AddNewTransit(int driverID, int consignmentID)
        {
            Transit newTrans = new Transit();

            newTrans.ID = ++_lastTransID;
            newTrans.DriverID = driverID;
            newTrans.ConsID = consignmentID;

            _db.Transit.InsertOnSubmit(newTrans);
            _db.SubmitChanges();

            return _lastTransID;
        }

        public void DelTransit(int transID)
        {
            var query =
                from trans in _db.Transit
                where trans.ID == transID
                select trans;

            foreach (var trans in query)
            {
                _db.Transit.DeleteOnSubmit(trans);
            }

            //_db.SubmitChanges();
        }

        public void AddNewRoute(int transitID, DateTime startTime, DateTime arrTime, string cities, bool status)
        {
            Route newRoute = new Route();

            newRoute.TransID = transitID;
            newRoute.StartTime = startTime;
            if (arrTime != DateTime.MinValue)
                newRoute.ArrTime = arrTime;
            newRoute.CitiesList = cities;
            newRoute.Status = status;

            _db.Route.InsertOnSubmit(newRoute);
            _db.SubmitChanges();
        }

        public void DelRoute(int transID)
        {
            var query =
                from route in _db.Route
                where route.TransID == transID
                select route;

            foreach (var route in query)
            {
                _db.Route.DeleteOnSubmit(route);
            }

            //_db.SubmitChanges();
        }

        public int AddNewCity(string name, int parkingTime, int regionID)
        {
            City newCity = new City();

            newCity.ID = ++_lastCityID;
            newCity.Name = name;
            newCity.ParkingTime = parkingTime;
            newCity.RegionID = regionID;

            _db.City.InsertOnSubmit(newCity);
            _db.SubmitChanges();

            return _lastCityID;
        }

        public int AddNewRegion(string name)
        {
            Region newRegion = new Region();

            newRegion.ID = ++_lastRegionID;
            newRegion.Name = name;
            _db.Region.InsertOnSubmit(newRegion);
            _db.SubmitChanges();

            return _lastRegionID;
        }

        public void AddNewTransitStady(int transitID, int cityID, DateTime noticedTime)
        {
            TransitStady newTranSt = new TransitStady();

            newTranSt.TransID = transitID;
            newTranSt.CityID = cityID;
            newTranSt.LocationTime = noticedTime;

            _db.TransitStady.InsertOnSubmit(newTranSt);
            _db.SubmitChanges();
        }

        public void SetConsignmentParameters(int consID, int dangerDegree, string afterCrash)
        {
            var query =
                from cons in _db.Consignment
                where cons.ID == consID
                select cons;

            if (query.Count() != 1)
                throw new Exception("В базе данных зарегистрировано несколько грузов с одинаковым ID.");

            query.First().Danger_degree = dangerDegree;
            query.First().After_crash = afterCrash;

            _db.SubmitChanges();
        }

        private bool CompareDates(DateTime t1, DateTime t2)
        {
            return t1.Minute == t2.Minute && t1.Hour == t2.Hour && t1.Date == t2.Date;
        }

        public void SetEndingStatus(int transID, DateTime start, DateTime arr)
        {
            var query =
                from route in _db.Route
                where route.TransID == transID && start.Minute == route.StartTime.Minute && 
                    start.Hour == route.StartTime.Hour && start.Date == route.StartTime.Date
                select route;

            if(query.Count() != 1)
                throw new Exception("В базе данных зарегистрировано несколько перевозок с одинаковым ID и временем отправления.");

            query.First().ArrTime = arr;
            query.First().Status = true;

            _db.SubmitChanges();
        }

        public void DeleteStadiesByTransitID(int transID)
        {
            var query =
                from stady in _db.TransitStady
                where stady.TransID == transID
                select stady;

            foreach (var stady in query)
            {
                _db.TransitStady.DeleteOnSubmit(stady);
            }

            //_db.SubmitChanges();
        }

        public int GetTableLength<T>()
        {
            var tblForGetting = _db.GetTable(typeof(T));
            return (from T data in tblForGetting
                    select data).
                    ToArray().
                    Length;
        }
        
        public T GetLastInTable<T>()
        {
            var tblForGetting = _db.GetTable(typeof(T));

            T[] records =
                (from T data in tblForGetting
                select data).ToArray();

            if (records.Length == 0) 
                throw new Exception("Ошибка получения записи: таблица " + typeof(T).Name + " пуста!");

            return records[records.Length - 1];
        }

        public bool HasPhoneNumber(string contact)
        {
            var query = (from cont in _db.Contact
                         where cont.ContactNumber == contact
                         select cont.DriverID).ToArray();
            return query.Length != 0;
        }

        public int[] FindDrivers(string lName, string name, string mName)
        {
            int[] query = (from driver in _db.Driver
                         where (driver.LName == lName)
                             && (driver.Name == name)
                             && (driver.MName == mName)
                         select driver.ID).ToArray();
            return query;
        }

        public int DriverWithPhoneNumber(string contact)
        {
            var query = (from cont in _db.Contact
                         where cont.ContactNumber == contact
                         select cont.DriverID).ToArray();
            return query.Length == 0 ? -1 : query[0];
        }
        
        public string GetDriversFullName(int driverID)
        {
            var query = (from driver in _db.Driver
                         where driver.ID == driverID
                         select driver.LName + " " + driver.Name + " " + driver.MName).ToArray();
            return query[0];
        }

        public List<string> GetDriversFullNames()
        {
            var query = (from driver in _db.Driver
                         select driver.LName + " " + driver.Name + " " + driver.MName).ToList<string>();
            return query;
        }

        public int GetCityID(string name)
        {
            var query = (from city in _db.City
                         where city.Name == name
                         select city.ID).ToArray();
            return query.Length == 0 ? -1 : query[0];
        }

        public string GetCityName(int cityID)
        {
            var query = (from city in _db.City
                         where city.ID == cityID
                         select city.Name).ToArray();
            return query[0];
        }

        public int GetRegionID(string name)
        {
            var query = (from reg in _db.Region
                         where reg.Name == name
                         select reg.ID).ToArray();
            return query.Length == 0 ? -1 : query[0];
        }

        public int GetConsignmentID(string name)
        {
            var query = (from cons in _db.Consignment
                         where cons.Name == name
                         select cons.ID).ToArray();
            return query.Length == 0 ? -1 : query[0];
        }

        public int GetTransitID(int driverID, DateTime start)
        {
            var query =
                (from trans in _db.Transit
                join route in _db.Route
                on trans.ID equals route.TransID
                where trans.DriverID == driverID && start.Minute == route.StartTime.Minute && 
                    start.Hour == route.StartTime.Hour && start.Date == route.StartTime.Date
                select trans.ID).ToArray();
            return query.Length == 0 ? -1 : query[0];
        }

        public List<int> GetTransitIDs(DateTime start, DateTime until, int placeID)
        {
            var query = (from stady in _db.TransitStady
                         where stady.LocationTime >= start &&
                             stady.LocationTime <= until && stady.CityID == placeID
                         select stady.TransID).ToList<int>();
            return query;
        }

        public List<int> GetTransitIDs(int driverID)
        {
            var query = (from trans in _db.Transit
                         where trans.DriverID == driverID
                         select trans.ID).ToList<int>();
            return query;
        }

        public List<int> TransitsBefore(DateTime time)
        {
            var query = (from route in _db.Route
                         where route.StartTime < time
                         select route.TransID).ToList<int>();
            return query;
        }

        public List<int> EndedTransits()
        {
            var query = (from route in _db.Route
                         where route.Status
                         select route.TransID).ToList<int>();
            return query;
        }

        public List<string> GetCitiesNames()
        {
            var query = (from city in _db.City
                         select city.Name).ToList<string>();
            return query;
        }

        public List<int> GetCitiesInRegion(int regionID)
        {
            var query = (from city in _db.City
                         where city.RegionID == regionID
                         select city.ID).ToList<int>();
            return query;
        }

        public List<string> GetConsignmentsNames()
        {
            var query = (from cons in _db.Consignment
                         select cons.Name).ToList<string>();
            return query;
        }

        public List<string> GetNumbers()
        {
            var query = (from cont in _db.Contact
                         select cont.ContactNumber).ToList<string>();
            return query;
        }

        public List<string> GetRegionsNames()
        {
            var query = (from reg in _db.Region
                         select reg.Name).ToList<string>();
            return query;
        }

        public int GetParkingMinutesOfTheCity(int cityID)
        {
            var query = (from city in _db.City
                         where city.ID == cityID
                         select city.ParkingTime).ToArray();
            return query.Length == 0 ? -1 : query[0];
        }

        public int GetDriverID(int transID)
        {
            var query = (from row in _db.Transit
                         where row.ID == transID
                         select row.DriverID).ToArray();
            return query[0];
        }

        public int GetConsignmentID(int transID)
        {
            var query = (from row in _db.Transit
                         where row.ID == transID
                         select row.ConsID).ToArray();
            return query[0];
        }

        public string GetDriverName(int driverID)
        {
            var query = (from row in _db.Driver
                         where row.ID == driverID
                         select row.Name).ToArray();
            return query[0];
        }

        public string GetDriverSurname(int driverID)
        {
            var query = (from row in _db.Driver
                         where row.ID == driverID
                         select row.LName).ToArray();
            return query[0];
        }

        public List<string> GetDriverNumbers(int driverID)
        {
            var query = (from row in _db.Contact
                         where row.DriverID == driverID
                         select row.ContactNumber).ToList<string>();
            return query;
        }

        public string GetConsignmentName(int consID)
        {
            var query = (from row in _db.Consignment
                         where row.ID == consID
                         select row.Name).ToArray();
            return query[0];
        }

        public string GetAfterCrashInfo(int consID)
        {
            var query = (from row in _db.Consignment
                         where row.ID == consID
                         select row.After_crash).ToArray();
            return query[0];
        }

        public int GetDangerDegree(int consID)
        {
            var query = (from row in _db.Consignment
                         where row.ID == consID
                         select row.Danger_degree).ToArray();
            return query[0];
        }

        public List<DateTime> GetLocationTime(int transID, int cityID)
        {
            var query = (from row in _db.TransitStady
                         where row.TransID == transID && row.CityID == cityID
                         select row.LocationTime).ToList<DateTime>();
            return query;
        }

        public string GetVisitsCities(int transID)
        {
            var query = (from row in _db.Route
                         where row.TransID == transID
                         select row.CitiesList).ToArray();
            return query[0];
        }

        public int GetCurrentLocation(int transID)
        {
            var was = (from transStady in _db.TransitStady
                       where transStady.TransID == transID && transStady.LocationTime <= DateTime.Now 
                       select transStady);
            var max = was.First();
            foreach (var trans in was)
            {
                if (max.LocationTime < trans.LocationTime)
                    max = trans;
            }
            return max.CityID;
        }

        public bool GetStatus(int transID)
        {
            var query = (from route in _db.Route
                         where route.TransID == transID
                         select route.Status).ToArray();
            return query[0];
        }

        public DateTime GetArrival(int transID)
        {
            var query = (from route in _db.Route
                         where route.TransID == transID
                         select route.ArrTime).ToArray();
            return (DateTime)query[0];
        }

        public void SubmitChanges()
        {
            _db.SubmitChanges();
        }
    }
}

