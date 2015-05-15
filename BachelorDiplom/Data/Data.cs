using System;
using System.Collections.Generic;
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
        /// <param name="carId"></param>
        /// <param name="consName"></param>
        /// <param name="startTime"></param>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns>ID добавленной перевозки</returns>
        int AddNewTransit(int driverId, int carId, string consName, DateTime startTime, PointLatLng startPoint, PointLatLng endPoint);

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

        Tuple<PointLatLng, PointLatLng> GetStartAndEndPoints(int transit);

        void SubmitChanges();
    }   
}
