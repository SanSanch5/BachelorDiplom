using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using GMap.NET;

namespace BachelorLibAPI.RoadsMap
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
        List<KeyValuePair<PointLatLng, int>> getShortTrack(PointLatLng start, PointLatLng goal, int startValue = 0);

        string getPlacemark(int x, int y);
        void setStartPoint(PointLatLng _start);
        void setEndPoint(PointLatLng _end);
        void constructShortTrack();
        void addTransitMarker(int _transitID);
        void removeTransitMarker(int _transitID);

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