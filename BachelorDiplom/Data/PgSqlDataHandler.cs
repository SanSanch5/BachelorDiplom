using System;
using System.Collections.Generic;
using BachelorLibAPI.Properties;
using GMap.NET;
using Npgsql;

namespace BachelorLibAPI.Data
{
    public sealed class PgSqlDataHandler : IDataHandler
    {
        private static readonly Lazy<PgSqlDataHandler> Lazy =
        new Lazy<PgSqlDataHandler>(() => new PgSqlDataHandler());
    
        public static PgSqlDataHandler Instance { get { return Lazy.Value; } }

        private NpgsqlConnection _npgsqlConnection;

        private PgSqlDataHandler()
        {
             _npgsqlConnection = new NpgsqlConnection(Settings.Default.CarsTrackingDBConnectionString);
        }

        public int GetCarIdByGRZ(string grz)
        {
            return 0;
        }
        public string GetCarMarkModel(string grz)
        {
            return "";
        }

        public void AddNewContact(int driverId, string contact)
        {

        }

        public void DelContacts(int driverId)
        {

        }

        public int AddNewDriver(string lName, string name, string mName)
        {
            return 0;
        }

        public void DelDriver(int driverId)
        {

        }

        public int AddNewTransit(int driverId, int consignmentId)
        {
            return 0;
        }

        public void DelTransit(int transId)
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

        public string GetDriversFullName(int driverId)
        {
            return "";
        }

        public List<string> GetDriversFullNames()
        {
            return new List<string>();
        }

        public int GetTransitId(int driverId, DateTime start)
        {
            return 0;
        }

        public List<int> GetTransitIDs(DateTime start, DateTime until, int placeId)
        {
            return new List<int>();
        }

        public List<int> GetTransitIDs(int driverId)
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

        public int GetDriverId(int transId)
        {
            return 0;
        }

        public string GetConsignmentName(int transId)
        {
            return "";
        }

        public string GetDriverName(int driverId)
        {
            return "";
        }

        public string GetDriverSurname(int driverId)
        {
            return "";
        }

        public List<string> GetDriverNumbers(int driverId)
        {
            return new List<string>();
        }

        public List<DateTime> GetLocationTime(int transId, PointLatLng pnt)
        {
            return new List<DateTime>();
        }

        public PointLatLng GetCurrentLocation(int transId)
        {
            return new PointLatLng();
        }

        public void SubmitChanges() 
        {

        }
    }
}
