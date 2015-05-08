using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

using BachelorLibAPI.Exceptions;

namespace BachelorLibAPI.RoadsMap
{
    public sealed class OpenStreetGreatMap : IMap
    {
        public OpenStreetGreatMap(ref GMapControl _gmap)
        {
            m_gmap = _gmap;
            m_gmap.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            m_gmap.Zoom = 7;
            m_gmap.SetPositionByKeywords("МГТУ им. Баумана");
            m_gmap.DragButton = MouseButtons.Left;
            m_gmap.DisableFocusOnMouseEnter = true;
            m_gmap.RoutesEnabled = true;
            m_gmap.Overlays.Clear();
            m_gmap.Overlays.Add(m_routesOverlay);
            m_gmap.Overlays.Add(m_markersOverlay);
            m_gmap.Overlays.Add(m_startMarkerOverlay);
            m_gmap.Overlays.Add(m_endMarkerOverlay);

            addTransitMarker(1);

            setActions();
            m_transitsDrawing = new Task(m_transitsDrawingAction);
            m_transitsDrawing.Start();
        }
        public void addTransitMarker(int _transitID)
        {
            TransitMarker m = new TransitMarker();
            m.transitID = _transitID;
            Bitmap pic = new Bitmap("..\\..\\Map\\Resources\\truckyellow.png");
            m.marker = new GMarkerGoogle(new PointLatLng(55,37), new Bitmap(pic, new Size(32, 32)));
            m.marker.ToolTipText = String.Format("Перевозка #{0}\nОткуда: {1}\nКуда: {2}\nГруз: {3}\nВодитель: {4}\nНомер телефона: {5}\nАвтомобиль: {6}\nГРЗ: {7}", 
                _transitID.ToString(), "", "", "", "", "", "", "");
            m_transitMarkers.Add(m);
            m_markersOverlay.Markers.Add(m.marker);
        }

        public void removeTransitMarker(int _transitID)
        {
            GMarkerGoogle m = m_transitMarkers.Where(x => x.transitID == _transitID).Select(x => x.marker).ToArray()[0];
            m_markersOverlay.Markers.Remove(m);
        }

        public string getPlacemark(int x, int y)
        {
            GeoCoderStatusCode st;
            Placemark? plc = getPlacemark(m_gmap.FromLocalToLatLng(x, y), out st);
            if (st == GeoCoderStatusCode.G_GEO_SUCCESS && plc != null)
            {
                return plc.Value.Address;
            }
            else throw new PlacemarkGettingException();
        }

        public void setStartPoint(PointLatLng _start)
        {
            m_start = _start;
            Bitmap pic = new Bitmap("..\\..\\Map\\Resources\\truckyellow.png");
            GMarkerGoogle marker = new GMarkerGoogle(_start, new Bitmap(pic, new Size(32, 32)));
            GeoCoderStatusCode st;
            marker.ToolTipText = "Точка отправления:\n" + getPlacemark(m_start, out st).Value.Address;
            m_startMarkerOverlay.Clear();
            m_startMarkerOverlay.Markers.Add(marker);
        }
        public void setEndPoint(PointLatLng _end)
        {
            m_end = _end;
            Bitmap pic = new Bitmap("..\\..\\Map\\Resources\\tractorunitblack.png");
            GMarkerGoogle marker = new GMarkerGoogle(_end, new Bitmap(pic, new Size(32, 32)));
            GeoCoderStatusCode st;
            marker.ToolTipText = "Точка назначения:\n" + getPlacemark(m_end, out st).Value.Address;
            m_endMarkerOverlay.Clear();
            m_endMarkerOverlay.Markers.Add(marker);
        }

