using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

namespace BachelorLibAPI.Data
{
    public sealed class PgSqlDataHandler : IDataHandler
    {
        private static readonly Lazy<PgSqlDataHandler> lazy =
        new Lazy<PgSqlDataHandler>(() => new PgSqlDataHandler());
    
        public static PgSqlDataHandler Instance { get { return lazy.Value; } }

        private PgSqlDataHandler()
        {
        }

        public void AddNewContact(int driverID, string contact)
        {

        }

        public void DelContacts(int driverID)
        {

        }

        public int AddNewDriver(string lName, string name, string mName)
        {
            return 0;
        }

        public void DelDriver(int driverID)
        {

        }

        public int AddNewTransit(int driverID, int consignmentID)
        {
            return 0;
        }

        public void DelTransit(int transID)
        {

        }

        public int GetTableLength<T>()
        {
            return 0;
        }

        public bool HasPhoneNumber(string contact)
        {
            return false;
        }

        public int[] FindDrivers(string lName, string name, string mName)
        {
            return new int[]{0};
        }

        public int DriverWithPhoneNumber(string contact)
        {
            return 0;
        }

        public string GetDriversFullName(int driverID)
        {
            return "";
        }

        public List<string> GetDriversFullNames()
        {
            return new List<string>();
        }

        public int GetTransitID(int driverID, DateTime start)
        {
            return 0;
        }

        public List<int> GetTransitIDs(DateTime start, DateTime until, int placeID)
        {
            return new List<int>();
        }

        public List<int> GetTransitIDs(int driverID)
        {
            return new List<int>();
        }

        public List<int> TransitsBefore(DateTime time)
        {
            return new List<int>();
        }

        public List<string> GetNumbers()
        {
            return new List<string>();
        }

        public int GetDriverID(int transID)
        {
            return 0;
        }

        public string GetConsignmentName(int transID)
        {
            return "";
        }

        public string GetDriverName(int driverID)
        {
            return "";
        }

        public string GetDriverSurname(int driverID)
        {
            return "";
        }

        public List<string> GetDriverNumbers(int driverID)
        {
            return new List<string>();
        }

        public List<DateTime> GetLocationTime(int transID, GMap.NET.PointLatLng pnt)
        {
            return new List<DateTime>();
        }

        public GMap.NET.PointLatLng GetCurrentLocation(int transID)
        {
            return new GMap.NET.PointLatLng();
        }

        public void SubmitChanges() 
        {

        }
    }
}
