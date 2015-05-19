using System;
using System.Collections.Generic;
using BachelorLibAPI.Program;
using GMap.NET;

namespace BachelorLibAPI.Map
{
    public enum MarkerType
    {
        Transit, Staff
    }
    public class MarkerRemoveEventArgs : EventArgs
    {
        public MarkerType MarkerType;
        public int Id { get; set; }
    }
    public delegate void MarkerRemovedEventHandler(object sender, MarkerRemoveEventArgs e);

    /// <summary>
    /// Интерфейс для карты. Классы-реализации должны предоставлять пользователю структуру карты, возможность её обновить
    /// и возможность находить кратчайшие пути с возможным объездом множества населённых пунктов.
    /// </summary>
    public interface IMap
    {
        event MarkerRemovedEventHandler MarkerRemove;
        /// <summary>
        /// Ключ записи словаря - положение промежуточной точки на карте, 
        /// значение записи словаря - время поездки из пункта отправления 
        /// </summary>
        /// <returns></returns>
        List<KeyValuePair<PointLatLng, int>> GetShortTrack();
        string GetPlacemark(int x, int y);
        Tuple<double, double> GetLatLong(int x, int y);
        double GetDistanceBetween(PointLatLng x, PointLatLng y);

        /// <summary>
        /// Попытка получить адрес по длине/широте
        /// </summary>
        /// <param name="pnt">Точка типа длина/широта</param>
        /// <returns>Адрес или сообщение о том, что адрес не удалось определить</returns>
        string GetPlacemark(PointLatLng pnt);
        void SetStartPoint(PointLatLng start);
        string GetStartPoint();
        void SetDanger(int transitId, PointLatLng pnt);

        void SetCurrentViewPoint(PointLatLng pnt);
        void SetMiddlePoint(PointLatLng mid);
        void SetEndPoint(PointLatLng end);
        string GetEndPoint();
        void SetStartPoint(string start);
        void SetMiddlePoint(string mid);
        bool HasMidPoints();
        void SetEndPoint(string end);
        void ConstructShortTrack();
        void AddTransitMarker(TransitInfo transit);
        void AddMchsMarker(MchsPointInfo mchsPoint);
        void RemoveTransitMarker(int transitId);
        bool CheckAdress(string adress);
        string GetCorrectAdress(string adress);
        bool CheckBeforeAdding();
        void DrawDangerRegion(CrashInfo crashInfo);
    }
}