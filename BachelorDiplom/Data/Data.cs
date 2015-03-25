using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BachelorLibAPI.Data
{
    public struct AnalyseReturnType
    {
        public string consName;
        public int dangerDegree;
        public string afterCrash;
        public string driversName;
        public string driversSurname;
        public List<string> driversNumbers;
        public string city;
        public DateTime location;
    }

    public struct DriverInfoType
    {
        public int ID;
        public string surname;
        public string name;
        public string mName;
        public List<string> numbers;
        public string consName;
        public int dangerDegree;
        public string startLocation;
        public DateTime start;
        public string goalLocation;
        public DateTime probableArrival;
        public DateTime arrival;
        public string probableLocation;
        public bool status;
    }

    /// <summary>
    /// Предоставляет необходимый функционал для работы с данными, абстрагируясь от способа их хранения
    /// </summary>
    public interface IDataHandler
    {
        List<int> MissedDriverIDs { get; }

        /// <summary>
        /// Добавляет новый контакт
        /// </summary>
        /// <param name="driverID">Идентификатор владельца контакта</param>
        /// <param name="contact">Номер телефона</param>
        void AddNewContact(int driverID, string contact);

        /// <summary>
        /// Удалить все контакты, записанные за водителя
        /// </summary>
        /// <param name="driverID">ID водителя</param>
        void DelContacts(int driverID);

        /// <summary>
        /// Добавить нового водителя
        /// </summary>
        /// <param name="lName">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="mName">Отчество, если есть</param>
        /// <returns>ID добавленного водителя</returns>
        int AddNewDriver(string lName, string name, string mName);
        
        /// <summary>
        /// Удалить водителя
        /// </summary>
        /// <param name="driverID"></param>
        void DelDriver(int driverID);

        /// <summary>
        ///  Добавить новый груз
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="dangerDegree">Уровень опасности</param>
        /// <param name="afterCrash">Действия при аварии</param>
        /// <returns>ID добавленного груза</returns>
        int AddNewConsignment(string name, int dangerDegree, string afterCrash);

        /// <summary>
        /// Добавить новую перевозку
        /// </summary>
        /// <param name="driverID"></param>
        /// <param name="consignmentID"></param>
        /// <returns>ID добавленной перевозки</returns>
        int AddNewTransit(int driverID, int consignmentID);

        /// <summary>
        /// Удалить перевозку
        /// </summary>
        /// <param name="transID"></param>
        void DelTransit(int transID);

        /// <summary>
        /// Добавить новый маршрут
        /// </summary>
        /// <param name="transitID"></param>
        /// <param name="startTime">Время отправления</param>
        /// <param name="arrTime">Время прибытия (отмечается по прибытии)</param>
        /// <param name="cities">Список проезжаемых городов</param>
        /// <param name="status">Завершена ли перевозка</param>
        void AddNewRoute(int transitID, DateTime startTime, DateTime arrTime, string cities, bool status);

        /// <summary>
        /// Удалить информацию о маршруте
        /// </summary>
        /// <param name="transID">ID перевозки</param>
        void DelRoute(int transID);

        /// <summary>
        /// Добавить новый населённый пункт
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="parkingTime">Время стоянки</param>
        /// <param name="regionID">ID региона</param>
        /// <returns>ID добавленного населённого пункта</returns>
        int AddNewCity(string name, int parkingTime, int regionID);

        /// <summary>
        /// Добавить регион
        /// </summary>
        /// <param name="name">Название региона</param>
        /// <returns>ID добавленного региона</returns>
        int AddNewRegion(string name);

        /// <summary>
        /// Добавить новую стадию перевозки. Стадии перевозки нужны для быстрого поиска информации обо всех водителях в конкретном месте в конкретное время.
        /// </summary>
        /// <param name="transitID">ID перевозки</param>
        /// <param name="cityID">ID населённого пункта</param>
        /// <param name="noticedTime">Время нахождения</param>
        void AddNewTransitStady(int transitID, int cityID, DateTime noticedTime);

        /// <summary>
        /// Обновляет уровень опасности и действия после аварии.
        /// </summary>
        /// <param name="consID"></param>
        /// <param name="dangerDegree"></param>
        /// <param name="afterCrash"></param>
        void SetConsignmentParameters(int consID, int dangerDegree, string afterCrash);

        /// <summary>
        /// Устанавливает время прибытия и статус завершения для перевозки.
        /// </summary>
        /// <param name="transID"></param>
        /// <param name="start"></param>
        /// <param name="arr"></param>
        void SetEndingStatus(int transID, DateTime start, DateTime arr);

        /// <summary>
        /// Удалить все стадии перевозки с ID этой перевозки
        /// </summary>
        /// <param name="transID"></param>
        void DeleteStadiesByTransitID(int transID);

        /// <summary>
        /// Возвращает полденюю запись в таблице
        /// </summary>
        /// <typeparam name="T">Типом должен являться пользовательский класс, описывающий таблицу БД</typeparam>
        /// <returns></returns>
        T GetLastInTable<T>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Количество записей в таблице</returns>
        int GetTableLength<T>();

        /// <summary>
        /// Существует ли в базе данных такой номер телефона
        /// </summary>
        bool HasPhoneNumber(string contact);

        /// <summary>
        /// Возвращает массив идентификаторов водителей с заданными ФИО
        /// </summary>
        int[] FindDrivers(string lName, string name, string mName);

        /// <summary>
        /// Возвращает ID водителя по его номеру телефона
        /// </summary>
        int DriverWithPhoneNumber(string contact);

        string GetDriversFullName(int driverID);
        List<string> GetDriversFullNames();

        /// <summary>
        /// Возвращает номер населённого пункта по его названию.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int GetCityID(string name);

        string GetCityName(int cityID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Номер региона (или округа, если быть точным)</returns>
        int GetRegionID(string name);

        /// <summary>
        /// Найти идентификатор груза по названию груза.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int GetConsignmentID(string name);

        /// <summary>
        /// Найти ID перевозки по ID водителя и времени отправления
        /// </summary>
        /// <param name="driverID"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        int GetTransitID(int driverID, DateTime start);

        /// <summary>
        /// Найти ID всех первозок в заданный промежуток времени в заданном городе
        /// </summary>
        /// <returns></returns>
        List<int> GetTransitIDs(DateTime start, DateTime until, int placeID);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="until"></param>
        /// <param name="placeID"></param>
        /// <returns></returns>
        List<int> GetTransitIDs(int driverID);

        /// <summary>
        /// Получить список из ID первозок, зарегистрированных ранее указанного времени 
        /// </summary>
        /// <param name="time">"Верхнее" время регистрации</param>
        /// <returns></returns>
        List<int> TransitsBefore(DateTime time);

        /// <summary>
        /// Получить список из ID завершённых перевозок
        /// </summary>
        /// <returns></returns>
        List<int> EndedTransits();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Список названий всех городов</returns>
        List<string> GetCitiesNames();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionID"></param>
        /// <returns>Список идентификаторов городов в регионе (округе)</returns>
        List<int> GetCitiesInRegion(int regionID);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Список названий всех грузов</returns>
        List<string> GetConsignmentsNames();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Список всех контактов</returns>
        List<string> GetNumbers();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Список названий всех регионов</returns>
        List<string> GetRegionsNames();

        /// <summary>
        /// Позволяет узнать продолжительность остановки/стоянки в минутах в заданном городе
        /// </summary>
        /// <param name="cityID"></param>
        /// <returns>Количество минут стоянки</returns>
        int GetParkingMinutesOfTheCity(int cityID);

        /// <summary>
        /// Определить водителя по номеру первозки
        /// </summary>
        /// <param name="transID">ID перевозки</param>
        /// <returns>ID водителя</returns>
        int GetDriverID(int transID);

        /// <summary>
        /// Определить груз по номеру перевозки
        /// </summary>
        /// <param name="transID"></param>
        /// <returns></returns>
        int GetConsignmentID(int transID);

        /// <summary>
        /// Получить имя водителя
        /// </summary>
        /// <param name="driverID">ID водителя</param>
        /// <returns></returns>
        string GetDriverName(int driverID);
        
        /// <summary>
        /// Получить фамилию водителя
        /// </summary>
        /// <param name="driverID">ID водителя</param>
        /// <returns></returns>
        string GetDriverSurname(int driverID);

        /// <summary>
        /// Получить контактные номера водителя
        /// </summary>
        /// <param name="driverID">ID водителя</param>
        /// <returns>Список номеров</returns>
        List<string> GetDriverNumbers(int driverID);

        /// <summary>
        /// Получить название груза
        /// </summary>
        /// <param name="consID">ID груза</param>
        /// <returns></returns>
        string GetConsignmentName(int consID);

        /// <summary>
        /// Получить описание действий при аварии
        /// </summary>
        /// <param name="consID">ID груза</param>
        /// <returns></returns>
        string GetAfterCrashInfo(int consID);

        /// <summary>
        /// Получить уровень опасности груза
        /// </summary>
        /// <param name="consID">ID груза</param>
        /// <returns></returns>
        int GetDangerDegree(int consID);

        /// <summary>
        /// Получить время нахождения водителя в городе в рамках перевозки
        /// </summary>
        /// <param name="transID">ID перевозки</param>
        /// <param name="cityID">ID города</param>
        /// <returns>Оцененное время, когда водитель будет в этом городе</returns>
        List<DateTime> GetLocationTime(int transID, int cityID);

        /// <summary>
        /// Получить время окончания завершённой перевозки
        /// </summary>
        /// <param name="transID">ID перевозки</param>
        /// <returns></returns>
        DateTime GetArrival(int transID);

        /// <summary>
        /// Узнать города, посещаемые водителем в рамках данной перевозки
        /// </summary>
        /// <param name="transID">ID перевозки</param>
        /// <returns>Строка, в которой через пробел записаны ID городов в порядке их посещения</returns>
        string GetVisitsCities(int transID);

        /// <summary>
        /// Получить текущее местонахождение водителя
        /// </summary>
        /// <param name="transID">ID перевозки</param>
        /// <returns></returns>
        int GetCurrentLocation(int transID);

        /// <summary>
        /// Получить статус перевозки
        /// </summary>
        /// <param name="transID">ID перевозки</param>
        /// <returns></returns>
        bool GetStatus(int transID);

        void SubmitChanges();
    }   
}
