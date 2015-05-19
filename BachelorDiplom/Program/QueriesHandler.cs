using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BachelorLibAPI.Algorithms;
using BachelorLibAPI.Data;
using BachelorLibAPI.Forms;
using BachelorLibAPI.Map;
using BachelorLibAPI.Properties;
using GMap.NET;

//using System.Transactions;

namespace BachelorLibAPI.Program
{
    /// <summary>
    /// Исполняет прецеденты, используя функционал, предоставляемый интерфейсами IDataHandler и IMap
    /// </summary>
    public class QueriesHandler
    {
        private ProgressBar _analyseProgress;

        private const int TimeCorrection = 30;
        private const int KmCorrection = 5;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dh">Реализация интерфейса IDataHandler</param>
        /// <param name="m">Реализация интерфейса IMap</param>
        public QueriesHandler(IDataHandler dh, IMap m)
        {
            DataHandler = dh;
            Map = m;
        }

        /// <summary>
        /// Свойство устанавливает, какую полосу загрузки использовать при анализе
        /// </summary>
        public ProgressBar AnalyseProgress
        {
            set { _analyseProgress = value; }
        }

        /// <summary>
        /// Свойство устанавливает новую реализацию интерфейса IDataHandler и возвращает текущуюю
        /// </summary>
        public IDataHandler DataHandler { get; set; }

        private IMap _map;

        /// <summary>
        /// Свойство устанавливает новую реализацию интерфейса IMap и возвращает текущуюю
        /// </summary>
        public IMap Map
        {
            get { return _map; }
            set 
            { 
                _map = value;
                _map.MarkerRemove += DeleteAdvanced;
            } }

        public static List<string> GetConsignmentsNames()
        {
            return Ahov.Coefficient.Select(x => x.Key).ToList();
        }

        public List<string> GetGrzList()
        {
            return DataHandler.GetGrzList();
        }

        public string GetCarInfo(string grz)
        {
            return DataHandler.GetCarInformation(DataHandler.GetCarIdByGRZ(grz));
        }

        private void DeleteAdvanced(object o, MarkerRemoveEventArgs e)
        {
            if(e.MarkerType == MarkerType.Transit)
            {
                DataHandler.DeleteTransit(e.Id);
                if (File.Exists(TempFilesDir + string.Format("\\Transits\\{0}", e.Id))) 
                    File.Delete(TempFilesDir + string.Format("\\Transits\\{0}", e.Id));

                MessageBox.Show(@"Перевозка удалена", @"Информация");
            }
            else if (e.MarkerType == MarkerType.Staff)
            {
                DataHandler.DeleteMchsStaff(e.Id);

                MessageBox.Show(@"Пункт удалён", @"Информация");
            }
        }

