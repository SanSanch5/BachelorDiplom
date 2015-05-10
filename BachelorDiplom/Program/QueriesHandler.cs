using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using BachelorLibAPI.Data;
using BachelorLibAPI.Forms;
using BachelorLibAPI.Map;
using BachelorLibAPI.Algorithms;

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

        /// <summary>
        /// Свойство устанавливает новую реализацию интерфейса IMap и возвращает текущуюю
        /// </summary>
        public IMap Map { get; private set; }

        public static List<string> GetConsignmentsNames()
        {
            return Ahov.Coefficient.Select(x => x.Key).ToList();
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
        /// <param name="placesLst"></param>
        /// <param name="start"></param>
        /// <param name="driverName"></param>
        public void AddNewWaybill(string driverName, string driverNum, string grz, string consName, List<string> placesLst, DateTime start)
        {
            var driverId = DataHandler.DriverWithPhoneNumber(driverNum);

            if (driverId == -1)
                throw new Exception("Нет водителя с таким номером телефона");

            var name = DataHandler.GetDriverName(driverId);
            if (name != driverName)
                throw new Exception(string.Format("Водителя с таким номером телефона зовут {0}, а не {1}", name, driverName));

            var carId = DataHandler.GetCarIdByGRZ(driverNum);

            if (carId == -1)
                throw new Exception("Нет автомобиля с таким регистрационным знаком");

            int citiesCount = placesLst.Count;
            if (placesLst.Count < 2)
                throw new Exception("Как минимум водитель посетит 2 города: начальный и конечный!");

            //var fullCitiesList = Map.GetShortTrack(DataHandler.GetCityID(placesLst[0]), _dataHandler.GetCityID(placesLst[1]));

            //for (int i = 1; i < citiesCount - 1; ++i)
            //{
            //    int correction = fullCitiesList.Last().Value;
            //    var localCitiesList = _map.getShortTrack(_dataHandler.GetCityID(placesLst[i]), _dataHandler.GetCityID(placesLst[i + 1]), correction);
            //    localCitiesList.Remove(localCitiesList.First());

            //    fullCitiesList.AddRange(localCitiesList);
            //}

            //string fullCitiesIDsString = "";
            //foreach (var id in fullCitiesList)
            //    fullCitiesIDsString += id.Key.ToString() + " ";

            //int transID = _dataHandler.AddNewTransit(driverID, consID);
            //_dataHandler.AddNewRoute(transID, start, DateTime.MinValue, fullCitiesIDsString, false);

            //citiesCount = fullCitiesList.Count;
            //for (int i = 0; i < citiesCount; ++i)
            //{
            //    int currentCityID = fullCitiesList[i].Key;
            //    DateTime noticedTime = start.AddMinutes(fullCitiesList[i].Value + _dataHandler.GetParkingMinutesOfTheCity(currentCityID));
            //    _dataHandler.AddNewTransitStady(transID, currentCityID, noticedTime);
            //}
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
