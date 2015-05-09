using System;
using System.Collections.Generic;
using GMap.NET;

namespace BachelorLibAPI.Data
{
    public struct TransitInfo
    {
        public int Id;
        public string From;
        public string To;
        public string Consignment;
        public string Driver;
        public string DriverNumber;
        public string Car;
        public string Grz;
    }

    public struct AnalyseReturnType
    {
        public string ConsName;
        public int DangerDegree;
        public string AfterCrash;
        public string DriversName;
        public string DriversSurname;
        public List<string> DriversNumbers;
        public string City;
        public DateTime Location;
    }

    public struct DriverInfoType
    {
        public int Id;
        public string Surname;
        public string Name;
        public string MName;
        public List<string> Numbers;
        public string ConsName;
        public int DangerDegree;
        public string StartLocation;
        public DateTime Start;
        public string GoalLocation;
        public DateTime ProbableArrival;
        public DateTime Arrival;
        public string ProbableLocation;
        public bool Status;
    }

    /// <summary>
    /// Предоставляет необходимый функционал для работы с данными, абстрагируясь от способа их хранения
    /// </summary>
    public interface IDataHandler
    {
        /// <summary>
        /// Добавляет новый контакт
        /// </summary>
        /// <param name="driverId">Идентификатор владельца контакта</param>
        /// <param name="contact">Номер телефона</param>
        void AddNewContact(int driverId, string contact);

        /// <summary>
        /// Удалить все контакты, записанные за водителя
        /// </summary>
        /// <param name="driverId">ID водителя</param>
        void DelContacts(int driverId);

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
        /// <param name="driverId"></param>
        void DelDriver(int driverId);

        /// <summary>
        /// Добавить новую перевозку
        /// </summary>
        /// <param name="driverId"></param>
        /// <param name="consignmentId"></param>
        /// <returns>ID добавленной перевозки</returns>
        int AddNewTransit(int driverId, int consignmentId);

        /// <summary>
        /// Удалить перевозку
        /// </summary>
        /// <param name="transId"></param>
        void DelTransit(int transId);

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

        string GetDriversFullName(int driverId);
        List<string> GetDriversFullNames();

        /// <summary>
        /// Найти ID перевозки по ID водителя и времени отправления
        /// </summary>
        /// <param name="driverId"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        int GetTransitId(int driverId, DateTime start);

        /// <summary>
        /// Найти ID всех первозок в заданный промежуток времени в заданном городе
        /// </summary>
        /// <returns></returns>
        List<int> GetTransitIDs(DateTime start, DateTime until, int placeId);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="until"></param>
        /// <param name="placeID"></param>
        /// <returns></returns>
        List<int> GetTransitIDs(int driverId);

        /// <summary>
        /// Получить список из ID первозок, зарегистрированных ранее указанного времени 
        /// </summary>
        /// <param name="time">"Верхнее" время регистрации</param>
        /// <returns></returns>
        List<int> TransitsBefore(DateTime time);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Список всех контактов</returns>
        List<string> GetNumbers();

        /// <summary>
        /// Определить водителя по номеру первозки
        /// </summary>
        /// <param name="transId">ID перевозки</param>
        /// <returns>ID водителя</returns>
        int GetDriverId(int transId);

        /// <summary>
        /// Определить груз по номеру перевозки
        /// </summary>
        /// <param name="transId"></param>
        /// <returns></returns>
        string GetConsignmentName(int transId);

        /// <summary>
        /// Получить имя водителя
        /// </summary>
        /// <param name="driverId">ID водителя</param>
        /// <returns></returns>
        string GetDriverName(int driverId);
        
        /// <summary>
        /// Получить фамилию водителя
        /// </summary>
        /// <param name="driverId">ID водителя</param>
        /// <returns></returns>
        string GetDriverSurname(int driverId);

        /// <summary>
        /// Получить контактные номера водителя
        /// </summary>
        /// <param name="driverId">ID водителя</param>
        /// <returns>Список номеров</returns>
        List<string> GetDriverNumbers(int driverId);

        /// <summary>
        /// Получить время нахождения водителя в городе в рамках перевозки
        /// </summary>
        /// <param name="transId">ID перевозки</param>
        /// <param name="cityID">ID города</param>
        /// <returns>Оцененное время, когда водитель будет в этом городе</returns>
        List<DateTime> GetLocationTime(int transId, PointLatLng pnt);

        /// <summary>
        /// Получить текущее местонахождение водителя
        /// </summary>
        /// <param name="transId">ID перевозки</param>
        /// <returns></returns>
        PointLatLng GetCurrentLocation(int transId);

        void SubmitChanges();
    }   
}
