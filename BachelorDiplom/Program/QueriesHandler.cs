﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
        private const int TimeCorrection = 30;
        private const int KmCorrection = 5;
        private const int TimeOfCleaning = 24; // общее время на устранение последствий
        private int _cleaningTime; // сколько времени будет у МЧС на устранение с учётом времени аварии и текущего времени

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
        /// Свойство устанавливает новую реализацию интерфейса IDataHandler и возвращает текущуюю
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        public IDataHandler DataHandler { get; set; }

        private IMap _map;

        /// <summary>
        /// Свойство устанавливает новую реализацию интерфейса IMap и возвращает текущую
        /// </summary>
        public IMap Map
        {
            get { return _map; }
            // ReSharper disable once MemberCanBePrivate.Global
            set 
            { 
                _map = value;
                _map.MarkerRemove += DeleteAdvanced;
            } }

        /// <summary>
        /// Метод позволяет получить полный перечень опасных химических грузов,
        /// поддерживаемых программой.
        /// </summary>
        public static List<string> GetConsignmentsNames()
        {
            return Ahov.Coefficient.Select(x => x.Key).ToList();
        }

        /// <summary>
        /// Метод позволяет получить полный список зарегистрированных в базе ГРЗ
        /// </summary>
        public List<string> GetGrzList()
        {
            return DataHandler.GetGrzList();
        }

        /// <summary>
        /// Метод позволяет получить информацию об автомобиле
        /// </summary>
        /// <param name="grz">Номер автомобиля</param>
        public string GetCarInfo(string grz)
        {
            return DataHandler.GetCarInformation(DataHandler.GetCarIdByGRZ(grz));
        }

        /// <summary>
        /// Обработчик событий, приходящих с карты
        /// </summary>
        /// <param name="o">Объект (карта)</param>
        /// <param name="e">Событие, хранит информацию о типе маркера и идентификатор удаляемой сущности</param>
        private void DeleteAdvanced(object o, MarkerRemoveEventArgs e)
        {
            switch (e.MarkerType)
            {
                case MarkerType.Transit:
                    DataHandler.DeleteTransit(e.Id);
                    if (File.Exists(TempFilesDir + string.Format("\\Transits\\{0}", e.Id))) 
                        File.Delete(TempFilesDir + string.Format("\\Transits\\{0}", e.Id));

                    MessageBox.Show(@"Перевозка удалена", @"Информация");
                    break;
                case MarkerType.Staff:
                    DataHandler.DeleteMchsStaff(e.Id);
                    MessageBox.Show(@"Пункт удалён", @"Информация");
                    break;
                case MarkerType.Crash:
                    DataHandler.DeleteCrash(e.Id);
                    Map.ClearMchsStaffs();
                    PutMchsPointsFromDbToMap();
                    break;
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

        /// <summary>
        /// Метод получает из базы информацию о пунктах реагирования МЧС,
        /// сохраняет данные об имеющемся количестве обезвреживающего вещества
        /// и ресурсах для устранения последствий и инициирует обработчику карты
        /// отметить доступные пункты маркерами на карте.
        /// </summary>
        public void PutMchsPointsFromDbToMap()
        {
            lock (_mchsPoints)
            {
                _availableForces = 0;
                _availableSubstances.Clear();
                _mchsPoints.Clear();
                var semires = DataHandler.GetMchsPointsInfo();
                foreach (var t in semires)
                {
                    var pointInfo = t;
                    if (!pointInfo.IsAvailable) continue;

                    var people = Math.Min(pointInfo.PeopleCount, pointInfo.PeopleReady);
                    var superCars = pointInfo.SuperCarCount;
                    _availableForces += superCars*Settings.Default.SpecialCarLabor + people*Settings.Default.ManLabor;

                    pointInfo.AntiSubstances = DataHandler.GetStaffAntiSubstances(t.Id);

                    foreach (var antiSubstance in pointInfo.AntiSubstances)
                    {
                        var key = antiSubstance.Key;
                        if (_availableSubstances.ContainsKey(key))
                            _availableSubstances[key] += antiSubstance.Value;
                        else
                            _availableSubstances[key] = antiSubstance.Value;
                    }

                    _mchsPoints.Add(pointInfo);
                    Map.AddMchsMarker(pointInfo);
                }
            }
        }

        /// <summary>
        /// Метод получает из базы информацию о перевозке
        /// </summary>
        /// <param name="id">Идентификатор перевозки</param>
        /// <returns>Информацию о перевозке</returns>
        private TransitInfo GetTransitInfo(int id)
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

            var isInAccident = DataHandler.IsInAccident(id);
            var res = isInAccident ? new TransitInfo{IsInAccident = true} : 
                new TransitInfo
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
            return res;
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

        private static void RemoveDeletedTransitsFiles(ICollection<int> transitIds)
        {
            foreach (
                var fileName in
                    new DirectoryInfo(TempFilesDir + @"\Transits").GetFiles()
                        .Where(file => !transitIds.Contains(int.Parse(file.Name)))
                        .Select(x => x.Name))
            {
                File.Delete(TempFilesDir + string.Format("\\Transits\\{0}", fileName));
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
                var transitTasks = new List<Task>();
                var crashTasks = new List<Task>();
                var transits = DataHandler.GetTransitIDs();
                Task.Run(() => RemoveDeletedTransitsFiles(transits));
                
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
                    var transitInfo = GetTransitInfo(id);
                    if (!transitInfo.IsInAccident)
                        transitTasks.Add(Task.Run(() => Map.AddTransitMarker(transitInfo)));
                    else
                    {
                        var crashInfo = DataHandler.GetCrashInfo(id);
                        if (crashInfo.UntilTime < DateTime.Now)
                            DataHandler.DeleteCrash(crashInfo.Id);
                        else
                        {
                            crashInfo.Center.Address = Map.GetPlacemark(crashInfo.Center.Position);
                            crashTasks.Add(Task.Run(() => Map.AddCrashMarker(crashInfo)));
                        }
                    }
                }
                Task.WaitAll(transitTasks.Where(x => x != null).ToArray());
                Task.WaitAll(crashTasks.Where(x => x != null).ToArray());
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
        public void AddNewWaybill(string driverName, string driverNum, string grz, string consName, double consCapacity, DateTime start)
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
                // ReSharper disable PossibleLossOfFraction
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
                        if (!(LatLongWorker.DistanceFromLatLonInKm(place.Position, pnt) < KmCorrection) || DataHandler.IsInAccident(id)) continue;

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
            _reportInfo.Add(new List<string>
            {
                "Возможные первозки, задействованные в аварии: ",
                _probableCrashes[place.Position].Aggregate("", (c, s) => c + s + "; ")
            });
            _reportInfo.Add(new List<string>
            {
                "Наиболее вероятная перевозка: ", mostProbably.HasValue ? mostProbably.Value.ToString() : ""
            });
            _reportInfo.Add(new List<string>());

            return mostProbably;
        }

        private void GetStaffsWithSubstance(PointLatLng crashPoint, KeyValuePair<string, double> antiSubstanceCount,
             out List<KeyValuePair<MchsPointInfo, double>> res)
        {
            var possibleMchsPoints =
                    _mchsPoints.Where(x => x.AntiSubstances.Select(y => y.Key).Contains(antiSubstanceCount.Key)
                                           && Math.Min(x.CanSuggest, x.AntiSubstances[antiSubstanceCount.Key]) > 0)
                        .ToList();

            res = GetPointsWithDistances(possibleMchsPoints, crashPoint);
            res.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
        }

        private List<KeyValuePair<MchsPointInfo, double>> GetPointsWithDistances(
            IReadOnlyList<MchsPointInfo> possibleMchsPoints, PointLatLng crashPoint)
        {
            var mchsPointsWithDistancesArray = new KeyValuePair<MchsPointInfo, double>[possibleMchsPoints.Count];
            var tasks =
                possibleMchsPoints.Select((t, i) => i)
                    .Select(
                        index =>
                            Task.Run(() =>
                            {
                                    // грубая оценка расстояния сразу откидывает все пункты, от которых ехать дольше 23-х часов
                                var distance = LatLongWorker.DistanceFromLatLonInKm(
                                    possibleMchsPoints[index].Place.Position,
                                    crashPoint) > (_cleaningTime - 1)*Settings.Default.MchsAverageVelocity
                                    ? -1
                                    : Map.GetDistanceBetween(possibleMchsPoints[index].Place.Position,
                                        crashPoint);
                                mchsPointsWithDistancesArray[index] = new KeyValuePair<MchsPointInfo, double>(possibleMchsPoints[index],
                                    distance);
                            }))
                    .ToList();
            Task.WaitAll(tasks.ToArray());
            return mchsPointsWithDistancesArray.ToList().Where(x => Math.Abs(x.Value - (-1)) > 1e-6).ToList();
        }

        private void GetStaffsWithResources(PointLatLng crashPoint,out List<KeyValuePair<MchsPointInfo, double>> res)
        {
            var possibleMchsPoints =
                    _mchsPoints.Where(x => Math.Min(x.PeopleCount, x.PeopleReady) + x.SuperCarCount > 0)
                        .ToList();

            res = GetPointsWithDistances(possibleMchsPoints, crashPoint);
            res.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
        }

        private List<int> CrashCounting(CrashInfo crashInfo, KeyValuePair<string, double> antiSubstanceCount, out DateTime until)
        {
            var progressBar = new ProgressBarForm(@"Расчёт устранения аварии...", 10000);
            progressBar.Progress(1000);

            _staffsWithSubstances = null;
            _staffsWithResources = null;
            var tarr = new Task[2];
            tarr[0] =
                Task.Run(
                    () => GetStaffsWithSubstance(crashInfo.Center.Position, antiSubstanceCount, out _staffsWithSubstances));
            tarr[1] =
                Task.Run(
                    () => GetStaffsWithResources(crashInfo.Center.Position, out _staffsWithResources));
            //Task.WaitAll(tarr);
            tarr[0].Wait();
            progressBar.Progress(1000);

            var people = 0;
            var superCars = 0;
            var mchsStaffsWithSubstance = new List<int>();
            var have = 0.0;
            var count = 0;
            // здесь считаю пункты с антивеществом - они едут в первую очередь
            while (count < _staffsWithSubstances.Count && have < antiSubstanceCount.Value)
            {
                var pnt = _staffsWithSubstances[count++];
                var having = pnt.Key.AntiSubstances.Where(x => x.Key == antiSubstanceCount.Key).ToList().First();
                have += Math.Min(having.Value, pnt.Key.CanSuggest);
                people += Math.Min(pnt.Key.PeopleCount, pnt.Key.PeopleReady);
                superCars += pnt.Key.SuperCarCount;
                mchsStaffsWithSubstance.Add(pnt.Key.Id);
            }
            tarr[1].Wait();
            progressBar.Progress(5000);

            _staffsWithSubstances =
                _staffsWithSubstances.Where(x => mchsStaffsWithSubstance.Contains(x.Key.Id)).ToList();
            _staffsWithResources = _staffsWithResources.Except(_staffsWithSubstances).ToList();
            var pointsWithDistances = _staffsWithSubstances.Concat(_staffsWithResources).ToList();
            pointsWithDistances.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            // необходимо искать минимальную сумму времени устранения и времени прибытия.
            // здесь считаю людей + суперкары
            count = 0;
            var minimum = _cleaningTime + 1.0;
            var minTravelTime = -1.0;
            var minCleaningDuration = -1;
            while (++count < _cleaningTime)
            {
                if (count > minimum) break;

                progressBar.Progress(3000 / TimeOfCleaning);
                List<int> points;
                var constraint = crashInfo.Area * 1000000 / count;
                if (!GetConstraintedPointsConfig(constraint, superCars, people, out points)) continue;

                var time =
                    pointsWithDistances.Last(x => mchsStaffsWithSubstance.Concat(points).Contains(x.Key.Id)).Value /
                    Settings.Default.MchsAverageVelocity;
                if (!(time + count < minimum)) continue;

                _requiredForces = points;
                minTravelTime = time;
                minCleaningDuration = count;
                minimum = time + count;
            }

            progressBar.Complete();
            var res = new List<int>();
            if (minimum > _cleaningTime)
            {
                _reportInfo.Add(new List<string>
                {
                    string.Format("Недостаточно ресурсов для устранения последствий аварии в течение {0} часов.",
                    _cleaningTime)
                }); _reportInfo.Add(new List<string>());
            }
            else
            {
                res = mchsStaffsWithSubstance.Concat(_requiredForces).ToList();
                crashInfo.UntilTime = DateTime.Now.AddHours(minimum);
                superCars = _mchsPoints.Where(x => res.Contains(x.Id)).Aggregate(0, (sum, x) => sum + x.SuperCarCount);
                people = _mchsPoints.Where(x => res.Contains(x.Id))
                    .Aggregate(0, (sum, x) => sum + Math.Min(x.PeopleCount, x.PeopleReady));

                _reportInfo.AddRange(new List<List<string>>
                {
                    new List<string>
                    {
                        "Задействованные пункты МЧС:", res.Aggregate("", (current, x) => current + x + ";")
                    },
                    new List<string>
                    {
                        "Количество антивещества предоставляется:", 
                        Math.Abs(have - (-1.0)) < 1e-5 ? "Не требуется" : have + " тонн"
                    },
                    new List<string>
                    {
                        "Количество работников для устранения предоставляется:", people.ToString()
                    },
                    new List<string>
                    {
                        "Количество спец. машин для устранения предоставляется:", superCars.ToString()
                    },
                    new List<string>
                    {
                        "Время прибытия составов на точку:", 
                        Math.Abs(minTravelTime - (-1.0)) < 1e-5 ? "Не определено" : minTravelTime + " часов",
                    },
                    new List<string>
                    {
                        "Время устранения (после прибытия последнего состава):", 
                        minCleaningDuration == -1 ? "Не определено" : minCleaningDuration + " часов"
                    },
                    new List<string>
                    {
                        "Последствия аварии будут устранены предположительно:", crashInfo.UntilTime.ToString(Resources.DateTimeFormat)
                    }
                }); _reportInfo.Add(new List<string>());
            }
            progressBar.Close();
            until = crashInfo.UntilTime;
            return res;
        }

        /// <summary>
        /// Анализ опасности.
        /// Производится выборка по по промежутку времени по каждой перевозке.
        /// Точка ищется в радиусе KmCorrection, поправка времени - TimeCorrection
        /// </summary>
        /// <param name="targetTime">Начало временного интервала</param>
        /// <param name="place">Место аварии</param>
        /// <param name="windDirection"></param>
        public void AnalyseDanger(DateTime targetTime, FullPointDescription place, double windDirection)
        {
            var progressBar = new ProgressBarForm(@"Анализ опасности, генерация отчёта...", 7000);
            progressBar.Progress(1000);
            _reportInfo.Clear();
            _reportInfo.Add(new List<string>
            {
                "Время аварии:", targetTime.ToString(Resources.DateTimeFormat)
            });
            _reportInfo.Add(new List<string>
            {
                "Место аварии:", place.Address
            });
            _reportInfo.Add(new List<string>
            {
                "", "Координаты:", string.Format("({0}; {1})", place.Position.Lat, place.Position.Lng)
            }); 
            _reportInfo.Add(new List<string>());

            var mostProbably = GetMostProbablyTransit(targetTime, place);
            if (!mostProbably.HasValue)
            {
                MessageBox.Show(string.Format("В радиусе {0} км. от заданного места " +
                                              "с {1}-ти минутной поправкой заданного времени " +
                                              "перевозок опасных грузов не было обнаружено", KmCorrection,
                    TimeCorrection), @"Информация");
                return;
            }
            progressBar.Progress(1000);

            var transInfo = GetTransitInfo(mostProbably.Value);
            _reportInfo.AddRange(transInfo.RecordsForReport());
            _reportInfo.Add(new List<string>());
            progressBar.Progress(1000);

            IChemicalEnvironmentCalculation calculation = new Rd90(transInfo.Consignment, transInfo.ConsignmentCapacity);
            var area = calculation.InfectionArea;
            var antiSubstanceCount = calculation.AntiSubstanceCount;
            var crashInfo = new CrashInfo
            {
                Area = area,
                Center = place,
                Consignment = transInfo.Consignment,
                ConsignmentCapacity = transInfo.ConsignmentCapacity,
                UntilTime = targetTime.AddHours(24),
                WindDirection = windDirection
            };
            var mchsStaffs = new List<int>();
            _reportInfo.AddRange(crashInfo.RecordsForReport()); 
            if(antiSubstanceCount.Key != "")
                _reportInfo.Add(new List<string>
                {
                    "Необходимо обезвреживающего вещества:", antiSubstanceCount.Key, 
                    antiSubstanceCount.Value.ToString(CultureInfo.CurrentCulture), "тонн"
                });
            _reportInfo.Add(new List<string>());
            progressBar.Progress(1000);

            Cursor.Current = Cursors.WaitCursor;
            // сгенерить данные об аварии в отчёт
            _cleaningTime = TimeOfCleaning - (int) ((DateTime.Now.Ticks - targetTime.Ticks)/TimeSpan.TicksPerHour);
            if (area*1000000/(_cleaningTime - 1) > _availableForces)
            {
                _reportInfo.Add(new List<string>
                {
                    string.Format("Недостаточно работников/специализированных автомобилей для устранения последствий аварии" +
                        "\n в течении {0} часов с момента происшествия.", TimeOfCleaning)
                }); _reportInfo.Add(new List<string>());
            }
            else if (antiSubstanceCount.Value > _availableSubstances[antiSubstanceCount.Key])
            {
                _reportInfo.Add(new List<string>
                {
                    "Недостаточно обезвреживающего вещества для устранения последствий аварии."
                }); _reportInfo.Add(new List<string>());
            }
            else
                mchsStaffs = CrashCounting(crashInfo, antiSubstanceCount, out crashInfo.UntilTime);
            progressBar.Progress(1000);

            Cursor.Current = Cursors.Default;
            crashInfo.Id = DataHandler.AddCrash(crashInfo.Center.Position, crashInfo.UntilTime, crashInfo.Area, crashInfo.WindDirection, mostProbably.Value, mchsStaffs);

            ExcelReportsGenerator.GenerateReport(_reportInfo, string.Format("Отчёт об аварии №{0}", crashInfo.Id));
            progressBar.Progress(1000);

            Map.RemoveTransitMarker(mostProbably.Value);
            Map.AddCrashMarker(crashInfo);
            Map.ClearMchsStaffs();
            PutMchsPointsFromDbToMap();
            progressBar.Complete();
            progressBar.Close();
        }

        private bool GetConstraintedPointsConfig(double constraint, int superCars, int people, out List<int> res)
        {
            var count = 0;
            res = new List<int>();
            var power = superCars * Settings.Default.SpecialCarLabor + people * Settings.Default.ManLabor;
            while (count < _staffsWithResources.Count && power < constraint)
            {
                var pnt = _staffsWithResources[count++];
                people += Math.Min(pnt.Key.PeopleCount, pnt.Key.PeopleReady);
                superCars += pnt.Key.SuperCarCount;
                power = superCars * Settings.Default.SpecialCarLabor + people * Settings.Default.ManLabor;
                res.Add(pnt.Key.Id);
            }
            return !(power < constraint);
        }

        public IEnumerable<string> GetNamesByNumber(string contact)
        {
            return DataHandler.GetNamesByNumber(contact);
        }

        public void SetCurrentPointOfView(double x, double y)
        {
            Map.SetCurrentViewPoint(new PointLatLng(x, y));
        }

        private readonly List<MchsPointInfo> _mchsPoints = new List<MchsPointInfo>();
        private readonly Dictionary<PointLatLng, HashSet<int>> _probableCrashes = new Dictionary<PointLatLng, HashSet<int>>();
        private List<KeyValuePair<MchsPointInfo, double>> _staffsWithSubstances, _staffsWithResources;
        private List<int> _requiredForces;

        private readonly Dictionary<string, double> _availableSubstances = new Dictionary<string, double>();
        private double _availableForces;

        private readonly List<List<string>> _reportInfo = new List<List<string>>();
    }
}
