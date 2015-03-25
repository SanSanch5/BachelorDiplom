using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

//using System.Transactions;

using BachelorLibAPI.Data;
using BachelorLibAPI.RoadsMap;
using BachelorLibAPI.Forms;

namespace BachelorLibAPI
{
    /// <summary>
    /// Исполняет прецеденты, используя функционал, предоставляемый интерфейсами IDataHandler и IMap
    /// </summary>
    public class QueriesHandler
    {
        private IDataHandler _dataHandler;
        private IMap _map;
        private ProgressBar _analyseProgress;

        private const int _timeCorrection = 20;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dh">Реализация интерфейса IDataHandler</param>
        /// <param name="m">Реализация интерфейса IMap</param>
        public QueriesHandler(IDataHandler dh, IMap m)
        {
            _dataHandler = dh;
            _map = m;
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
        public IDataHandler DataHandler
        {
            get { return _dataHandler; }
            set { _dataHandler = value; }
        }

        /// <summary>
        /// Свойство устанавливает новую реализацию интерфейса IMap и возвращает текущуюю
        /// </summary>
        public IMap Map
        {
            get { return _map; }
            set { _map = value; }
        }

        private void AddDriver(string lName, string name, string mName, string num1, string num2)
        {
            int newID = _dataHandler.AddNewDriver(lName, name, mName);
            AddContactsToDriver(newID, num1, num2);
            MessageBox.Show("Водитель успешно добавлен", "Информация");
        }

        private void AddContactsToDriver(int driverID, string num1, string num2)
        {
            _dataHandler.AddNewContact(driverID, num1);
            if (num1 != num2 && num2 != "")
                _dataHandler.AddNewContact(driverID, num2);
        }

        private void NamesakesWork(int [] IDs, string num1, string num2)
        {
            List<string> numbers = new List<string>();
            foreach (int ID in IDs)            
                numbers.AddRange(_dataHandler.GetDriverNumbers(ID));

            NamesakesForm namesakesForm = new NamesakesForm(numbers);
            namesakesForm.ShowDialog();
            if(namesakesForm.DialogResult == DialogResult.OK)
            {
                string number = namesakesForm.Number;
                int driverID = _dataHandler.DriverWithPhoneNumber(number);
                AddContactsToDriver(driverID, num1, num2);
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
            if (_dataHandler.HasPhoneNumber(num1))
            {
                throw new Exception("Номер " + num1 + " уже зарегистрирован на водителя с id " + _dataHandler.DriverWithPhoneNumber(num1));
            }

            if (num2 != "" && _dataHandler.HasPhoneNumber(num2))
            {
                throw new Exception("Номер " + num2 + " уже зарегистрирован на водителя с id " + _dataHandler.DriverWithPhoneNumber(num2));
            }

            var namesakes = _dataHandler.FindDrivers(lName, name, mName);
            if (namesakes.Length != 0)
            {
                // обработать ситуацию с полными тёзками
                NamesakesBox namesakesBox = new NamesakesBox(lName, name, mName);
                namesakesBox.ShowDialog();
                switch (namesakesBox.DialogResult)
                {
                    case DialogResult.OK:
                        namesakesBox.Close();
                        if (namesakes.Length == 1)
                        {
                            AddContactsToDriver(namesakes[0], num1, num2);
                            MessageBox.Show("Контакты успешно добавлены.", "Информация");
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
            if (!_dataHandler.HasPhoneNumber(number))
                throw new Exception("Водитель с таким номером не найден в базе.");

            return _dataHandler.GetDriversFullName(_dataHandler.DriverWithPhoneNumber(number));
        }

        private void DelTransits(List<int> transIDs)
        {
            foreach (int transID in transIDs)
            {
                _dataHandler.DelTransit(transID);
                _dataHandler.DelRoute(transID);
                _dataHandler.DeleteStadiesByTransitID(transID);
            }
        }

        /// <summary>
        /// Удаляет записи о водителе вместе со всеми зарегистрированными на него перевозками
        /// </summary>
        /// <param name="number"></param>
        public void DelDriver(string number)
        {
            int driverID = _dataHandler.DriverWithPhoneNumber(number);
            _dataHandler.DelDriver(driverID);
            _dataHandler.DelContacts(driverID);
            DelTransits(_dataHandler.GetTransitIDs(driverID));

            _dataHandler.SubmitChanges();
        }

        /// <summary>
        /// Добавить новый тип груза
        /// Если груз с таким именем уже есть в базе, то обновить действие при аварии и уровень опасности
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="dangerDegree">Класс опасности</param>
        /// <param name="afterCrash">Действия при аварии</param>
        public void AddNewConsignment(string name, int dangerDegree, string afterCrash)
        {
            int consID = _dataHandler.GetConsignmentID(name);
            if (consID == -1)
            {
                _dataHandler.AddNewConsignment(name, dangerDegree, afterCrash);
                MessageBox.Show("Груз успешно добавлен", "Информация");                
            }
            else
            {
                _dataHandler.SetConsignmentParameters(consID, dangerDegree, afterCrash);
                MessageBox.Show("Груз успешно обновлён", "Информация");
            }
        }

        /// <summary>
        /// Добавляем новую перевозку
        /// Рассчитывается маршрут перевозки.
        /// Рассчитывается время, которое необходимо водителю
        /// для переезда в каждый город из начального.
        /// Полученная информация регистрируется в специальной таблице.
        /// </summary>
        /// <param name="driverID"></param>
        /// <param name="consID"></param>
        /// <param name="start"></param>
        /// <param name="citiesLst"></param>
        public void AddNewTransit(int driverID, int consID, DateTime start, List<string> citiesLst)
        {
            int citiesCount = citiesLst.Count;
            if (citiesLst.Count < 2)
                throw new Exception("Как минимум водитель посетит 2 города: начальный и конечный!");

            var fullCitiesList = _map.GetShortTrack(_dataHandler.GetCityID(citiesLst[0]), _dataHandler.GetCityID(citiesLst[1]));

            for (int i = 1; i < citiesCount - 1; ++i)
            {
                int correction = fullCitiesList.Last().Value;
                var localCitiesList = _map.GetShortTrack(_dataHandler.GetCityID(citiesLst[i]), _dataHandler.GetCityID(citiesLst[i+1]), correction);
                localCitiesList.Remove(localCitiesList.First());

                fullCitiesList.AddRange(localCitiesList);
            }

            string fullCitiesIDsString = "";
            foreach (var id in fullCitiesList)
                fullCitiesIDsString += id.Key.ToString() + " ";

            int transID = _dataHandler.AddNewTransit(driverID, consID);
            _dataHandler.AddNewRoute(transID, start, DateTime.MinValue, fullCitiesIDsString, false);

            citiesCount = fullCitiesList.Count;
            for (int i = 0; i < citiesCount; ++i)
            {
                int currentCityID = fullCitiesList[i].Key;
                DateTime noticedTime = start.AddMinutes(fullCitiesList[i].Value + _dataHandler.GetParkingMinutesOfTheCity(currentCityID));
                _dataHandler.AddNewTransitStady(transID, currentCityID, noticedTime);
            }
        }

        /// <summary>
        /// Удалить все перевозки, зарегистрированные ранее заданного времени
        /// </summary>
        /// <param name="time"></param>
        public void DelTransitsBefore(DateTime time)
        {
            DelTransits(_dataHandler.TransitsBefore(time));

            _dataHandler.SubmitChanges();
        }

        /// <summary>
        /// Удалить все завершённые перевозки
        /// </summary>
        public void DelEndedTransits()
        {
            DelTransits(_dataHandler.EndedTransits());

            _dataHandler.SubmitChanges();
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
        /// <param name="num"></param>
        /// <param name="consName"></param>
        /// <param name="citiesLst"></param>
        /// <param name="start"></param>
        public void AddNewWaybill(string num, string consName, List<string> citiesLst, DateTime start)
        {
            int driverID = _dataHandler.DriverWithPhoneNumber(num);
            
            if (driverID == -1)
                throw new Exception("Нет водителя с таким номером телефона");
            
            int consID = _dataHandler.GetConsignmentID(consName);
            
            if (consID == -1)
                throw new Exception("Нет груза с таким именем");
            
            List<string> cities = _dataHandler.GetCitiesNames();
            foreach (var city in citiesLst)
                if (!cities.Contains(city))
                    throw new Exception("Нет города под названием " + city);

            List<int> transitIDs = _dataHandler.GetTransitIDs(driverID);
            foreach (int transID in transitIDs)
            {
                _dataHandler.DeleteStadiesByTransitID(transID);
            }
            _dataHandler.SubmitChanges();

            AddNewTransit(driverID, consID, start, citiesLst);
        }

        /// <summary>
        /// Установить статус завершения для перевозки.
        /// </summary>
        /// <param name="num"></param>
        /// <param name="start"></param>
        /// <param name="arr"></param>
        public void SetEndingStatus(string num, DateTime start, DateTime arr)
        {
            /*
             * Устанавливаем завершённость поездки
             * Удаляем все строки из стадий перевозки с ID этой перевозки
             */
            int driverID = _dataHandler.DriverWithPhoneNumber(num);

            if (driverID == -1)
                throw new Exception("Нет водителя с таким номером телефона");

            int transitID = _dataHandler.GetTransitID(driverID, start);
            
            if (transitID == -1)
                throw new Exception("Не было зафиксированно такой перевозки");

            _dataHandler.SetEndingStatus(transitID, start, arr);

            _dataHandler.DeleteStadiesByTransitID(transitID);
            _dataHandler.SubmitChanges();
        }

        private void SetProgressParameters(List<int> citiesIDs, DateTime since, DateTime until)
        {
            _analyseProgress.Visible = true;
            _analyseProgress.Minimum = 0;
            _analyseProgress.Value = 0;
            
            int max = 0;
            foreach (var cityID in citiesIDs)
            {
                List<int> transIDs = _dataHandler.GetTransitIDs(since, until, cityID);
                max += transIDs.Count();
            }
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
            List<AnalyseReturnType> res = new List<AnalyseReturnType>();
            List<int> citiesIDs = new List<int>();
            int cityPlaceID = _dataHandler.GetCityID(place);
            if (cityPlaceID != -1)
                citiesIDs.Add(cityPlaceID);
            else
            {
                int regionPlaceID = _dataHandler.GetRegionID(place);

                if(regionPlaceID == -1)
                    throw new Exception("Не удалось идентифицировать место аварии.");

                citiesIDs.AddRange(_dataHandler.GetCitiesInRegion(regionPlaceID));
            }

            since = since.AddMinutes(-_timeCorrection);
            until = until.AddMinutes(_timeCorrection);
            SetProgressParameters(citiesIDs, since, until);

            foreach (var cityID in citiesIDs)
            {
                List<int> transIDs = _dataHandler.GetTransitIDs(since, until, cityID);
                foreach (int transID in transIDs)
                {
                    AnalyseReturnType foundTrans = new AnalyseReturnType();
                    int driverID = _dataHandler.GetDriverID(transID);
                    int consID = _dataHandler.GetConsignmentID(transID);

                    foundTrans.afterCrash = _dataHandler.GetAfterCrashInfo(consID);
                    foundTrans.consName = _dataHandler.GetConsignmentName(consID);
                    foundTrans.dangerDegree = _dataHandler.GetDangerDegree(consID);
                    foundTrans.driversName = _dataHandler.GetDriverName(driverID);
                    foundTrans.driversNumbers = _dataHandler.GetDriverNumbers(driverID);
                    foundTrans.driversSurname = _dataHandler.GetDriverSurname(driverID);
                    foundTrans.city = _dataHandler.GetCityName(cityID);
                    List<DateTime> locations = new List<DateTime>(_dataHandler.GetLocationTime(transID, cityID));

                    var samePlace = res.Where(x => x.city == foundTrans.city 
                        && x.driversNumbers.First() == foundTrans.driversNumbers.First()).ToList<AnalyseReturnType>();
                    foreach(var v in samePlace)
                        locations.Remove(v.location);

                    foundTrans.location = locations.First();

                    res.Add(foundTrans);

                    ++_analyseProgress.Value;
                }
            }

            return res;
        }

        /// <summary>
        /// Специальный метод для пользователей, которые в ComboBox загрузили весь список водителей (ФИО)
        /// В любом другом случае не использовать!!!
        /// </summary>
        /// <param name="index">Индекс водителя в ComboBox</param>
        /// <returns>Индекс водителя в базе данных</returns>
        public int GetComboBoxedDriverID(int index)
        {
            int driverID = index + 1;
            foreach (int missedID in _dataHandler.MissedDriverIDs)
            {
                if (missedID <= driverID)
                    driverID++;
            }
            return driverID;
        }

        /// <summary>
        /// Специальный метод для пользователей, которые в ComboBox загрузили весь список водителей (ФИО)
        /// В любом другом случае не использовать!!!
        /// </summary>
        /// <param name="index">Индекс водителя в ComboBox</param>
        /// <returns>Информацию о водителе</returns>
        public DriverInfoType GetComboBoxedDriverInfo(int index)
        {
            return GetDriverInfo(GetComboBoxedDriverID(index));
        }

        /// <summary>
        /// Получить все номера водителя
        /// </summary>
        /// <param name="driverID">ID водителя</param>
        /// <returns></returns>
        public List<string> GetDriverNumbers(int driverID)
        {
            return _dataHandler.GetDriverNumbers(driverID);
        }

        /// <summary>
        /// Получить информацию о каждом водителе и о его последней перевозке
        /// </summary>
        /// <param name="driverID">ID водителя</param>
        /// <returns></returns>
        public DriverInfoType GetDriverInfo(int driverID)
        {
            DriverInfoType driverInfo = new DriverInfoType();

            var transIDs = _dataHandler.GetTransitIDs(driverID);
            if(transIDs.Count() == 0)
                throw new NullReferenceException("Не найдено ни одной перевозки, зарегистрированной на этого водителя.");

            int lastTransID = transIDs.Last();
            int consID = _dataHandler.GetConsignmentID(lastTransID);
            driverInfo.ID = driverID;
            driverInfo.consName = _dataHandler.GetConsignmentName(consID);
            driverInfo.dangerDegree = _dataHandler.GetDangerDegree(consID);

            string cities = _dataHandler.GetVisitsCities(lastTransID);
            int[] cityIDs = cities.Split(new char[] { ' ', ',' }).Where(x => x != "").Select(x => int.Parse(x)).ToArray();
            int lastCityID = cityIDs[cityIDs.Length - 1];
            driverInfo.goalLocation = _dataHandler.GetCityName(lastCityID);
            driverInfo.startLocation = _dataHandler.GetCityName(cityIDs[0]);
            driverInfo.numbers = _dataHandler.GetDriverNumbers(driverID);

            if (driverInfo.status = _dataHandler.GetStatus(lastTransID))
                driverInfo.arrival = _dataHandler.GetArrival(lastTransID);
            else
            {
                driverInfo.start = _dataHandler.GetLocationTime(lastTransID, cityIDs[0]).First();
                if (driverInfo.start > DateTime.Now)
                    driverInfo.probableLocation = driverInfo.startLocation;
                else
                    driverInfo.probableLocation = _dataHandler.GetCityName(_dataHandler.GetCurrentLocation(lastTransID));
                driverInfo.probableArrival = _dataHandler.GetLocationTime(lastTransID, lastCityID).Last();
            }

            return driverInfo;
        }

        /// <summary>
        /// Получить полный список ФИО всех водителей
        /// </summary>
        /// <returns></returns>
        public List<string> GetDriversFullNames()
        {
            return _dataHandler.GetDriversFullNames();
        }

        /// <summary>
        /// Получить полный список названий городов
        /// </summary>
        /// <returns></returns>
        public List<string> GetCitiesNames()
        {
            return _dataHandler.GetCitiesNames();
        }

        /// <summary>
        /// Получить полный список названий грузов
        /// </summary>
        /// <returns></returns>
        public List<string> GetConsignmentsNames()
        {
            return _dataHandler.GetConsignmentsNames();
        }

        /// <summary>
        /// получить полный список всех имеющихся в базе контактных номеров
        /// </summary>
        /// <returns></returns>
        public List<string> GetNumbers()
        {
            return _dataHandler.GetNumbers();
        }

        /// <summary>
        /// Получить полный список названий регионов
        /// </summary>
        /// <returns></returns>
        public List<string> GetRegionsNames()
        {
            return _dataHandler.GetRegionsNames();
        }

        /// <summary>
        /// Узнать, зарегистрирован ли на кого-либо номер
        /// </summary>
        /// <param name="num">Проверяемый номер</param>
        /// <returns></returns>
        public bool HasPhoneNumber(string num)
        {
            return _dataHandler.HasPhoneNumber(num);
        }
    }
}
