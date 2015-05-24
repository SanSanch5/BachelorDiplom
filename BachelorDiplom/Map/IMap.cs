using System;
using System.Collections.Generic;
using BachelorLibAPI.Program;
using GMap.NET;

namespace BachelorLibAPI.Map
{
    /// <summary>
    /// Тип маркера. Необходим для различения реакции на событие, отправляемое при удалении маркера с карты.
    /// </summary>
    public enum MarkerType
    {
        Transit, Staff, Crash
    }

    /// <summary>
    /// Аргументы события, инициируемого при удалении маркера с карты. 
    /// </summary>
    public class MarkerRemoveEventArgs : EventArgs
    {
        public MarkerType MarkerType;
        public int Id { get; set; }
    }

    /// <summary>
    /// Делегат, хранящий обработчики событий, отправляемого при удалении маркера с карты.
    /// </summary>
    /// <param name="sender">Объект-отправитель.</param>
    /// <param name="e">Аргументы события.</param>
    public delegate void MarkerRemovedEventHandler(object sender, MarkerRemoveEventArgs e);

    /// <summary>
    /// Интерфейс для карты. Классы-реализации должны предоставлять пользователю структуру карты, возможность её обновить
    /// и возможность находить кратчайшие пути с возможным объездом множества населённых пунктов.
    /// </summary>
    public interface IMap
    {
        event MarkerRemovedEventHandler MarkerRemove;

        void ClearMchsStaffs();

        /// <summary>
        /// Ключ записи словаря - положение промежуточной точки на карте, 
        /// значение записи словаря - время поездки из пункта отправления 
        /// </summary>
        /// <returns></returns>
        List<KeyValuePair<PointLatLng, int>> GetShortTrack();

        /// <summary>
        /// Определяет адрес в точке, которую пользователь определил посредством клика по карте.
        /// </summary>
        /// <param name="x">Отступ от левого края карты.</param>
        /// <param name="y">Отступ от верхнего края карты.</param>
        /// <returns>Адрес.</returns>
        string GetPlacemark(int x, int y);

        /// <summary>
        /// Определяет точку типа широта/долгота, которую пользователь определил посредством клика по карте.
        /// </summary>
        /// <param name="x">Отступ от левого края карты.</param>
        /// <param name="y">Отступ от верхнего края карты.</param>
        /// <returns>Пару значений широта/долгота.</returns>
        Tuple<double, double> GetLatLong(int x, int y);

        /// <summary>
        /// Определяет точку типа широта/долгота, которую пользователь определил посредством ввода адреса.
        /// </summary>
        /// <returns>Пару значений широта/долгота.</returns>
        Tuple<double, double> GetPoint(string address); 

        /// <summary>
        /// Находит расстояние между двумя точками типа широта/долгота.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        double GetDistanceBetween(PointLatLng x, PointLatLng y);

        /// <summary>
        /// Попытка получить адрес по длине/широте
        /// </summary>
        /// <param name="pnt">Точка типа длина/широта</param>
        /// <returns>Адрес или сообщение о том, что адрес не удалось определить</returns>
        string GetPlacemark(PointLatLng pnt);

        /// <summary>
        /// Устанавливает начальную точку маршрута.
        /// </summary>
        /// <param name="start">Точка типа широта/долгота.</param>
        void SetStartPoint(PointLatLng start);

        /// <summary>
        /// Получает начальную точку маршрута.
        /// </summary>
        /// <returns></returns>
        string GetStartPoint();

        /// <summary>
        /// Устанавливает карту в такое положение, в котором точка pnt является центром карты.
        /// </summary>
        /// <param name="pnt"></param>
        void SetCurrentViewPoint(PointLatLng pnt);

        /// <summary>
        /// Добавляет промежуточную точку.
        /// </summary>
        /// <param name="mid">Точка типа широта/долгота.</param>
        void SetMiddlePoint(PointLatLng mid);

        /// <summary>
        /// Устанавливает конечную точку.
        /// </summary>
        /// <param name="end">Точка типа широта/долгота.</param>
        void SetEndPoint(PointLatLng end);

        /// <summary>
        /// Получает конечную точку.
        /// </summary>
        string GetEndPoint();

        /// <summary>
        /// Устанавливает начальную точку.
        /// </summary>
        /// <param name="start">Адрес места.</param>
        void SetStartPoint(string start);

        /// <summary>
        /// Устанавливает промежуточную точку.
        /// </summary>
        /// <param name="mid">Адрес места.</param>
        void SetMiddlePoint(string mid);
        bool HasMidPoints();

        /// <summary>
        /// Устанавливает конечную точку.
        /// </summary>
        /// <param name="end">Адрес места.</param>
        void SetEndPoint(string end);

        /// <summary>
        /// Конструирует промежуточные стадии пути.
        /// </summary>
        void ConstructShortTrack();

        /// <summary>
        /// Добавляет на карту маркер перевозки.
        /// </summary>
        /// <param name="transit">Информация о перевозке.</param>
        void AddTransitMarker(TransitInfo transit);
        /// <summary>
        /// Добавляет на карту маркер пункта реагирования.
        /// </summary>
        /// <param name="mchsPoint">Информация о пункте расположения сил МЧС.</param>
        void AddMchsMarker(MchsPointInfo mchsPoint);
        /// <summary>
        /// Добавляет на карту маркер аварии.
        /// </summary>
        /// <param name="crashInfo">Информация об аварии.</param>
        void AddCrashMarker(CrashInfo crashInfo);

        /// <summary>
        /// Удаляет с карты маркер перевозки.
        /// </summary>
        /// <param name="transitId">Идентификатор перевозки, которую необходимо удалить.</param>
        void RemoveTransitMarker(int transitId);
        /// <summary>
        /// Проверяет, если ли географическая точка, соответствующая такому адресу.
        /// </summary>
        bool CheckAdress(string adress);

        /// <summary>
        /// Метод для получения полного адреса географиеского местоположения
        /// по одному/нескольким набором ключевых слов.
        /// </summary>
        /// <param name="adress">Набор ключевых слов</param>
        /// <returns>Полный адрес</returns>
        string GetCorrectAdress(string adress);

        /// <summary>
        /// Демонстрирует пользователю построенные маршру со всеми промежуточными точками.
        /// Предоставляет возможность внести необходимые поправки перед добавлением перевозки.
        /// </summary>
        bool CheckBeforeAdding();
    }
}