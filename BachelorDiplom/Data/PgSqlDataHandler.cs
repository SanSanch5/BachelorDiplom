using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using BachelorLibAPI.Properties;
using GMap.NET;
using Npgsql;
using NpgsqlTypes;

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
            lock(_lockObject)
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
        }

        public int GetTransitCarId(int transitId)
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd =
                    new NpgsqlCommand(
                        string.Format(
                            "SELECT car_id from t_transit where id = '{0}';",
                            transitId), _npgsqlConnection);
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
        }

        public void AddNewContact(int driverId, string contact)
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd =
                    new NpgsqlCommand(
                        string.Format("INSERT INTO t_telephone_number(number, driver_id) VALUES ('{0}', {1});", contact,
                            driverId), _npgsqlConnection);
                cmd.ExecuteNonQuery();

                _npgsqlConnection.Close();
            }
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
            lock (_lockObject)
            {
                _npgsqlConnection.Open();
                var customCulture = (CultureInfo) Thread.CurrentThread.CurrentCulture.Clone();
                customCulture.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = customCulture;

                var cmd =
                    new NpgsqlCommand(
                        string.Format(
                            "INSERT INTO t_transit(driver_id, car_id, start_time, start_position, end_position, consignment_name) " +
                            "VALUES ({0}, {1}, '{2}', '({3}, {4})', '({5}, {6})', '{7}');", driverId, carId,
                            startTime.ToString(Resources.DateTimeFormat), startPoint.Lat, startPoint.Lng, endPoint.Lat,
                            endPoint.Lng, consName), _npgsqlConnection);
                cmd.ExecuteNonQuery();

                var res = (int) (new NpgsqlCommand(
                    string.Format("select id from t_transit where driver_id = {0} and start_time = '{1}';", driverId,
                        startTime.ToString("yyyy-MM-dd HH:mm:ss")),
                    _npgsqlConnection)).ExecuteScalar();

                _npgsqlConnection.Close();
                return res;
            }
        }

        public void DelTransit(int transId)
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd =
                    new NpgsqlCommand(
                        string.Format("DELETE FROM t_transit WHERE id = {0};", transId),
                        _npgsqlConnection);
                cmd.ExecuteNonQuery();

                _npgsqlConnection.Close();
            }
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
            lock (_lockObject)
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
        }

        public string GetCarInformation(int carId)
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd =
                    new NpgsqlCommand(
                        string.Format(
                            "SELECT concat(mark_name, ' ', model_name) as full_name from t_car where id = {0};",
                            carId), _npgsqlConnection);
                string res;

                try
                {
                    res = (string) cmd.ExecuteScalar();
                    res = res ?? @"Не удалось определить автомобиль";
                }
                catch (Exception ex)
                {
                    res = @"Возникла ошибка " + ex.Message;
                }
                finally
                {
                    _npgsqlConnection.Close();
                }

                return res;
            }
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

        public List<int> GetTransitIDs()
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd = new NpgsqlCommand("SELECT id from t_transit;", _npgsqlConnection);
                var res = new List<int>();

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                        res.Add(int.Parse(dr[0].ToString()));
                }
                catch (Exception)
                {
                    res = new List<int>();
                }
                finally
                {
                    _npgsqlConnection.Close();
                }

                return res;
            }
        }

        public List<TransitInfo> GetTransits()
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd = new NpgsqlCommand("SELECT id from t_transit;", _npgsqlConnection);
                var res = new List<TransitInfo>();

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                    }
                }
                catch (Exception)
                {
                    res = new List<TransitInfo>();
                }
                finally
                {
                    _npgsqlConnection.Close();
                }

                return res;
            }
        }

        public IEnumerable<int> GetTransitIDs(int driverId)
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

        public string GetGrzByCarId(int carId)
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd =
                    new NpgsqlCommand(
                        string.Format(
                            "SELECT t_grz.grz from t_car join t_grz on (t_car.grz_id = t_grz.id) where t_car.id = {0};",
                            carId), _npgsqlConnection);
                string res;

                try
                {
                    res = (string) cmd.ExecuteScalar();
                    res = res ?? @"Не удалось определить автомобиль";
                }
                catch (Exception ex)
                {
                    res = @"Возникла ошибка " + ex.Message;
                }
                finally
                {
                    _npgsqlConnection.Close();
                }

                return res;
            }
        }

        public List<string> GetGrzList()
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd = new NpgsqlCommand("SELECT grz from t_grz;", _npgsqlConnection);
                var res = new List<string>();

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                        res.Add(dr[0].ToString());
                }
                catch (Exception)
                {
                    res = new List<string>();
                }
                finally
                {
                    _npgsqlConnection.Close();
                }

                return res;
            }
        }

        public int GetDriverId(int transitId)
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd =
                    new NpgsqlCommand(
                        string.Format(
                            "SELECT driver_id from t_transit where id = '{0}';",
                            transitId), _npgsqlConnection);
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
        }

        public string GetConsignmentName(int transId)
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd =
                    new NpgsqlCommand(string.Format("SELECT consignment_name FROM t_transit where id = {0};", transId),
                        _npgsqlConnection);
                string res;

                try
                {
                    res = (string) cmd.ExecuteScalar();
                    res = res ?? @"Для данной перевозки не задан груз";
                }
                catch (Exception ex)
                {
                    res = @"Возникла ошибка " + ex.Message;
                }
                finally
                {
                    _npgsqlConnection.Close();
                }

                return res;
            }
        }

        public string GetDriverName(int driverId)
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd = new NpgsqlCommand(string.Format("SELECT name FROM t_driver where id = {0};", driverId),
                    _npgsqlConnection);
                string res;

                try
                {
                    res = (string) cmd.ExecuteScalar();
                    res = res ?? @"Водитель не определён";
                }
                catch (Exception ex)
                {
                    res = @"Возникла ошибка " + ex.Message;
                }
                finally
                {
                    _npgsqlConnection.Close();
                }

                return res;
            }
        }

        public string GetDriverSurname(int driverId)
        {
            return "";
        }

        public List<string> GetDriverNumbers(int driverId)
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd =
                    new NpgsqlCommand(
                        string.Format("SELECT number from t_telephone_number where driver_id = {0};", driverId),
                        _npgsqlConnection);
                var res = new List<string>();

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                        res.Add(dr[0].ToString());
                }
                catch (Exception)
                {
                    res = new List<string>();
                }
                finally
                {
                    _npgsqlConnection.Close();
                }

                return res;
            }
        }

        public List<DateTime> GetLocationTime(int transId, PointLatLng pnt)
        {
            return new List<DateTime>();
        }

        public PointLatLng GetCurrentLocation(int transId)
        {
            return new PointLatLng();
        }

        public Tuple<PointLatLng, PointLatLng> GetStartAndEndPoints(int transitId)
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd =
                    new NpgsqlCommand(
                        string.Format("SELECT start_position, end_position from t_transit where id = {0};", transitId),
                        _npgsqlConnection);
                Tuple<PointLatLng, PointLatLng> res = null;
                try
                {
                    var dr = cmd.ExecuteReader();
                    dr.Read();
                    var start = (NpgsqlPoint) dr[0];
                    var end = (NpgsqlPoint) dr[1];
                    res = new Tuple<PointLatLng, PointLatLng>(new PointLatLng(start.X, start.Y),
                        new PointLatLng(end.X, end.Y));
                }
                catch (Exception)
                {
                    // ignored
                }
                finally
                {
                    _npgsqlConnection.Close();
                }

                return res;
            }
        }

        public void SubmitChanges() 
        {

        }

        private readonly object _lockObject = new object();
    }
}
