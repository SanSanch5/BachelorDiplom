using System.Collections.Generic;
using BachelorLibAPI.Data;
using GMap.NET;

namespace BachelorLibAPI.Map
{
    /// <summary>
    /// Интерфейс для карты. Классы-реализации должны предоставлять пользователю структуру карты, возможность её обновить
    /// и возможность находить кратчайшие пути с возможным объездом множества населённых пунктов.
    /// </summary>
    public interface IMap
    {
        /// <summary>
        /// Ключ записи словаря - положение промежуточной точки на карте, 
        /// значение записи словаря - время поездки из пункта отправления 
        /// </summary>
        /// <returns></returns>
        List<KeyValuePair<PointLatLng, int>> GetShortTrack();
        string GetPlacemark(int x, int y);
        void SetStartPoint(PointLatLng start);
        void SetMiddlePoint(PointLatLng mid);
        void SetEndPoint(PointLatLng end);
        void SetStartPoint(string start);
        void SetMiddlePoint(string mid);
        void SetEndPoint(string end);
        void ConstructShortTrack();
        void AddTransitMarker(TransitInfo transit);
        void RemoveTransitMarker(int transitId);
        bool CheckAdress(string adress);
    }
}