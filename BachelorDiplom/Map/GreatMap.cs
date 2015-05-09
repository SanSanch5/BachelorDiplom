using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

using BachelorLibAPI.Program;
using BachelorLibAPI.Properties;
using BachelorLibAPI.Data;

using Timer = System.Threading.Timer;

namespace BachelorLibAPI.Map
{
    public sealed class OpenStreetGreatMap : IMap
    {

        public OpenStreetGreatMap(ref GMapControl gmap)
        {
            _gmap = gmap;
            _gmap.MapProvider = OpenStreetMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            _gmap.Zoom = 7;
            PointLatLng pnt = new PointLatLng();
            _gmap.SetPositionByKeywords("МГТУ им. Баумана");
            _gmap.DragButton = MouseButtons.Left;
            _gmap.DisableFocusOnMouseEnter = true;
            _gmap.RoutesEnabled = true;
            _gmap.Overlays.Clear();
            _gmap.Overlays.Add(_routesOverlay);
            _gmap.Overlays.Add(_markersOverlay);
            _gmap.Overlays.Add(_startMarkerOverlay);
            _gmap.Overlays.Add(_endMarkerOverlay);
            _gmap.OnMarkerClick += GmapOnMarkerClick;

            AddTransitMarker(new TransitInfo
            {
                Id = 0,
                Car = "Mersedez",
                Consignment = "Аммиак",
                Driver = "Пахомов Александр",
                DriverNumber = "8(916)778-10-28",
                From = "Москва",
                Grz = "Е777КХ777",
                To = "Воронеж"
            });

            SetActions();
            var mTransitsDrawing = new Task(_mTransitsDrawingAction);
            mTransitsDrawing.Start();
        }

        private void GmapOnMarkerClick(object sender, MouseEventArgs mouseEventArgs)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add(@"Удалить", null, (o, args) => _markersOverlay.Markers.Remove(sender as GMarkerGoogle));
            menu.Show(Cursor.Position);
        }

        public void AddTransitMarker(TransitInfo transit)
        {
            var m = new TransitMarker {Transit = transit};
            var pic = new Bitmap("..\\..\\Map\\Resources\\truckyellow.png");
            GeoCoderStatusCode st;
            PointLatLng p = new PointLatLng(55, 37);
            m.Marker = new GMarkerGoogle(p, new Bitmap(pic, new Size(32, 32)))
            {
                ToolTipText =
                    string.Format(
                        "Перевозка #{0}\nОткуда: {1}\nКуда: {2}\nГруз: {3}\nВодитель: {4}\nНомер телефона: {5}\nАвтомобиль: {6}\nГРЗ: {7}\nТекущее местоположение: {8}",
                        m.Transit.Id, m.Transit.From, m.Transit.To, m.Transit.Consignment, m.Transit.Driver, m.Transit.DriverNumber, m.Transit.Car, m.Transit.Grz,
                        // ReSharper disable once PossibleInvalidOperationException
                        GetPlacemark(p, out st).HasValue ? GetPlacemark(p, out st).Value.Address : @"Неизвестно")
            };
            
            _mTransitMarkers.Add(m);
            _markersOverlay.Markers.Add(m.Marker);
        }

        public void RemoveTransitMarker(int transitId)
        {
            var m = _mTransitMarkers.Where(x => x.Transit.Id == transitId).Select(x => x.Marker).ToArray()[0];
            _markersOverlay.Markers.Remove(m);
        }

        public string GetPlacemark(int x, int y)
        {
            GeoCoderStatusCode st;
            var plc = GetPlacemark(_gmap.FromLocalToLatLng(x, y), out st);
            if (st == GeoCoderStatusCode.G_GEO_SUCCESS && plc != null)
            {
                return plc.Value.Address;
            }
            throw new PlacemarkGettingException();
        }

        public void SetStartPoint(PointLatLng start)
        {
            _mStart = start;
            var pic = new Bitmap("..\\..\\Map\\Resources\\truckyellow.png");
            var marker = new GMarkerGoogle(start, new Bitmap(pic, new Size(32, 32)));
            GeoCoderStatusCode st;
            var placemark = GetPlacemark(_mStart, out st);
            if (placemark != null)
                marker.ToolTipText = "Точка отправления:\n" + placemark.Value.Address;
            _startMarkerOverlay.Clear();
            _startMarkerOverlay.Markers.Add(marker);
        }
        public void SetEndPoint(PointLatLng end)
        {
            _mEnd = end;
            var pic = new Bitmap("..\\..\\Map\\Resources\\tractorunitblack.png");
            var marker = new GMarkerGoogle(end, new Bitmap(pic, new Size(32, 32)));
            GeoCoderStatusCode st;
            var placemark = GetPlacemark(_mEnd, out st);
            if (placemark != null)
                marker.ToolTipText = "Точка назначения:\n" + placemark.Value.Address;
            _endMarkerOverlay.Clear();
            _endMarkerOverlay.Markers.Add(marker);
        }

