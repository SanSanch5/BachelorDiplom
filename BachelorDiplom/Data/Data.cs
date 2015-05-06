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
        /// 
        /// </summary>
        /// <returns>Список всех контактов</returns>
        List<string> GetNumbers();

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
        string GetConsignmentName(int transID);

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
        /// Получить время нахождения водителя в городе в рамках перевозки
        /// </summary>
        /// <param name="transID">ID перевозки</param>
        /// <param name="cityID">ID города</param>
        /// <returns>Оцененное время, когда водитель будет в этом городе</returns>
        List<DateTime> GetLocationTime(int transID, GMap.NET.PointLatLng pnt);

        /// <summary>
        /// Получить текущее местонахождение водителя
        /// </summary>
        /// <param name="transID">ID перевозки</param>
        /// <returns></returns>
        GMap.NET.PointLatLng GetCurrentLocation(int transID);

        void SubmitChanges();
    }   
}
