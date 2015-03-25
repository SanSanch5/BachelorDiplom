using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BachelorLibAPI.RoadsMap
{
    /// <summary>
    /// Интерфейс для карты. Классы-реализации должны предоставлять пользователю структуру карты, возможность её обновить
    /// и возможность находить кратчайшие пути с возможным объездом множества населённых пунктов.
    /// </summary>
    public interface IMap
    {
        /// <summary>
        /// Ключ - ID основного города
        /// Значение - ассоциативный массив, ключ которого - ID города, смежного с основным,
        /// а значение - время в минутах, необходимое для переезда в город из основного
        /// </summary>
        Dictionary<int, Dictionary<int, int>> RoadMap { get; }

        /// <summary>
        /// Перезагрузить карту
        /// </summary>
        void Reload();

        /// <summary>
        /// Ключ записи словаря - идентификационный номер города в базе данных, 
        /// значение записи словаря - время поездки из пункта отправления 
        /// </summary>
        /// <param name="start">ID начального пункта</param>
        /// <param name="goal">ID конечного пункта</param>
        /// <param name="startValue">Время, которое автомобиль был в пути до прибытия в начальный пункт (по умолчанию 0)</param>
        /// <returns></returns>
        List<KeyValuePair<int, int>> GetShortTrack(int start, int goal, int startValue = 0);

        /// <summary>
        /// Ключ записи словаря - идентификационный номер города в базе данных, 
        /// значение записи словаря - время поездки из пункта отправления 
        /// </summary>
        /// <param name="start">ID начального пункта</param>
        /// <param name="goal">ID конечного пункта</param>
        /// <param name="dont_drive">Вектор городов, которые нужно объехать</param>
        /// <param name="startValue">Время, которое автомобиль был в пути до прибытия в начальный пункт (по умолчанию 0)</param>
        /// <returns></returns>
        List<KeyValuePair<int, int>> GetShortTrackWithoutPoints(int start, int goal, double[] dont_drive, int startValue = 0);
    }

    /// <summary>
    /// Реализация интерфейса карты. Структура заполняется из файла, лежащего в папке с программой.
    /// При изменении структуры файл необходимо обновлять.
    /// </summary>
    public sealed class Graph : IMap
    {
        private static readonly Lazy<Graph> lazy =
            new Lazy<Graph>(() => new Graph());

        public static Graph Instance
        {
            get { return lazy.Value; }
        }

        public void Reload()
        {
            map.Clear();
            this.ReadMapFile();
        }

        public Dictionary<int, Dictionary<int, int>> RoadMap
        {
            get { return map; }
        }

        private int[] hasntCheckedNodes(HashSet<int> wasChecked)
        {
            HashSet<int> allNodes = new HashSet<int>(map.Keys);
            return allNodes.Except<int>(wasChecked).ToArray();
        }

        private int getMinTime(int[] nodes, Dictionary<int, int> sumTimes)
        {
            int n = nodes.Length;
            int min = 0;
            for (int i = 1; i < n; ++i)
            {
                int currTime = sumTimes[nodes[i]];
                if (currTime < sumTimes[nodes[min]]) min = i;
            }
            return nodes[min];
        }

        private List<KeyValuePair<int, int>> getTrack(int start, int goal, Dictionary<int, int> cameFrom, Dictionary<int, int> sumTime)
        {
            List<KeyValuePair<int, int>> track = new List<KeyValuePair<int, int>>();

            int currNode = goal;
            while (currNode != start)
            {
                track.Add(new KeyValuePair<int, int>(currNode, sumTime[currNode]));
                currNode = cameFrom[currNode];
            }
            track.Add(new KeyValuePair<int, int>(start, _startValue));
            track.Reverse();

            return track;
        }

        private List<KeyValuePair<int, int>> DijkstraMod(int start, int goal, HashSet<int> wasChecked)
        {
            int n = map.Count;
            Dictionary<int, int> cameFrom = new Dictionary<int, int>();
            Dictionary<int, int> sumTime = new Dictionary<int, int>(); // метки вершин
            sumTime.Add(start, _startValue);

            foreach (var pair in map)
            {
                if (pair.Key != start) sumTime.Add(pair.Key, int.MaxValue);
                cameFrom.Add(pair.Key, -1);
            }

            while (hasntCheckedNodes(wasChecked).Length != 0)
            {
                int currNode = getMinTime(hasntCheckedNodes(wasChecked), sumTime);
                wasChecked.Add(currNode);
                foreach (var pair in map[currNode])
                {
                    if (!wasChecked.Contains(pair.Key))
                    {
                        int newSum = sumTime[currNode] + pair.Value;
                        if (sumTime[pair.Key] > newSum)
                        {
                            sumTime[pair.Key] = newSum;
                            cameFrom[pair.Key] = currNode;
                        }
                    }
                }
            }

            return getTrack(start, goal, cameFrom, sumTime);
        }

        public List<KeyValuePair<int, int>> GetShortTrack(int start, int goal, int startValue = 0)
        {
            _startValue = startValue;
            HashSet<int> wasChecked = new HashSet<int>();
            return DijkstraMod(start, goal, wasChecked);
        }

        public List<KeyValuePair<int, int>> GetShortTrackWithoutPoints(int start, int goal, double[] dont_drive, int startValue = 0)
        {
            _startValue = startValue;
            HashSet<int> wasChecked = new HashSet<int>();
            foreach (int id in dont_drive)
                wasChecked.Add(id);

            return DijkstraMod(start, goal, wasChecked);
        }

        private static string MAP_FILE = "..\\..\\..\\map.txt";
        private Dictionary<int, Dictionary<int, int>> map;
        private int _startValue;

        private Graph()
        {
            map = new Dictionary<int, Dictionary<int, int>>();
            this.ReadMapFile();
        }

        private void ReadMapFile()
        {
            var infile = new StreamReader(MAP_FILE);
            for (var line = infile.ReadLine(); line != null; line = infile.ReadLine())
            {
                var ints = line.Split().Select(x => int.Parse(x)).ToArray();
                if (ints.Length != 3) throw new Exception("Wrong file record format");
                if (map.ContainsKey(ints[0]))
                {
                    map[ints[0]].Add(ints[1], ints[2]);
                }
                else
                {
                    Dictionary<int, int> dic = new Dictionary<int, int>();
                    dic.Add(ints[1], ints[2]);
                    map.Add(ints[0], dic);
                }
            }
        }
    }
}