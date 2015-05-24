using System;
using System.Collections.Generic;
using BachelorLibAPI.Program;
using GMap.NET;

namespace BachelorLibAPI.Data
{
    /// <summary>
    /// Предоставляет необходимый функционал для работы с данными, абстрагируясь от способа их хранения
    /// Перед вызовом любой функции нужно открыть соединение, после закрыть. (OpenConnection, CloseConnection)
    /// </summary>
    public interface IDataHandler
    {
        /// <summary>
        /// Удалить все контакты, записанные за водителя
        /// </summary>
        /// <param name="driverId">ID водителя</param>
        void DelContacts(int driverId);

        /// <summary>
        /// Добавить нового водителя
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="number"></param>
        /// <param name="middleName">Отчество, если есть</param>
        /// <param name="lastName">Фамилия</param>
        /// <returns>ID добавленного водителя</returns>
        int AddNewDriver(string name, string number, string middleName = "", string lastName = "");

        int AddCrash(PointLatLng place, DateTime until, double area, double windDirection, int transitId, IEnumerable<int> mchsIds);
        
        /// <summary>
        /// Удалить водителя
        /// </summary>
        /// <param name="driverId"></param>
        void DelDriver(int driverId);

        /// <summary>
        /// Добавить новую перевозку
        /// </summary>
        /// <param name="driverId"></param>
        /// <param name="carId"></param>
        /// <param name="consName"></param>
        /// <param name="consCapacity"></param>
        /// <param name="startTime"></param>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns>ID добавленной перевозки</returns>
        int AddNewTransit(int driverId, int carId, string consName, double consCapacity, DateTime startTime, PointLatLng startPoint, PointLatLng endPoint);

        /// <summary>
        /// Удалить перевозку
        /// </summary>
        /// <param name="transId"></param>
        void DeleteTransit(int transId);

        void DeleteMchsStaff(int staffId);

        void DeleteCrash(int crashId);

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

        string GetDriversFullName(int driverId);

        string GetCarInformation(int carId);
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
        List<int> GetTransitIDs();

        IEnumerable<int> GetTransitIDs(int driverId);

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

        string GetGrzByCarId(int carId);
        List<string> GetGrzList(); 

        /// <summary>
        /// Определить водителя по номеру первозки
        /// </summary>
        /// <param name="transId">ID перевозки</param>
        /// <returns>ID водителя</returns>
        int GetDriverId(int transId);

        // ReSharper disable once InconsistentNaming
        int GetCarIdByGRZ(string grz);

        int GetTransitCarId(int transitId);

        /// <summary>
        /// Определить груз по номеру перевозки
        /// </summary>
        /// <param name="transId"></param>
        /// <returns></returns>
        string GetConsignmentName(int transId);

        double GetConsignmentCapacity(int transitId);

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
        /// Получить контактные номера водителей с таким именем
        /// </summary>
        /// <param name="driverName">Имя водителя</param>
        /// <returns>Список номеров</returns>
        List<string> GetNumbersByName(string driverName);

        /// <summary>
        /// Возвращает имена водителей с таким номером
        /// </summary>
        List<string> GetNamesByNumber(string contact);
        string GetDriverNumber(int driverId);

        int GetDriverId(string name, string number);

        Tuple<PointLatLng, PointLatLng> GetStartAndEndPoints(int transit);
        Dictionary<string, double> GetStaffAntiSubstances(int staffId);
        IEnumerable<MchsPointInfo> GetMchsPointsInfo();
        CrashInfo GetCrashInfo(int transitId);

        bool IsInAccident(int transitId);
    }   
}