        /// <summary>
        /// Удалить все стадии перевозок, зарегистрированные ранее заданного времени
        /// </summary>
        public static void DeleteTransitStadiesOlderThenYesterday()
        {
            Task.Run(() =>
            {
                var
                    di = new DirectoryInfo(TempFilesDir);
                foreach (var file in di.GetFiles().Where(file =>
                {
                    // ReSharper disable once InconsistentNaming
                    var time_date = file.Name.Split('_');
                    var time = time_date[0].Split('-');
                    var date = time_date[1].Split('-');
                    var fileDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]),
                        int.Parse(time[0]), int.Parse(time[1]), 0);
                    return fileDate < DateTime.Now.AddDays(-1);
                }))
                {
                    File.Delete(TempFilesDir + file.Name);
                }
            });
        }

        public void PutMchsPointsFromDbToMap()
        {
            var semires = DataHandler.GetMchsPointsInfo();
            foreach (var t in semires)
            {
                var pointInfo = t;
                pointInfo.AntiSubstances = DataHandler.GetStaffAntiSubstances(t.Id);
                _mchsPoints.Add(pointInfo);
                Map.AddMchsMarker(pointInfo);
            }
        }

        public TransitInfo GetTransitInfo(int id)
        {
            var carId = DataHandler.GetTransitCarId(id);
            var driverId = DataHandler.GetDriverId(id);
            var startEnd = DataHandler.GetStartAndEndPoints(id);

            var car =
                carId == -1 ? @"Не удалось определить автомобиль" : DataHandler.GetCarInformation(carId);

            int currentPosition;
            PointLatLng currentPoint;
            int count;
            GetTransitInfoFromFile(id, out currentPosition, out currentPoint, out count);
            
            return new TransitInfo
            {
                Car = car,
                Consignment = DataHandler.GetConsignmentName(id),
                ConsignmentCapacity = DataHandler.GetConsignmentCapacity(id),
                Driver = DataHandler.GetDriverName(driverId),
                DriverNumber = DataHandler.GetDriverNumber(driverId),
                From = new FullPointDescription
                {
                    Address = Map.GetPlacemark(startEnd.Item1),
                    Position = startEnd.Item1
                },
                To = new FullPointDescription
                {
                    Address = Map.GetPlacemark(startEnd.Item2),
                    Position = startEnd.Item2
                },
                Grz = DataHandler.GetGrzByCarId(carId),
                Id = id,
                CurrentPlace = new FullPointDescription
                {
                    Address = Map.GetPlacemark(currentPoint),
                    Position = currentPoint
                },
                StadiesCount = count,
                IsFinshed = currentPosition == count - 1,
                IsInAccident = false
            };
        }

        /// <summary>
        /// Получает хранящиеся в файлах промежуточные данные о перевозках
        /// </summary>
        /// <param name="id">Идентификатор перевозки</param>
        /// <param name="currentPos">Позиция актуальной записи в файле</param>
        /// <param name="currentLatLng">Найденная актуальная точка на карте</param>
        /// <param name="counter">Количество элементов в файле</param>
        private static void GetTransitInfoFromFile(int id, out int currentPos, out PointLatLng currentLatLng, out int counter)
        {
            currentPos = -1;
            currentLatLng = new PointLatLng(0, 0);
            counter = 0;
            var fs = new FileStream(TempFilesDir + string.Format("\\Transits\\{0}", id), FileMode.Open);
            using (var sr = new StreamReader(fs))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    ++counter;
                    var splitted = line.Split(';').ToList();
                    var tm = DateTime.Parse(splitted[0]);
                    if (tm > DateTime.Now) continue;
                    ++currentPos;
                    currentLatLng.Lat = double.Parse(splitted[1]);
                    currentLatLng.Lng = double.Parse(splitted[2]);
                }
            }
        }

        /// <summary>
        /// При начальной загрузке программы в фоновом режиме (пользователь может работать в это время)
        /// Запрашивает данные из базы о зарегистрированных перевозках
        /// и отдаёт в нужном карте формате для расстановки маркеров.
        /// </summary>
        public void PutTransitsFromDbToMap()
        {
            Task.Run(() =>
            {
                var tasks = new List<Task>();
                var transits = DataHandler.GetTransitIDs();
                var pbForm = new ProgressBarForm(@"Отображение перевозок на карте...", transits.Count*1000);
                foreach (var transitId in transits)
                {
                    pbForm.Progress(1000);
                    int currentPosition;
                    PointLatLng currentPoint;
                    int counter;
                    GetTransitInfoFromFile(transitId, out currentPosition, out currentPoint, out counter);

                    if (currentPosition == -1) continue;

                    var id = transitId;
                    tasks.Add(Task.Run(() => Map.AddTransitMarker(GetTransitInfo(id))));
                }
                Task.WaitAll(tasks.Where(x => x != null).ToArray());
                pbForm.Complete();
                Thread.Sleep(1000);
                pbForm.Close();
            });
        }

        private const string TempFilesDir = @"..\..\TempStadiesFiles\";

        /// <summary>
        /// Добавление нового путевого листа
        /// Не производится добавление водителя! 
        /// Если водитель ранее не был зарегистрирован в базе, необходимо зарегистрировать его,
        /// воспользовавшись формой добавления водителя.
        /// Удаляются все стадии предыдущих перевозок, зарегистрированных на этого водителя, 
        /// но сами перевозки не отмечаются, как завершённые. Для этого необходимо воспользоваться
        /// специально предназначенной для этого формой.
        /// </summary>
        /// <param name="driverNum"></param>
        /// <param name="grz"></param>
        /// <param name="consName"></param>
        /// <param name="consCapacity"></param>
        /// <param name="start"></param>
        /// <param name="driverName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void AddNewWaybill(string driverName, string driverNum, string grz, string consName, double consCapacity, DateTime start,
            string from, string to)
        {
            var driverId = DataHandler.GetDriverId(driverName, driverNum);

            if (driverId == -1)
                driverId = DataHandler.AddNewDriver(driverName, driverNum);

            var carId = DataHandler.GetCarIdByGRZ(grz);

            if (carId == -1)
                throw new Exception("Нет автомобиля с таким регистрационным знаком");

            Cursor.Current = Cursors.WaitCursor;
            var detailedRoute = Map.GetShortTrack();
            var transitId = DataHandler.AddNewTransit(driverId, carId, consName, consCapacity, start, detailedRoute.First().Key,
                detailedRoute.Last().Key);

            var currentPosition = -1;

            Task.Run(() =>
            {
                var pbForm = new ProgressBarForm(@"Запись временных файлов", detailedRoute.Count);

                var fsForTransit = new FileStream(TempFilesDir + string.Format("\\Transits\\{0}", transitId), FileMode.Create);
                using (var swForTransit = new StreamWriter(fsForTransit))
                {
                    foreach (var keyValuePair in detailedRoute)
                    {
                        pbForm.Progress();
                        var tm = start.AddMinutes(keyValuePair.Value);
                        var fs = new FileStream(TempFilesDir + tm.ToString(Resources.TimeFileFormat), FileMode.Append);
                        using (var sw = new StreamWriter(fs))
                        {
                            sw.WriteLine("{0};{1};{2}", transitId, keyValuePair.Key.Lat, keyValuePair.Key.Lng);
                        }
                        if (tm <= DateTime.Now) ++currentPosition;

                        swForTransit.WriteLine("{0};{1};{2}", tm.ToString(Resources.DateTimeFormat), keyValuePair.Key.Lat, keyValuePair.Key.Lng);
                    }
                }

                pbForm.Complete();
                pbForm.Close();
                if (currentPosition == -1) return;

                Map.AddTransitMarker(new TransitInfo
                {
                    Car = DataHandler.GetCarInformation(carId),
                    Consignment = consName,
                    ConsignmentCapacity = consCapacity,
                    Driver = driverName,
                    DriverNumber = driverNum,
                    From = new FullPointDescription
                    {
                        Address = Map.GetPlacemark(detailedRoute.First().Key),
                        Position = detailedRoute.First().Key
                    },
                    To = new FullPointDescription
                    {
                        Address = Map.GetPlacemark(detailedRoute.Last().Key),
                        Position = detailedRoute.Last().Key
                    },
                    Grz = grz,
                    Id = transitId,
                    CurrentPlace = new FullPointDescription
                    {
                        Address = Map.GetPlacemark(detailedRoute[currentPosition].Key),
                        Position = detailedRoute[currentPosition].Key
                    },
                    IsFinshed = currentPosition == detailedRoute.Count - 1,
                    IsInAccident = false
                });
            });
            Cursor.Current = Cursors.Default;
        }
        /// <summary>
        /// Иногда возвращается слишком длинный адрес, который потом невозможно преобразовать обратно в точку.
        /// Метод необходим, чтобы провайдел искал на карте то, что сам же вернул ранее
        /// </summary>
        /// <param name="adress"></param>
        /// <returns></returns>
        private static string CutAdress(string adress)
        {
            var elems = adress.Split(',').ToList();
            if (elems.Count > 6)
                adress = string.Format("{0},{1},{2},{3},{4}", elems[1], elems[2], elems[3], elems[4], elems[5]);
            return adress;
        }

        public bool CheckAdress(string adress)
        {
            return Map.CheckAdress(CutAdress(adress));
        }
        public string GetCorrectAdress(string adress)
        {
            return Map.GetCorrectAdress(adress);
        }

        public void SetStartPoint(string adress)
        {
            Map.SetStartPoint(adress);
        }

        public string GetStartPoint()
        {
            return Map.GetStartPoint();
        }

        public void SetMiddlePoint(string adress)
        {
            Map.SetMiddlePoint(adress);
        }

        public bool HasMiddlePoints()
        {
            return Map.HasMidPoints();
        }
        public void SetEndPoint(string adress)
        {
            Map.SetEndPoint(adress);
        }

        public bool CheckBeforeAdding()
        {
            return Map.CheckBeforeAdding();
        }

        public string GetEndPoint()
        {
            return Map.GetEndPoint();
        }

        public void ConstructShortTrack()
        {
            Map.ConstructShortTrack();
        }

        private void SetProgressParameters(IEnumerable<int> citiesIDs, DateTime since, DateTime until)
        {
            _analyseProgress.Visible = true;
            _analyseProgress.Minimum = 0;
            _analyseProgress.Value = 0;
            
            var max = citiesIDs.Select(cityId => DataHandler.GetTransitIDs(since, until, cityId)).Select(transIDs => transIDs.Count()).Sum();
            _analyseProgress.Maximum = max;
        }

        private int? GetMostProbablyTransit(DateTime targetTime, FullPointDescription place)
        {
            _probableCrashes[place.Position] = new HashSet<int>();

            int? mostProbably = null;
            var stady = int.MaxValue;

            var di = new DirectoryInfo(TempFilesDir);
            foreach (var file in di.GetFiles().Where(file =>
            {
                // ReSharper disable once InconsistentNaming
                var time_date = file.Name.Split('_');
                var time = time_date[0].Split('-');
                var date = time_date[1].Split('-');
                var fileDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]),
                    int.Parse(time[0]), int.Parse(time[1]), 0);
                return fileDate < targetTime.AddMinutes(TimeCorrection / 2) && fileDate > targetTime.AddMinutes(-TimeCorrection / 2);
            }))
            {
                using (var sr = new StreamReader(new FileStream(TempFilesDir + file.Name, FileMode.Open)))
                {
                    string line;
                    var pnt = new PointLatLng();
                    var localStady = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        ++localStady;
                        var splitted = line.Split(';').ToList();
                        pnt.Lat = double.Parse(splitted[1]);
                        pnt.Lng = double.Parse(splitted[2]);
                        var id = int.Parse(splitted[0]);
                        if (!(LatLongWorker.DistanceFromLatLonInKm(place.Position, pnt) < KmCorrection)) continue;

                        _probableCrashes[place.Position].Add(id);
                        if (!mostProbably.HasValue || stady > localStady)
                        {
                            stady = localStady;
                            mostProbably = id;
                        }
                        break;
                    }
                }
            }
            return mostProbably;
        }

        /// <summary>
        /// Анализ опасности.
        /// Производится выборка по по промежутку времени по каждой перевозке.
        /// Точка ищется в радиусе километра, поправка времени - TimeCorrection
        /// </summary>
        /// <param name="targetTime">Начало временного интервала</param>
        /// <param name="place">Место аварии</param>
        public void AnalyseDanger(DateTime targetTime, FullPointDescription place)
        {
            var mostProbably = GetMostProbablyTransit(targetTime, place);
            if (!mostProbably.HasValue)
            {
                MessageBox.Show(string.Format("В радиусе {0} км. от заданного места " +
                                "с {1}-ти минутной поправкой заданного времени " +
                                "перевозок опасных грузов не было обнаружено", KmCorrection, TimeCorrection), @"Информация");
                return;
            }

            var transInfo = GetTransitInfo(mostProbably.Value);

            IChemicalEnvironmentCalculation calculation = new Rd90(transInfo.Consignment, transInfo.ConsignmentCapacity);
            var area = calculation.InfectionArea;
            var antiSubstanceCount = calculation.AntiSubstanceCount;
            var crashInfo = new CrashInfo
            {
                Area = area,
                Center = place,
                Consignment = transInfo.Consignment,
                ConsignmentCapacity = transInfo.ConsignmentCapacity,
                StartTime = targetTime,
                WindDirection = 45
            };
            
            Map.SetDanger(mostProbably.Value, place.Position);
            Map.DrawDangerRegion(crashInfo);

            var possibleMchsPoints = _mchsPoints;
            if (antiSubstanceCount.Key != "")
                possibleMchsPoints =
                    _mchsPoints.Where(x => x.AntiSubstances.Select(y => y.Key).Contains(antiSubstanceCount.Key))
                        .ToList();
            var mchsPointsWithDistances =
                possibleMchsPoints.Select(
                    x =>
                        new KeyValuePair<MchsPointInfo, double>(x,
                            60 / Settings.Default.AvegareVelocity *
                            LatLongWorker.DistanceFromLatLonInKm(x.Place.Position, place.Position))).ToList();
            mchsPointsWithDistances.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            var have = 0;
            for (var i = 0; i < mchsPointsWithDistances.Count && have < antiSubstanceCount.Value; ++i)
            {
                var pnt = mchsPointsWithDistances[i];
                //if(pnt.Key.AntiSubstances.Where(x => x.Key == antiSubstanceCount.Key))
            }
        }

        public List<string> GetNamesByNumber(string contact)
        {
            return DataHandler.GetNamesByNumber(contact);
        }

        /// <summary>
        /// Получить полный список ФИО всех водителей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetDriversFullNames()
        {
            return DataHandler.GetDriversFullNames();
        }

        /// <summary>
        /// получить полный список всех имеющихся в базе контактных номеров
        /// </summary>
        /// <returns></returns>
        public List<string> GetNumbers()
        {
            return DataHandler.GetNumbers();
        }

        /// <summary>
        /// Узнать, зарегистрирован ли на кого-либо номер
        /// </summary>
        /// <param name="num">Проверяемый номер</param>
        /// <returns></returns>
        public bool HasPhoneNumber(string num)
        {
            return DataHandler.HasPhoneNumber(num);
        }

        public void SetCurrentPointOfView(double x, double y)
        {
            Map.SetCurrentViewPoint(new PointLatLng(x, y));
        }

        private readonly List<TransitInfo> _actualTransits = new List<TransitInfo>();
        private readonly List<MchsPointInfo> _mchsPoints = new List<MchsPointInfo>();
        private readonly Dictionary<PointLatLng, HashSet<int>> _probableCrashes = new Dictionary<PointLatLng, HashSet<int>>(); 
    }
}
