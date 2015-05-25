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
        /// Добавляет в базу информацию об аварии.
        /// </summary>
        /// <param name="place">Место аварии.</param>
        /// <param name="until">Время, к которому последствия аварии будут устранены.</param>
        /// <param name="area">Площадь заражения в результате аварии.</param>
        /// <param name="windDirection">Направление ветра при аварии.</param>
        /// <param name="transitId">Идентификатор перевозки.</param>
        /// <param name="mchsIds">Идентификатор пункта реагирования сил МЧС.</param>
        /// <returns>Идентификатор добавленной аварии.</returns>
        int AddCrash(PointLatLng place, DateTime until, double area, double windDirection, int transitId, IEnumerable<int> mchsIds);

        /// <summary>
        /// Добавляет новую перевозку.
        /// </summary>
        /// <param name="driverId">id водителя</param>
        /// <param name="carId">id автомобиля</param>
        /// <param name="consName">Наименование опасного вещества.</param>
        /// <param name="consCapacity">Количество опасного вещества в тоннах.</param>
        /// <param name="startTime">Начало перевозки.</param>
        /// <param name="startPoint">Начальная позиция маршрута.</param>
        /// <param name="endPoint">Конечная позиция маршрута.</param>
        /// <returns>ID добавленной перевозки.</returns>
        int AddNewTransit(int driverId, int carId, string consName, double consCapacity, DateTime startTime, PointLatLng startPoint, PointLatLng endPoint);

        /// <summary>
        /// Удаляет перевозку.
        /// </summary>
        /// <param name="transId">id перевозки.</param>
        void DeleteTransit(int transId);

        /// <summary>
        /// Удаляет пункт реагирования сил МЧС.
        /// </summary>
        /// <param name="staffId">id пункта..</param>
        void DeleteMchsStaff(int staffId);

        /// <summary>
        /// Удаляет информацию об аварии.
        /// </summary>
        /// <param name="crashId">id аварии.</param>
        void DeleteCrash(int crashId);

        /// <summary>
        /// Получает список идентификаторов зарегистрированных перевозок.
        /// </summary>
        List<int> GetTransitIDs();

        /// <summary>
        /// Получает регистрационный знак автомобиля по id автомобиля.
        /// </summary>
        string GetGrzByCarId(int carId);

        /// <summary>
        /// Получает полный спискок зарегистрированных ГРЗ.
        /// </summary>
        /// <returns></returns>
        List<string> GetGrzList(); 

        /// <summary>
        /// Определить водителя по номеру первозки
        /// </summary>
        /// <param name="transId">ID перевозки</param>
        /// <returns>ID водителя</returns>
        int GetDriverId(int transId);

        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// Получает информацию об автомобиле по ГРЗ, который за ним закреплён.
        /// </summary>
        int GetCarIdByGRZ(string grz);

        /// <summary>
        /// Получает id автомобиля.
        /// </summary>
        /// <param name="transitId">id перевозки, в которой этот автомобиль мог участвовать.</param>
        /// <returns></returns>
        int GetTransitCarId(int transitId);

        /// <summary>
        /// Определить груз по номеру перевозки
        /// </summary>
        string GetConsignmentName(int transId);

        /// <summary>
        /// Определить количество вещества в тоннах по номеру перевозки
        /// </summary>
        double GetConsignmentCapacity(int transitId);

        /// <summary>
        /// Получить имя водителя
        /// </summary>
        /// <param name="driverId">ID водителя</param>
        string GetDriverName(int driverId);

        /// <summary>
        /// Возвращает имена водителей с таким номером
        /// </summary>
        IEnumerable<string> GetNamesByNumber(string contact);

        /// <summary>
        /// Возвращает номер телефона, привязанного к водителю с идентификатором id
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        string GetDriverNumber(int driverId);

        /// <summary>
        /// Получает идентификатор водителя.
        /// </summary>
        /// <param name="name">Имя водителя.</param>
        /// <param name="number">Телефон водителя.</param>
        /// <returns></returns>
        int GetDriverId(string name, string number);

        /// <summary>
        /// Получает начальную и конечную точки маршрута.
        /// </summary>
        /// <param name="transit">id перевозки.</param>
        /// <returns></returns>
        Tuple<PointLatLng, PointLatLng> GetStartAndEndPoints(int transit);

        /// <summary>
        /// Получает словарь, где ключом является название обезвреживающего вещества,
        /// имеющегося во всех пунктов, а значением - сумма всех запасов обезвреживающих
        /// ввеществ, сгруппированная по id.
        /// </summary>
        /// <param name="staffId">id пункта реагирования МЧС.</param>
        /// <returns></returns>
        Dictionary<string, double> GetStaffAntiSubstances(int staffId);

        /// <summary>
        /// Получает полный список доступных и зарегистрированных на этот момент пунктов реагирования.
        /// </summary>
        /// <returns></returns>
        IEnumerable<MchsPointInfo> GetMchsPointsInfo();

        /// <summary>
        /// Получает информацию об аварии.
        /// </summary>
        /// <param name="transitId">id перевозки.</param>
        CrashInfo GetCrashInfo(int transitId);

        /// <summary>
        /// Проверяет, находится ли перевозка в аварии.
        /// </summary>
        /// <param name="transitId">id проверяемой перевозкию.</param>
        bool IsInAccident(int transitId);

        string GetCarInformation(int carId);
        int AddNewDriver(string driverName, string driverNum, string middleName = "", string lastName = "");
    }   
}
