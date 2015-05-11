using System;
using System.Collections.Generic;
using System.Globalization;
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

        private readonly NpgsqlConnection _npgsqlConnection;

        private PgSqlDataHandler()
        {
             _npgsqlConnection = new NpgsqlConnection(Resources.PgConnectionString);
        }

        public int GetCarIdByGRZ(string grz)
        {
            _npgsqlConnection.Open();

            var cmd =
                new NpgsqlCommand(
                    string.Format(
                        "SELECT t_car.id from t_car join t_grz on (t_car.grz_id = t_grz.id) where t_grz.grz = '{0}';",
                        grz), _npgsqlConnection);
            int res;

            try
            {
                res = (int)cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                res = -1;
            }
            finally
            {
                _npgsqlConnection.Close();
            }

            return res;
        }
        public string GetCarMarkModel(string grz)
        {
            return "";
        }

        public void AddNewContact(int driverId, string contact)
        {
            _npgsqlConnection.Open();

            var cmd =
                new NpgsqlCommand(
                    string.Format("INSERT INTO t_telephone_number(number, driver_id) VALUES ('{0}', {1});", contact,
                        driverId), _npgsqlConnection);
            cmd.ExecuteNonQuery();

            _npgsqlConnection.Close();
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

        public int AddNewTransit(int driverId, int carId, string consName, DateTime startTime, PointLatLng startPoint, PointLatLng endPoint)
        {
            _npgsqlConnection.Open();
            var customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            var cmd =
                new NpgsqlCommand(
                    string.Format(
                        "INSERT INTO t_transit(dirver_id, car_id, start_time, start_position, end_position, consignment_name) " +
                        "VALUES ({0}, {1}, '{2}', '({3}, {4})', '({5}, {6})', '{7}');", driverId, carId,
                        startTime.ToString("yyyy-MM-dd HH:mm:ss"), startPoint.Lat, startPoint.Lng, endPoint.Lat,
                        endPoint.Lng, consName), _npgsqlConnection);
            cmd.ExecuteNonQuery();

            var res = (int)(new NpgsqlCommand(
                    string.Format("select id from t_transit where dirver_id = {0} and start_time = '{1}';", driverId,
                        startTime.ToString("yyyy-MM-dd HH:mm:ss")),
                    _npgsqlConnection)).ExecuteScalar();

            _npgsqlConnection.Close();
            return res;
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
            return new[]{0};
        }

        public int DriverWithPhoneNumber(string contact)
        {
            _npgsqlConnection.Open();

            var cmd =
                new NpgsqlCommand(
                    string.Format("select driver_id from t_telephone_number where number = '{0}';", contact),
                    _npgsqlConnection);
            int res;

            try
            {
                res = (int) cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                res = -1;
            }
            finally
            {
                _npgsqlConnection.Close();
            }

            return res;
        }

        public string GetCarInformation(int carId)
        {
            _npgsqlConnection.Open();

            var cmd =
                new NpgsqlCommand(
                    string.Format("SELECT concat(mark_name, ' ', model_name) as full_name from t_car where id = {0};",
                        carId), _npgsqlConnection);
            string res;

            try
            {
                res = (string) cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                res = "";
            }
            finally
            {
                _npgsqlConnection.Close();
            }

            return res;
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
            _npgsqlConnection.Open();

            var cmd = new NpgsqlCommand(string.Format("SELECT name FROM t_driver where id = {0};", driverId), _npgsqlConnection);
            string res;

            try
            {
                res = (string)cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                res = "";
            }
            finally
            {
                _npgsqlConnection.Close();
            }

            return res;
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