        public void constructShortTrack()
        {
            if (m_stadiesGeneration != null && !m_stadiesGeneration.IsCompleted)
                m_stadieGenerationCTS.Cancel();

            MapRoute route = ((OpenStreetMapProvider)m_gmap.MapProvider).GetRoute(m_start, m_end, false, false, 11);
            if (route == null || route.Points.Count == 0)
            {
                MessageBox.Show("Не удалось построить маршрут.");
                return;
            }

            GMapRoute r = new GMapRoute(route.Points, "My route");
            r.Stroke.Width = 5;
            r.Stroke.Color = Color.Black;

            m_routesOverlay.Routes.Add(r);
            m_gmap.ZoomAndCenterRoute(r);

            GeoCoderStatusCode st = new GeoCoderStatusCode();
            if (MessageBox.Show("Маршрут: \nиз: " +
                getPlacemark(m_start, out st).Value.Address + "\nв: " +
                getPlacemark(m_end, out st).Value.Address + "\nРасстояние: " +
                route.Distance.ToString("N2") + " км.\n" + 
                "Использовать этот маршрут для новой перевозки?\nВы можете добавить промежуточные точки для уточнения", 
                "Требуется подтверждение!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                m_startMarkerOverlay.Clear();
                m_endMarkerOverlay.Clear();
                    // сохранить
                    // запустить таском генерацию всех подмаршрутов...
                var cancellationToken = m_stadieGenerationCTS.Token;
                m_stadiesGeneration = Task.Run(() => generateStadies(cancellationToken, route), cancellationToken);
            }

            m_routesOverlay.Clear();
        }

        public List<KeyValuePair<PointLatLng, int>> getShortTrack(PointLatLng start, PointLatLng goal, int startValue = 0)
        {
            m_stadiesGeneration.Wait();
            return m_detailedRoute;
        }

        private Placemark? getPlacemark(PointLatLng pnt, out GeoCoderStatusCode st)
        {
            Placemark? plc = null;
            st = GeoCoderStatusCode.G_GEO_BAD_KEY;

            plc = ((OpenStreetMapProvider)m_gmap.MapProvider).GetPlacemark(pnt, out st);

            return plc;
        }

        private void generateStadiesPart(CancellationToken _token, int _start, int _increment, MapRoute _route, List<KeyValuePair<PointLatLng, int>> _res)
        {
            var route = _route.Points;
            var start = route[0];

            for (int i = _start; i < route.Count; i += _increment)
            {
                if (_token.IsCancellationRequested)
                    return;

                MapRoute r = ((OpenStreetMapProvider)m_gmap.MapProvider).GetRoute(start, route[i], false, false, 11);
                _res.Add(new KeyValuePair<PointLatLng, int>(route[i], (int)(60 * r.Distance / Properties.Settings.Default.AvegareVelocity)));

                Debug.WriteLine("Добавлена промежуточная точка {0}:{1} Время {2}", _res.Last().Key.Lat.ToString("G5"),
                    _res.Last().Key.Lng.ToString("G5"),
                    _res.Last().Value);
            }
        }

        private void mergeSortedLists(CancellationToken _token, List<KeyValuePair<PointLatLng, int>> lst1_res, List<KeyValuePair<PointLatLng, int>> lst2)
        {
            int i = 0;
            bool startAdding = false;
            int cnt = lst1_res.Count;
            foreach (var elem in lst2)
            {
                if (_token.IsCancellationRequested)
                    return;

                if (startAdding)
                    lst1_res.Add(elem);
                else
                {
                    while (!startAdding && lst1_res[i].Value <= elem.Value)
                    {
                        ++i;
                        if (i >= cnt)
                            startAdding = true;
                    }
                    lst1_res.Insert(i, elem);
                }
            }
        }

        private void generateStadies(CancellationToken _token, MapRoute _route)
        {
            long st = DateTime.Now.Ticks;
            List<PointLatLng> route = ((MapRoute)_route).Points;
            m_detailedRoute.Add(new KeyValuePair<PointLatLng, int>(route[0], 0));
            Debug.WriteLine("Добавлено: {0} - {1}", route[0], 0);

            List<KeyValuePair<PointLatLng, int>> res1 = new List<KeyValuePair<PointLatLng, int>>();
            List<KeyValuePair<PointLatLng, int>> res2 = new List<KeyValuePair<PointLatLng, int>>();
            List<KeyValuePair<PointLatLng, int>> res3 = new List<KeyValuePair<PointLatLng, int>>();
            List<KeyValuePair<PointLatLng, int>> res4 = new List<KeyValuePair<PointLatLng, int>>();

            var t1 = Task.Run(() => generateStadiesPart(_token, 100, 400, _route, res1), _token);
            var t2 = Task.Run(() => generateStadiesPart(_token, 200, 400, _route, res2), _token);
            var t3 = Task.Run(() => generateStadiesPart(_token, 300, 400, _route, res3), _token);
            var t4 = Task.Run(() => generateStadiesPart(_token, 400, 400, _route, res4), _token);
            t1.Wait();
            t2.Wait();
            if (_token.IsCancellationRequested) return;
            var t12 = Task.Run(() => mergeSortedLists(_token, res1, res2), _token);

            t3.Wait();
            t4.Wait();
            if (_token.IsCancellationRequested) return;
            var t34 = Task.Run(() => mergeSortedLists(_token, res3, res4), _token);

            t12.Wait();
            t34.Wait();
            if (_token.IsCancellationRequested) return;
            mergeSortedLists(_token, res1, res3);

            if (_token.IsCancellationRequested) return;

            m_detailedRoute.AddRange(res1);
            int ost = (route.Count - 1) % 100;
            if (ost != 0)
                m_detailedRoute.Add(new KeyValuePair<PointLatLng, int>(route.Last(), (int)((MapRoute)_route).Distance));

            Debug.WriteLine("Обработано {0} объектов через каждые 100. Итоговое время: {1} минут.\nНа расчёт затрачено {2} секунд",
                route.Count, (int)((MapRoute)_route).Distance, (DateTime.Now.Ticks - st) / TimeSpan.TicksPerSecond);
        }

        private void setActions()
        {
            m_transitsDrawingAction = () =>
            {
                var drawingEvent = new AutoResetEvent(false);
                TimerCallback tcb = drawTransits;
                System.Threading.Timer drawingTimer = new System.Threading.Timer(tcb, drawingEvent, 0, 60000);

                while (true)
                    drawingEvent.WaitOne();
            };
        }

        private struct TransitMarker
        {
            public int transitID;
            public GMarkerGoogle marker;
        }

        private readonly string temp_files_dir = @"..\..\TempStadiesFiles\";

        private void drawTransits(Object _stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)_stateInfo;
            string filename = temp_files_dir + DateTime.Now.ToString("HH:mm_dd.MM.yyyy");
            foreach(var transMarker in m_transitMarkers)
            {
                // ищу файл по текущему времени, ищу в нём номер перевозки, устанавливаю позицию - изи
                if (File.Exists(filename))
                {
                    using (var fs = new FileStream(filename, FileMode.Open))
                    {

                    }
                }
                //transMarker.marker.Position = new PointLatLng(transMarker.marker.Position.Lat + 0.002, transMarker.marker.Position.Lng - 0.03);
            }

            Debug.WriteLine("{0} I'm here!", DateTime.Now.ToString("HH:mm_dd.MM.yyyy"));
            autoEvent.Set();
        }

        private GMapControl m_gmap;
        private GMapOverlay m_startMarkerOverlay = new GMapOverlay("startMarker");
        private GMapOverlay m_endMarkerOverlay = new GMapOverlay("endMarker");
        private GMapOverlay m_routesOverlay = new GMapOverlay("routes");
        private GMapOverlay m_markersOverlay = new GMapOverlay("markers");
        private PointLatLng m_start, m_end;
        List<KeyValuePair<PointLatLng, int>> m_detailedRoute = new List<KeyValuePair<PointLatLng, int>>();

        private List<TransitMarker> m_transitMarkers = new List<TransitMarker>();

        private CancellationTokenSource m_stadieGenerationCTS = new CancellationTokenSource();
        private Task m_stadiesGeneration;
        private Task m_transitsDrawing;
        private Action m_transitsDrawingAction;
    }
}