        public void ConstructShortTrack()
        {
            if (_mStadiesGeneration != null && !_mStadiesGeneration.IsCompleted)
                _stadieGenerationCts.Cancel();

            var route = ((OpenStreetMapProvider)_gmap.MapProvider).GetRoute(_mStart, _mEnd, false, false, 11);
            if (route == null || route.Points.Count == 0)
            {
                MessageBox.Show(Resources.CannotConstructShortTrack);
                return;
            }

            var r = new GMapRoute(route.Points, "My route")
            {
                Stroke =
                {
                    Width = 5,
                    Color = Color.Brown
                }
            };

            _routesOverlay.Routes.Add(r);
            _gmap.ZoomAndCenterRoute(r);

            GeoCoderStatusCode st;
            var placemarkEnd = GetPlacemark(_mEnd, out st);
            var placemarkStart = GetPlacemark(_mStart, out st);
            if (placemarkStart != null && (placemarkEnd != null && MessageBox.Show(string.Format("Маршрут: \nиз: {0}\nв: {1}\nРасстояние: {2} км.\nИспользовать этот маршрут для новой перевозки?\n\nВы можете добавить промежуточные точки для уточнения", 
                placemarkStart.Value.Address, placemarkEnd.Value.Address, route.Distance.ToString("N2")), 
                @"Требуется подтверждение!", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                _startMarkerOverlay.Clear();
                _endMarkerOverlay.Clear();
                    // сохранить
                    // запустить таском генерацию всех подмаршрутов...
                var cancellationToken = _stadieGenerationCts.Token;
                _mStadiesGeneration = Task.Run(() => GenerateStadies(cancellationToken, route), cancellationToken);
            }

            _routesOverlay.Clear();
        }

        public List<KeyValuePair<PointLatLng, int>> GetShortTrack(PointLatLng start, PointLatLng goal, int startValue = 0)
        {
            _mStadiesGeneration.Wait();
            return _mDetailedRoute;
        }

        private Placemark? GetPlacemark(PointLatLng pnt, out GeoCoderStatusCode st)
        {
            return ((OpenStreetMapProvider)_gmap.MapProvider).GetPlacemark(pnt, out st);
        }

        private void GenerateStadiesPart(CancellationToken token, int start, int increment, MapRoute route, List<KeyValuePair<PointLatLng, int>> res)
        {
            var routePoints = route.Points;
            var startPoint = routePoints[0];

            for (var i = start; i < routePoints.Count; i += increment)
            {
                if (token.IsCancellationRequested)
                    return;

                var r = ((OpenStreetMapProvider)_gmap.MapProvider).GetRoute(startPoint, routePoints[i], false, false, 11);
                res.Add(new KeyValuePair<PointLatLng, int>(routePoints[i], (int)(60 * r.Distance / Settings.Default.AvegareVelocity)));

                Debug.WriteLine("Добавлена промежуточная точка {0}:{1} Время {2}", res.Last().Key.Lat.ToString("G5"),
                    res.Last().Key.Lng.ToString("G5"),
                    res.Last().Value);
            }
        }

        private void MergeSortedLists(CancellationToken token, List<KeyValuePair<PointLatLng, int>> lst1Res, List<KeyValuePair<PointLatLng, int>> lst2)
        {
            var i = 0;
            var startAdding = false;
            var cnt = lst1Res.Count;
            foreach (var elem in lst2)
            {
                if (token.IsCancellationRequested)
                    return;

                if (startAdding)
                    lst1Res.Add(elem);
                else
                {
                    while (!startAdding && lst1Res[i].Value <= elem.Value)
                    {
                        ++i;
                        if (i >= cnt)
                            startAdding = true;
                    }
                    lst1Res.Insert(i, elem);
                }
            }
        }

        private void GenerateStadies(CancellationToken token, MapRoute route)
        {
            var st = DateTime.Now.Ticks;
            var routePoints = route.Points;
            _mDetailedRoute.Add(new KeyValuePair<PointLatLng, int>(routePoints[0], 0));
            Debug.WriteLine("Добавлено: {0} - {1}", routePoints[0], 0);

            var res1 = new List<KeyValuePair<PointLatLng, int>>();
            var res2 = new List<KeyValuePair<PointLatLng, int>>();
            var res3 = new List<KeyValuePair<PointLatLng, int>>();
            var res4 = new List<KeyValuePair<PointLatLng, int>>();

            var t1 = Task.Run(() => GenerateStadiesPart(token, 100, 400, route, res1), token);
            var t2 = Task.Run(() => GenerateStadiesPart(token, 200, 400, route, res2), token);
            var t3 = Task.Run(() => GenerateStadiesPart(token, 300, 400, route, res3), token);
            var t4 = Task.Run(() => GenerateStadiesPart(token, 400, 400, route, res4), token);
            t1.Wait(token);
            t2.Wait(token);
            if (token.IsCancellationRequested) return;
            var t12 = Task.Run(() => MergeSortedLists(token, res1, res2), token);

            t3.Wait(token);
            t4.Wait(token);
            if (token.IsCancellationRequested) return;
            var t34 = Task.Run(() => MergeSortedLists(token, res3, res4), token);

            t12.Wait(token);
            t34.Wait(token);
            if (token.IsCancellationRequested) return;
            MergeSortedLists(token, res1, res3);

            if (token.IsCancellationRequested) return;

            _mDetailedRoute.AddRange(res1);
            var ost = (routePoints.Count - 1) % 100;
            if (ost != 0)
                _mDetailedRoute.Add(new KeyValuePair<PointLatLng, int>(routePoints.Last(), (int)route.Distance));

            Debug.WriteLine("Обработано {0} объектов через каждые 100. Итоговое время: {1} минут.\nНа расчёт затрачено {2} секунд",
                routePoints.Count, (int)route.Distance, (DateTime.Now.Ticks - st) / TimeSpan.TicksPerSecond);
        }

        private void SetActions()
        {
            _mTransitsDrawingAction = () =>
            {
                var drawingEvent = new AutoResetEvent(false);
                TimerCallback tcb = DrawTransits;
                // ReSharper disable once ObjectCreationAsStatement
                new Timer(tcb, drawingEvent, 0, 60000);

                while (true)
                    drawingEvent.WaitOne();
                // ReSharper disable once FunctionNeverReturns
            };
        }

        private struct TransitMarker
        {
            public TransitInfo Transit;
            public GMarkerGoogle Marker;
        }

        private readonly string _tempFilesDir = @"..\..\TempStadiesFiles\";

        private void DrawTransits(object stateInfo)
        {
            var autoEvent = (AutoResetEvent)stateInfo;
            var filename = _tempFilesDir + DateTime.Now.ToString("HH:mm_dd.MM.yyyy");
            foreach(var transMarker in _mTransitMarkers)
            {
                // ищу файл по текущему времени, ищу в нём номер перевозки, устанавливаю позицию - изи
                if (!File.Exists(filename)) continue;
                using (var fs = new FileStream(filename, FileMode.Open))
                {

                }
                //transMarker.marker.Position = new PointLatLng(transMarker.marker.Position.Lat + 0.002, transMarker.marker.Position.Lng - 0.03);
            }

            Debug.WriteLine("{0} I'm here!", DateTime.Now.ToString("HH:mm_dd.MM.yyyy"));
            autoEvent.Set();
        }

        private readonly GMapControl _gmap;
        private readonly GMapOverlay _startMarkerOverlay = new GMapOverlay("startMarker");
        private readonly GMapOverlay _endMarkerOverlay = new GMapOverlay("endMarker");
        private readonly GMapOverlay _routesOverlay = new GMapOverlay("routes");
        private readonly GMapOverlay _markersOverlay = new GMapOverlay("markers");
        private PointLatLng _mStart, _mEnd;
        readonly List<KeyValuePair<PointLatLng, int>> _mDetailedRoute = new List<KeyValuePair<PointLatLng, int>>();

        private readonly List<TransitMarker> _mTransitMarkers = new List<TransitMarker>();

        private readonly CancellationTokenSource _stadieGenerationCts = new CancellationTokenSource();
        private Task _mStadiesGeneration;
        private Action _mTransitsDrawingAction;
    }
}
