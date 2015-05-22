using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using BachelorLibAPI.Program;
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

        public void DelContacts(int driverId)
        {

        }

        public int AddNewDriver(string name, string number, string middleName = "", string lastName = "")
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var query = string.Format(
                    "INSERT INTO t_driver(name, middle_name, last_name, number) " +
                    "VALUES ('{0}', '{1}', '{2}', '{3}');", name, middleName, lastName, number);
                var cmd = new NpgsqlCommand(query, _npgsqlConnection);
                cmd.ExecuteNonQuery();

                _npgsqlConnection.Close();

                return GetDriverId(name, number);
            }
        }

        public void DelDriver(int driverId)
        {

        }

        public int AddNewTransit(int driverId, int carId, string consName, double consCapacity, DateTime startTime, PointLatLng startPoint, PointLatLng endPoint)
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
                            "INSERT INTO t_transit(driver_id, car_id, start_time, start_position, end_position, consignment_name, consignment_capacity) " +
                            "VALUES ({0}, {1}, '{2}', '({3}, {4})', '({5}, {6})', '{7}', {8});", driverId, carId,
                            startTime.ToString(Resources.DateTimeFormat), startPoint.Lat, startPoint.Lng, endPoint.Lat,
                            endPoint.Lng, consName, consCapacity), _npgsqlConnection);
                cmd.ExecuteNonQuery();

                var res = (int) (new NpgsqlCommand(
                    string.Format("select id from t_transit where driver_id = {0} and start_time = '{1}';", driverId,
                        startTime.ToString("yyyy-MM-dd HH:mm:ss")),
                    _npgsqlConnection)).ExecuteScalar();

                customCulture.NumberFormat.NumberDecimalSeparator = ",";
                Thread.CurrentThread.CurrentCulture = customCulture;

                _npgsqlConnection.Close();
                return res;
            }
        }

        public void DeleteTransit(int transId)
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

        public void DeleteMchsStaff(int staffId)
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd =
                    new NpgsqlCommand(
                        string.Format("DELETE FROM mchs.staff WHERE id = {0};", staffId),
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

        public double GetConsignmentCapacity(int transitId)
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd =
                    new NpgsqlCommand(string.Format("SELECT consignment_capacity FROM t_transit where id = {0};", transitId),
                        _npgsqlConnection);
                double res = -1;

                try
                {
                    res = (double)cmd.ExecuteScalar();
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

        public string GetDriverNumber(int driverId)
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd =
                    new NpgsqlCommand(
                        string.Format("SELECT number from t_driver where id = {0};", driverId),
                        _npgsqlConnection);
                string res;

                try
                {
                    res = (string)cmd.ExecuteScalar();
                    res = res ?? @"Водитель не найден";
                }
                catch (Exception ex)
                {
                    res = @"Водитель не найден";
                    Debug.WriteLine(ex.Message);
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

        public int GetDriverId(string name, string number)
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var res = -1;
                try
                {
                    res = (int)(new NpgsqlCommand(
                    string.Format("select id from t_driver where name = '{0}' and number = '{1}';", name,
                        number),
                    _npgsqlConnection)).ExecuteScalar();
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

        public List<string> GetNamesByNumber(string contact)
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd =
                    new NpgsqlCommand(
                        string.Format("select name from t_driver where number = '{0}';", contact),
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

        public List<string> GetNumbersByName(string driverName)
        {
            lock (_lockObject)
            {
                _npgsqlConnection.Open();

                var cmd =
                    new NpgsqlCommand(
                        string.Format("select number from t_driver where name = '{0}';", driverName),
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

        public Dictionary<string, double> GetStaffAntiSubstances(int staffId)
        {
            lock(_lockObject)
            {
                _npgsqlConnection.Open();

                var antiSubstances = new Dictionary<string, double>();

                try
                {
                    var cmd = new NpgsqlCommand(string.Format("select * from mchs.f_as_for_staff({0});", staffId),
                        _npgsqlConnection);
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                        antiSubstances[dr[0].ToString()] = (double) dr[1];
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    antiSubstances = new Dictionary<string, double>();
                }
                finally
                {
                    _npgsqlConnection.Close();
                }

                return antiSubstances;
            }
        }

        public List<MchsPointInfo> GetMchsPointsInfo()
        {
            lock(_lockObject)
            {
                _npgsqlConnection.Open();
                var res = new List<MchsPointInfo>();

                try
                {
                    var cmd = new NpgsqlCommand("select * from mchs.f_staff_info_for_client();", _npgsqlConnection);
                    var dr = cmd.ExecuteReader();
                    
                    while (dr.Read())
                    {
                        var pnt = (NpgsqlPoint) dr[1];
                        res.Add(new MchsPointInfo
                        {
                            Id = (int)dr[0],
                            Place = new FullPointDescription{Position = new PointLatLng(pnt.X, pnt.Y)},
                            CanSuggest = dr[2].ToString() == "" ? 0 : double.Parse(dr[2].ToString()),
                            PeopleReady = dr[3].ToString() == "" ? 0 : int.Parse(dr[3].ToString()),
                            PeopleCount = (int)dr[4],
                            SuperCarCount = dr[5].ToString() == "" ? 0 : int.Parse(dr[5].ToString())
                        });
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    res = new List<MchsPointInfo>();
                }
                finally
                {
                    _npgsqlConnection.Close();
                }

                return res;
            }
        }

        private readonly object _lockObject = new object();
    }
}
