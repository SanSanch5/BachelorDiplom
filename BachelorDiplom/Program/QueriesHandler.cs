using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private ProgressBar _analyseProgress;

        private const int TimeCorrection = 20;

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
                _map.TransitRemove += DelTransit;
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

        private void AddDriver(string lName, string name, string mName, string num1, string num2)
        {
            var newId = DataHandler.AddNewDriver(lName, name, mName);
            AddContactsToDriver(newId, num1, num2);
            MessageBox.Show(@"Водитель успешно добавлен", @"Информация");
        }

        private void AddContactsToDriver(int driverId, string num1, string num2)
        {
            DataHandler.AddNewContact(driverId, num1);
            if (num1 != num2 && num2 != "")
                DataHandler.AddNewContact(driverId, num2);
        }

        private void NamesakesWork(int [] ds, string num1, string num2)
        {
            var numbers = new List<string>();
            foreach (var id in ds)            
                numbers.AddRange(DataHandler.GetDriverNumbers(id));

            var namesakesForm = new NamesakesForm(numbers);
            namesakesForm.ShowDialog();
            if(namesakesForm.DialogResult == DialogResult.OK)
            {
                var number = namesakesForm.Number;
                var driverId = DataHandler.DriverWithPhoneNumber(number);
                AddContactsToDriver(driverId, num1, num2);
            }
        }

        /// <summary>
        /// Добавление нового водителя
        /// Проверка на номер телефона: если такой уже зарегистрирован, то выдаётся соответствующее сообщение
        /// Проверка на наличие водителей с таким же именем, выводятся их номера в отдельном окне,   
        /// Пользователь решает, нужно ли создать нового водителя или же добавить контакты к какому-то из уже зарегистрированных
        /// </summary>
        /// <param name="lName">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="mName">Отчество</param>
        /// <param name="num1">Основной номер телефона</param>
        /// <param name="num2">Дополнительный номер</param>
        public void AddNewDriver(string lName, string name, string mName, string num1, string num2)
        {
            if (DataHandler.HasPhoneNumber(num1))
            {
                throw new Exception("Номер " + num1 + " уже зарегистрирован на водителя с id " + DataHandler.DriverWithPhoneNumber(num1));
            }

            if (num2 != "" && DataHandler.HasPhoneNumber(num2))
            {
                throw new Exception("Номер " + num2 + " уже зарегистрирован на водителя с id " + DataHandler.DriverWithPhoneNumber(num2));
            }

            var namesakes = DataHandler.FindDrivers(lName, name, mName);
            if (namesakes.Length != 0)
            {
                // обработать ситуацию с полными тёзками
                var namesakesBox = new NamesakesBox(lName, name, mName);
                namesakesBox.ShowDialog();
                switch (namesakesBox.DialogResult)
                {
                    case DialogResult.OK:
                        namesakesBox.Close();
                        if (namesakes.Length == 1)
                        {
                            AddContactsToDriver(namesakes[0], num1, num2);
                            MessageBox.Show(@"Контакты успешно добавлены.", @"Информация");
                        }
                        else
                            NamesakesWork(namesakes, num1, num2);
                        break;
                    case DialogResult.Retry:
                        namesakesBox.Close();
                        AddDriver(lName, name, mName, num1, num2);
                        break;
                }
            }
            else
            {
                AddDriver(lName, name, mName, num1, num2);
            }
        }

        /// <summary>
        /// Возвращает ФИО водителя по заданному номеру
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string CheckDriver(string number)
        {
            if (!DataHandler.HasPhoneNumber(number))
                throw new Exception("Водитель с таким номером не найден в базе.");

            return DataHandler.GetDriversFullName(DataHandler.DriverWithPhoneNumber(number));
        }

        private void DelTransits(IEnumerable<int> transIDs)
        {
            foreach (var transId in transIDs)
            {
                DataHandler.DelTransit(transId);
            }
        }

        private void DelTransit(object o, TransitRemoveEventArgs e)
        {
            DataHandler.DelTransit(e.TransitId);
            if (File.Exists(TempFilesDir + string.Format("\\Transits\\{0}", e.TransitId))) 
                File.Delete(TempFilesDir + string.Format("\\Transits\\{0}", e.TransitId));

            MessageBox.Show(@"Перевозка удалена", @"Информация");
        }

        /// <summary>
        /// Удаляет записи о водителе вместе со всеми зарегистрированными на него перевозками
        /// </summary>
        /// <param name="number"></param>
        public void DelDriver(string number)
        {
            var driverId = DataHandler.DriverWithPhoneNumber(number);
            DataHandler.DelDriver(driverId);
            DataHandler.DelContacts(driverId);
            DelTransits(DataHandler.GetTransitIDs(driverId));

            DataHandler.SubmitChanges();
        }

        /// <summary>
        /// Добавляем новую перевозку
        /// Рассчитывается маршрут перевозки.
        /// Рассчитывается время, которое необходимо водителю
        /// для переезда в каждый город из начального.
        /// Полученная информация регистрируется в специальной таблице.
        /// </summary>
        /// <param name="driverId"></param>
        /// <param name="consId"></param>
        /// <param name="start"></param>
        /// <param name="citiesLst"></param>
        public void AddNewTransit(int driverId, int consId, DateTime start, List<string> citiesLst)
        {
//            int citiesCount = placesLst.Count;
//            if (placesLst.Count < 2)
//                throw new Exception("Как минимум водитель посетит 2 города: начальный и конечный!");
//
//            var fullCitiesList = _map.getShortTrack(_dataHandler.GetCityID(placesLst[0]), _dataHandler.GetCityID(placesLst[1]));
//
//            for (int i = 1; i < citiesCount - 1; ++i)
//            {
//                int correction = fullCitiesList.Last().Value;
//                var localCitiesList = _map.getShortTrack(_dataHandler.GetCityID(placesLst[i]), _dataHandler.GetCityID(placesLst[i+1]), correction);
//                localCitiesList.Remove(localCitiesList.First());
//
//                fullCitiesList.AddRange(localCitiesList);
//            }
//
//            string fullCitiesIDsString = "";
//            foreach (var id in fullCitiesList)
//                fullCitiesIDsString += id.Key.ToString() + " ";
//
//            int transID = _dataHandler.AddNewTransit(driverID, consID);
//            _dataHandler.AddNewRoute(transID, start, DateTime.MinValue, fullCitiesIDsString, false);
//
//            citiesCount = fullCitiesList.Count;
//            for (int i = 0; i < citiesCount; ++i)
//            {
//                int currentCityID = fullCitiesList[i].Key;
//                DateTime noticedTime = start.AddMinutes(fullCitiesList[i].Value + _dataHandler.GetParkingMinutesOfTheCity(currentCityID));
//                _dataHandler.AddNewTransitStady(transID, currentCityID, noticedTime);
//            }
        }

        /// <summary>
        /// Удалить все перевозки, зарегистрированные ранее заданного времени
        /// </summary>
        /// <param name="time"></param>
        public void DelTransitsBefore(DateTime time)
        {
            DelTransits(DataHandler.TransitsBefore(time));

            DataHandler.SubmitChanges();
        }

        private Action AddTransitToMap(int id, int currentPosition, PointLatLng currentPoint, int count)
        {
            return () =>
            {
                var carId = DataHandler.GetTransitCarId(id);
                var driverId = DataHandler.GetDriverId(id);
                var startEnd = DataHandler.GetStartAndEndPoints(id);

                try
                {
                    var car =
                        carId == -1 ? @"Не удалось определить автомобиль" : DataHandler.GetCarInformation(carId);
                    var consignment = DataHandler.GetConsignmentName(id);
                    var driver = DataHandler.GetDriverName(driverId);
                    var driverNumber = DataHandler.GetDriverNumbers(driverId).First();
                    var from = new FullPointDescription
                    {
                        Address = Map.GetPlacemark(startEnd.Item1),
                        Position = startEnd.Item1
                    };
                    var to = new FullPointDescription
                    {
                        Address = Map.GetPlacemark(startEnd.Item2),
                        Position = startEnd.Item2
                    };
                    var grz = DataHandler.GetGrzByCarId(carId);
                    var currentPlace = new FullPointDescription
                    {
                        Address = Map.GetPlacemark(currentPoint),
                        Position = currentPoint
                    };
                    var isFinshed = currentPosition == count - 1;
                    var ti = new TransitInfo
                    {
                        Car = car,
                        Consignment = consignment,
                        Driver = driver,
                        DriverNumber = driverNumber,
                        From = from,
                        To = to,
                        Grz = grz,
                        Id = id,
                        CurrentPlace = currentPlace,
                        IsFinshed = isFinshed
                    };
                    
                    Map.AddTransitMarker(ti);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            };
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
                    var currentPosition = -1;
                    var currentPoint = new PointLatLng(0, 0);
                    var counter = 0;
                    var fs = new FileStream(TempFilesDir + string.Format("\\Transits\\{0}", transitId), FileMode.Open);
                    using (var sr = new StreamReader(fs))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            ++counter;
                            var splitted = line.Split(';').ToList();
                            var tm = DateTime.Parse(splitted[0]);
                            if (tm > DateTime.Now) continue;
                            ++currentPosition;
                            currentPoint.Lat = double.Parse(splitted[1]);
                            currentPoint.Lng = double.Parse(splitted[2]);
                        }
                    }

                    if (currentPosition == -1) continue;

                    var id = transitId;
                    tasks.Add(Task.Run(AddTransitToMap(id, currentPosition, currentPoint, counter)));
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
        /// <param name="start"></param>
        /// <param name="driverName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void AddNewWaybill(string driverName, string driverNum, string grz, string consName, DateTime start,
            string from, string to)
        {
            var driverId = DataHandler.DriverWithPhoneNumber(driverNum);

            if (driverId == -1)
                throw new Exception("Водитель с таким номером не найден");

            var name = DataHandler.GetDriverName(driverId);
            if (name != driverName)
                throw new Exception(string.Format("Водителя с таким номером телефона зовут {0}, а не {1}", name, driverName));

            var carId = DataHandler.GetCarIdByGRZ(grz);

            if (carId == -1)
                throw new Exception("Нет автомобиля с таким регистрационным знаком");

            Cursor.Current = Cursors.WaitCursor;
            var detailedRoute = Map.GetShortTrack();
            var transitId = DataHandler.AddNewTransit(driverId, carId, consName, start, detailedRoute.First().Key,
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
                    Driver = driverName,
                    DriverNumber = driverNum,
                    From = new FullPointDescription
                    {
                        Address = from,
                        Position = detailedRoute.First().Key
                    },
                    To = new FullPointDescription
                    {
                        Address = to,
                        Position = detailedRoute.Last().Key
                    },
                    Grz = grz,
                    Id = transitId,
                    CurrentPlace = new FullPointDescription
                    {
                        Address = Map.GetPlacemark(detailedRoute[currentPosition].Key),
                        Position = detailedRoute[currentPosition].Key
                    },
                    IsFinshed = currentPosition == detailedRoute.Count - 1
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

        /// <summary>
        /// Анализ опасности.
        /// Производится выборка по по промежутку времени по каждой перевозке
        /// для каждого города из указанного региона
        /// </summary>
        /// <param name="since">Начало временного интервала</param>
        /// <param name="until">Конец временного интервала</param>
        /// <param name="place">Место аварии (город или регион)</param>
        /// <returns>Список найденных опасных грузов, водителей и мест с подробной информацией о них.</returns>
        public List<AnalyseReturnType> AnalyseDanger(DateTime since, DateTime until, string place)
        {
            var res = new List<AnalyseReturnType>();
            //List<int> citiesIDs = new List<int>();
            //int cityPlaceID = _dataHandler.GetCityID(place);
            //if (cityPlaceID != -1)
            //    citiesIDs.Add(cityPlaceID);
            //else
            //{
            //    int regionPlaceID = _dataHandler.GetRegionID(place);

            //    if(regionPlaceID == -1)
            //        throw new Exception("Не удалось идентифицировать место аварии.");

            //    citiesIDs.AddRange(_dataHandler.GetCitiesInRegion(regionPlaceID));
            //}

            //since = since.AddMinutes(-_timeCorrection);
            //until = until.AddMinutes(_timeCorrection);
            //SetProgressParameters(citiesIDs, since, until);

            //foreach (var cityID in citiesIDs)
            //{
            //    List<int> transIDs = _dataHandler.GetTransitIDs(since, until, cityID);
            //    foreach (int transID in transIDs)
            //    {
            //        AnalyseReturnType foundTrans = new AnalyseReturnType();
            //        int driverID = _dataHandler.GetDriverID(transID);
            //        int consID = _dataHandler.GetConsignmentID(transID);

            //        foundTrans.afterCrash = _dataHandler.GetAfterCrashInfo(consID);
            //        foundTrans.consName = _dataHandler.GetConsignmentName(consID);
            //        foundTrans.dangerDegree = _dataHandler.GetDangerDegree(consID);
            //        foundTrans.driversName = _dataHandler.GetDriverName(driverID);
            //        foundTrans.driversNumbers = _dataHandler.GetDriverNumbers(driverID);
            //        foundTrans.driversSurname = _dataHandler.GetDriverSurname(driverID);
            //        foundTrans.city = _dataHandler.GetCityName(cityID);
            //        List<DateTime> locations = new List<DateTime>(_dataHandler.GetLocationTime(transID, cityID));

            //        var samePlace = res.Where(x => x.city == foundTrans.city 
            //            && x.driversNumbers.First() == foundTrans.driversNumbers.First()).ToList<AnalyseReturnType>();
            //        foreach(var v in samePlace)
            //            locations.Remove(v.location);

            //        foundTrans.location = locations.First();

            //        res.Add(foundTrans);

            //        ++_analyseProgress.Value;
            //    }
            //}

            return res;
        }

        /// <summary>
        /// Специальный метод для пользователей, которые в ComboBox загрузили весь список водителей (ФИО)
        /// В любом другом случае не использовать!!!
        /// </summary>
        /// <param name="index">Индекс водителя в ComboBox</param>
        /// <returns>Индекс водителя в базе данных</returns>
        public int GetComboBoxedDriverId(int index)
        {
            var driverId = index + 1;
            //foreach (int missedID in _dataHandler.MissedDriverIDs)
            //{
            //    if (missedID <= driverID)
            //        driverID++;
            //}
            return driverId;
        }

        /// <summary>
        /// Специальный метод для пользователей, которые в ComboBox загрузили весь список водителей (ФИО)
        /// В любом другом случае не использовать!!!
        /// </summary>
        /// <param name="index">Индекс водителя в ComboBox</param>
        /// <returns>Информацию о водителе</returns>
        public DriverInfoType GetComboBoxedDriverInfo(int index)
        {
            return GetDriverInfo(GetComboBoxedDriverId(index));
        }

        /// <summary>
        /// Получить все номера водителя
        /// </summary>
        /// <param name="driverId">ID водителя</param>
        /// <returns></returns>
        public List<string> GetDriverNumbers(int driverId)
        {
            return DataHandler.GetDriverNumbers(driverId);
        }

        /// <summary>
        /// Получить информацию о каждом водителе и о его последней перевозке
        /// </summary>
        /// <param name="driverId">ID водителя</param>
        /// <returns></returns>
        public DriverInfoType GetDriverInfo(int driverId)
        {
            var driverInfo = new DriverInfoType();

            //var transIDs = _dataHandler.GetTransitIDs(driverID);
            //if(transIDs.Count() == 0)
            //    throw new NullReferenceException("Не найдено ни одной перевозки, зарегистрированной на этого водителя.");

            //int lastTransID = transIDs.Last();
            //int consID = _dataHandler.GetConsignmentID(lastTransID);
            //driverInfo.ID = driverID;
            //driverInfo.consName = _dataHandler.GetConsignmentName(consID);
            //driverInfo.dangerDegree = _dataHandler.GetDangerDegree(consID);

            //string cities = _dataHandler.GetVisitsCities(lastTransID);
            //int[] cityIDs = cities.Split(new char[] { ' ', ',' }).Where(x => x != "").Select(x => int.Parse(x)).ToArray();
            //int lastCityID = cityIDs[cityIDs.Length - 1];
            //driverInfo.goalLocation = _dataHandler.GetCityName(lastCityID);
            //driverInfo.startLocation = _dataHandler.GetCityName(cityIDs[0]);
            //driverInfo.numbers = _dataHandler.GetDriverNumbers(driverID);

            //if (driverInfo.status = _dataHandler.GetStatus(lastTransID))
            //    driverInfo.arrival = _dataHandler.GetArrival(lastTransID);
            //else
            //{
            //    driverInfo.start = _dataHandler.GetLocationTime(lastTransID, cityIDs[0]).First();
            //    if (driverInfo.start > DateTime.Now)
            //        driverInfo.probableLocation = driverInfo.startLocation;
            //    else
            //        driverInfo.probableLocation = _dataHandler.GetCityName(_dataHandler.GetCurrentLocation(lastTransID));
            //    driverInfo.probableArrival = _dataHandler.GetLocationTime(lastTransID, lastCityID).Last();
            //}

            return driverInfo;
        }

        /// <summary>
        /// Получить полный список ФИО всех водителей
        /// </summary>
        /// <returns></returns>
        public List<string> GetDriversFullNames()
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
    }
}
