using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
        public PointLatLng? StartPoint { get; private set; }
        public PointLatLng? EndPoint { get; private set; }

        public OpenStreetGreatMap(ref GMapControl gmap)
        {
            _gmap = gmap;
            _gmap.MapProvider = OpenStreetMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            _gmap.Zoom = 7;
            
            _gmap.SetPositionByKeywords("МГТУ им. Баумана");
            _gmap.DragButton = MouseButtons.Left;
            _gmap.DisableFocusOnMouseEnter = true;
            _gmap.RoutesEnabled = true;
            _gmap.Overlays.Clear();
            _gmap.Overlays.Add(_routesOverlay);
            _gmap.Overlays.Add(_markersOverlay);
            _gmap.Overlays.Add(_startMarkerOverlay);
            _gmap.Overlays.Add(_middleMarkersOverlay);
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

        public bool CheckAdress(string adress)
        {
            GeoCoderStatusCode st;
            ((OpenStreetMapProvider) _gmap.MapProvider).GetPoint(adress, out st);
            return st == GeoCoderStatusCode.G_GEO_SUCCESS;
        }

        /// <summary>
        /// Использовать желательно после использования CheckAdress
        /// Иначе ловите исключение типа UnknownPlacemark
        /// </summary>
        public string GetCorrectAdress(string adress)
        {
            if(!CheckAdress(adress))
                throw new UnknownPlacemark();

            GeoCoderStatusCode st;
            var provider = (OpenStreetMapProvider) _gmap.MapProvider;
            // ReSharper disable once PossibleInvalidOperationException
            var plc = GetPlacemark(provider.GetPoint(adress, out st).Value, out st);
            return plc.HasValue ? plc.Value.Address : adress;
        }

        private void GmapOnMarkerClick(object sender, MouseEventArgs mouseEventArgs)
        {
            var menu = new ContextMenuStrip();
            menu.Items.Add(@"Удалить", new Bitmap("..\\..\\Map\\Resources\\cross.png"), (o, args) => RemoveMarkerAdvanced(sender as GMarkerGoogle));
            menu.Show(Cursor.Position);
        }

        private void RemoveMarkerAdvanced(GMarkerGoogle marker)
        {
            if (StartPoint.HasValue && marker.Position == StartPoint.Value)
            {
                _startMarkerOverlay.Clear();
                StartPoint = null;
            }
            else if (EndPoint.HasValue && marker.Position == EndPoint.Value)
            {
                _endMarkerOverlay.Clear();
                EndPoint = null;
            }
            else if (_middlePoints.Contains(marker.Position))
            {
                _middleMarkersOverlay.Markers.Remove(marker);
                _middlePoints.Remove(marker.Position);
            }
            else if (
                MessageBox.Show(@"Вы уверены, что хотите удалить перевозку?", @"Внимание!", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                    // удалить перевозку

                _transitMarkers.Remove(_transitMarkers.Where(x => x.Marker == marker).ToList()[0]);
                _markersOverlay.Markers.Remove(marker);
            }
        }

        public void AddTransitMarker(TransitInfo transit)
        {
            var m = new TransitMarker {Transit = transit};
            var pic = new Bitmap("..\\..\\Map\\Resources\\truckyellow.png");
            GeoCoderStatusCode st;
            var p = new PointLatLng(55, 37);
            m.Marker = new GMarkerGoogle(p, new Bitmap(pic, new Size(32, 32)))
            {
                ToolTipText =
                    string.Format(
                        "Перевозка #{0}\nОткуда: {1}\nКуда: {2}\nГруз: {3}\nВодитель: {4}\nНомер телефона: {5}\nАвтомобиль: {6}\nГРЗ: {7}\nТекущее местоположение: {8}",
                        m.Transit.Id, m.Transit.From, m.Transit.To, m.Transit.Consignment, m.Transit.Driver, m.Transit.DriverNumber, m.Transit.Car, m.Transit.Grz,
                        // ReSharper disable once PossibleInvalidOperationException
                        GetPlacemark(p, out st).HasValue ? GetPlacemark(p, out st).Value.Address : @"Неизвестно")
            };
            
            _transitMarkers.Add(m);
            _markersOverlay.Markers.Add(m.Marker);
        }

        public void RemoveTransitMarker(int transitId)
        {
            var m = _transitMarkers.Where(x => x.Transit.Id == transitId).Select(x => x.Marker).ToArray()[0];
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
            CancelTask(_stadiesGenerationCts, _stadiesGeneration);

            StartPoint = start;
            var pic = new Bitmap("..\\..\\Map\\Resources\\start.png");
            var marker = new GMarkerGoogle(start, new Bitmap(pic, new Size(32, 32)));
            GeoCoderStatusCode st;
            var placemark = GetPlacemark(StartPoint.Value, out st);
            if (placemark != null)
                marker.ToolTipText = "Точка отправления:\n" + placemark.Value.Address;
            _startMarkerOverlay.Clear();
            _startMarkerOverlay.Markers.Add(marker);

            _gmap.Position = StartPoint.Value;
            if (_gmap.Zoom < 14) _gmap.Zoom = 14;
        }

        public void SetStartPoint(string start)
        {
            GeoCoderStatusCode st;
            // ReSharper disable once PossibleNullReferenceException
            var pnt = (_gmap.MapProvider as OpenStreetMapProvider).GetPoint(start, out st);
            if(pnt.HasValue)
                SetStartPoint(pnt.Value);
            else
                throw new UnknownPlacemark();
        }

        public void SetMiddlePoint(PointLatLng mid)
        {
            CancelTask(_stadiesGenerationCts, _stadiesGeneration);

            _middlePoints.Add(mid);
            var pic = new Bitmap("..\\..\\Map\\Resources\\flag_pink.png");
            var marker = new GMarkerGoogle(mid, new Bitmap(pic, new Size(32, 32)));
            GeoCoderStatusCode st;
            var placemark = GetPlacemark(mid, out st);
            if (placemark != null)
                marker.ToolTipText = string.Format("Промежуточная точка #{0}\n{1}", _middlePoints.Count, placemark.Value.Address);
            _middleMarkersOverlay.Markers.Add(marker);

            _gmap.Position = mid;
            if (_gmap.Zoom < 14) _gmap.Zoom = 14;
        }

        public void SetMiddlePoint(string mid)
        {
            GeoCoderStatusCode st;
            // ReSharper disable once PossibleNullReferenceException
            var pnt = (_gmap.MapProvider as OpenStreetMapProvider).GetPoint(mid, out st);
            if (pnt.HasValue)
                SetMiddlePoint(pnt.Value);
            else
                throw new UnknownPlacemark();
        }

        public void SetEndPoint(PointLatLng end)
        {
            CancelTask(_stadiesGenerationCts, _stadiesGeneration);

            EndPoint = end;
            var pic = new Bitmap("..\\..\\Map\\Resources\\finish.png");
            var marker = new GMarkerGoogle(end, new Bitmap(pic, new Size(32, 32)));
            GeoCoderStatusCode st;
            var placemark = GetPlacemark(EndPoint.Value, out st);
            if (placemark != null)
                marker.ToolTipText = "Точка назначения:\n" + placemark.Value.Address;
            _endMarkerOverlay.Clear();
            _endMarkerOverlay.Markers.Add(marker);

            _gmap.Position = EndPoint.Value;
            if(_gmap.Zoom < 14) _gmap.Zoom = 14;
        }
        public void SetEndPoint(string end)
        {
            GeoCoderStatusCode st;
            // ReSharper disable once PossibleNullReferenceException
            var pnt = (_gmap.MapProvider as OpenStreetMapProvider).GetPoint(end, out st);
            if (pnt.HasValue)
                SetEndPoint(pnt.Value);
            else
                throw new UnknownPlacemark();
        }

        private double _distance;
        public bool CheckBeforeAdding()
        {
            Cursor.Current = Cursors.WaitCursor;
            _stadiesGeneration.Wait();
            Cursor.Current = Cursors.Default;

            var r = new GMapRoute(_detailedRoute.Select(x => x.Key).ToList(), "New route")
            {
                Stroke =
                {
                    Width = 2,
                    Color = Color.Brown
                }
            };

            _routesOverlay.Routes.Add(r);
            _gmap.ZoomAndCenterRoute(r);

            GeoCoderStatusCode st;
            var placemarkEnd = GetPlacemark(EndPoint.Value, out st);
            var placemarkStart = GetPlacemark(StartPoint.Value, out st);
            if (placemarkStart == null || (placemarkEnd == null || MessageBox.Show(
                string.Format(
                    "Маршрут: \nиз: {0}\nв: {1}\n{2} промежуточных точек\nРасстояние: {3} км.\n\nВы можете добавить промежуточные точки для уточнения\n\nИспользовать этот маршрут для новой перевозки?",
                    placemarkStart.Value.Address, placemarkEnd.Value.Address, _middlePoints.Count,
                    _distance.ToString("N2")),
                @"Требуется подтверждение!", MessageBoxButtons.YesNo) != DialogResult.Yes))
            {
                _routesOverlay.Clear();
                return false;
            }
            
            _startMarkerOverlay.Clear();
            _endMarkerOverlay.Clear();
            _middleMarkersOverlay.Clear();
            _routesOverlay.Clear();

            StartPoint = EndPoint = null;
            _middlePoints.Clear();
            return true;
        }

        private void CancelTask(CancellationTokenSource cts, Task task)
        {
            if (task == null) return;
            if (!task.IsCompleted)
                cts.Cancel();
            else if (task.IsCanceled)
                Debug.WriteLine("Задача отменена");
            else if (task.IsCompleted || task.IsCanceled)
                _stadiesGeneration.Dispose();
        }

        public void ConstructShortTrack()
        {
            CancelTask(_stadiesGenerationCts, _stadiesGeneration);

            if (StartPoint == null)
                throw new Exception(@"Не задана начальная точка");
            if (EndPoint == null)
                throw new Exception(@"Не задана конечная точка");

            var routePoints = new List<PointLatLng>();
            var curStart = StartPoint.Value;
            _distance = 0;
            MapRoute route;
            _detailedRoute.Clear();

            foreach (var middlePoint in _middlePoints)
            {
                route = ((OpenStreetMapProvider)_gmap.MapProvider).GetRoute(curStart, middlePoint, false, false, 11);
                if (route == null || route.Points.Count == 0)
                    throw new Exception(Resources.CannotConstructShortTrack);

                routePoints.AddRange(route.Points);
                routePoints.RemoveAt(routePoints.Count-1);
                curStart = middlePoint;
                _distance += route.Distance;
            }

            route = ((OpenStreetMapProvider)_gmap.MapProvider).GetRoute(curStart, EndPoint.Value, false, false, 11);
            if (route == null || route.Points.Count == 0)
                throw new Exception(Resources.CannotConstructShortTrack);

            routePoints.AddRange(route.Points);
            _distance += route.Distance;
            
            _stadiesGenerationCts = new CancellationTokenSource();
            var cancellationToken = _stadiesGenerationCts.Token;
            _stadiesGeneration = Task.Run(() => GenerateStadies(cancellationToken, routePoints, 50, 15), cancellationToken);
        }

        public List<KeyValuePair<PointLatLng, int>> GetShortTrack()
        {
            _stadiesGeneration.Wait();
            return _detailedRoute;
        }

        public string GetStartPoint()
        {
            GeoCoderStatusCode st;
            if (!StartPoint.HasValue) return "";

            var plc = GetPlacemark(StartPoint.Value, out st);
            return plc != null ? plc.Value.Address : "";
        }
        public string GetEndPoint()
        {
            GeoCoderStatusCode st;
            if (!EndPoint.HasValue) return "";

            var plc = GetPlacemark(EndPoint.Value, out st);
            return plc != null ? plc.Value.Address : "";
        }

        public bool HasMidPoints()
        {
            return _middlePoints.Count != 0;
        }

        private Placemark? GetPlacemark(PointLatLng pnt, out GeoCoderStatusCode st)
        {
            return ((OpenStreetMapProvider)_gmap.MapProvider).GetPlacemark(pnt, out st);
        }

        private void GenerateStadiesPart(CancellationToken token, int start, int diff, int increment,
            IReadOnlyList<PointLatLng> routePoints, ICollection<KeyValuePair<PointLatLng, int>> res)
        {
            for (var i = diff*start + diff; i < routePoints.Count; i += increment)
            {
                if (token.IsCancellationRequested)
                    return;

                var r = ((OpenStreetMapProvider) _gmap.MapProvider).GetRoute(routePoints[i - diff], routePoints[i],
                    false, false, 11);

                var tmp = 1; // попытка избежать невозможность построения подмаршрута...
                while (r == null)
                    r = ((OpenStreetMapProvider) _gmap.MapProvider).GetRoute(routePoints[i - diff + diff/10*(tmp++)],
                        routePoints[i], false, false, 11);

                res.Add(new KeyValuePair<PointLatLng, int>(routePoints[i],
                    (int) (60*r.Distance/Settings.Default.AvegareVelocity)));

                Debug.WriteLine("До промежуточной точки {0}:{1} из предыдущей ({2} назад) Время {3}",
                    res.Last().Key.Lat.ToString("G5"),
                    res.Last().Key.Lng.ToString("G5"),
                    diff,
                    res.Last().Value);
            }
        }

        private void GenerateStadies(CancellationToken token, IReadOnlyList<PointLatLng> routePoints, int diff,
            int threadsCount = 4)
        {
            var st = DateTime.Now.Ticks;
            _detailedRoute.Add(new KeyValuePair<PointLatLng, int>(routePoints[0], 0));
            Debug.WriteLine("Добавлено: {0} - {1}", routePoints[0], 0);

            var results = new List<List<KeyValuePair<PointLatLng, int>>>();
            var tasks = new List<Task>();

            try
            {
                for (var i = 0; i < threadsCount; ++i)
                {
                    results.Add(new List<KeyValuePair<PointLatLng, int>>());
                    var i1 = i;
                    tasks.Add(
                        Task.Run(() => GenerateStadiesPart(token, i1, diff, diff*threadsCount, routePoints, results[i1]),
                            token));
                }

                var ost = (routePoints.Count - 1)%diff;
                var lastHandledPoint = routePoints.Count - 1 - ost;
                var counter = 0;
                var currentTime = 0;
                    // жёсткая логика для вытаскивания данных из другого потока
                while (counter < (routePoints.Count - 1)/diff*threadsCount)
                {
                    for (var i = 0; i < results.Count; i++)
                    {
                        var result = results[i];
                        var added = false;
                        while (!added)
                        {
                            if (tasks[i].IsCompleted)
                            {
                                if (result.Count > counter)
                                    _detailedRoute.Add(new KeyValuePair<PointLatLng, int>(result[counter].Key,
                                        currentTime += result[counter].Value));
                                added = true;
                            }
                            else if (result.Count > counter)
                            {
                                _detailedRoute.Add(new KeyValuePair<PointLatLng, int>(result[counter].Key,
                                    currentTime += result[counter].Value));
                                added = true;
                            }
                            else
                            {
                                foreach (var task in tasks)
                                    task.Wait(diff*10, token);
                            }
                        }
                    }
                    ++counter;
                }

                var wholeTime = _detailedRoute.Last().Value;
                var shouldBe = (int)(60 * _distance / Settings.Default.AvegareVelocity);
                if (ost != 0)
                {
                    var r = ((OpenStreetMapProvider) _gmap.MapProvider).GetRoute(routePoints[lastHandledPoint],
                        routePoints.Last(), false, false, 11);
                    wholeTime = _detailedRoute.Last().Value + (int) (60*r.Distance/Settings.Default.AvegareVelocity);
                    _detailedRoute.Add(new KeyValuePair<PointLatLng, int>(routePoints.Last(), wholeTime > shouldBe ? wholeTime : shouldBe));
                }

                Debug.WriteLine(
                    "Обработано {0} объектов через каждые {1}. Итоговое время: {2} минут.\nНа расчёт затрачено {3} секунд",
                    routePoints.Count, diff, wholeTime, (DateTime.Now.Ticks - st)/TimeSpan.TicksPerSecond);
            }
            catch (OperationCanceledException)
            {
                foreach (
                    var task in
                        tasks.Where(
                            task => (task.Status == TaskStatus.Canceled) || (task.Status == TaskStatus.RanToCompletion))
                    )
                {
                    task.Dispose();
                }
            }
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

        private const string TempFilesDir = @"..\..\TempStadiesFiles\";

        private void DrawTransits(object stateInfo)
        {
            var autoEvent = (AutoResetEvent)stateInfo;
            var filename = TempFilesDir + DateTime.Now.ToString("HH:mm_dd.MM.yyyy");
            foreach(var transMarker in _transitMarkers)
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
        private readonly GMapOverlay _middleMarkersOverlay = new GMapOverlay("midMarkers");
        private readonly GMapOverlay _endMarkerOverlay = new GMapOverlay("endMarker");
        private readonly GMapOverlay _routesOverlay = new GMapOverlay("routes");
        private readonly GMapOverlay _markersOverlay = new GMapOverlay("markers");
        private readonly List<KeyValuePair<PointLatLng, int>> _detailedRoute = new List<KeyValuePair<PointLatLng, int>>();
        private readonly List<PointLatLng> _middlePoints = new List<PointLatLng>(); 

        private readonly List<TransitMarker> _transitMarkers = new List<TransitMarker>();

        private CancellationTokenSource _stadiesGenerationCts = new CancellationTokenSource();
        private Task _stadiesGeneration;
        private Action _mTransitsDrawingAction;
    }
}
