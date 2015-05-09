using System.Collections.Generic;
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
        /// <param name="start">положение начального пункта</param>
        /// <param name="goal">положение конечного пункта</param>
        /// <param name="startValue">Время, которое автомобиль был в пути до прибытия в начальный пункт (по умолчанию 0)</param>
        /// <returns></returns>
        List<KeyValuePair<PointLatLng, int>> GetShortTrack(PointLatLng start, PointLatLng goal, int startValue = 0);

        string GetPlacemark(int x, int y);
        void SetStartPoint(PointLatLng start);
        void SetEndPoint(PointLatLng end);
        void ConstructShortTrack();
        void AddTransitMarker(int transitId);
        void RemoveTransitMarker(int transitId);

        /// <summary>
        /// Ключ записи словаря - положение промежуточной точки на карте, 
        /// значение записи словаря - время поездки из пункта отправления 
        /// </summary>
        /// <param name="start">положение начального пункта</param>
        /// <param name="goal">положение конечного пункта</param>
        /// <param name="dont_drive">Вектор городов, которые нужно объехать</param>
        /// <param name="startValue">Время, которое автомобиль был в пути до прибытия в начальный пункт (по умолчанию 0)</param>
        /// <returns></returns>
        //List<KeyValuePair<int, int>> GetShortTrackWithoutPoints(int start, int goal, double[] dont_drive, int startValue = 0);
    }
}