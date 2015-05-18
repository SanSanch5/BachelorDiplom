using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BachelorLibAPI.Algorithms;
using BachelorLibAPI.Program;
using BachelorLibAPI.Forms;
using BachelorLibAPI.Properties;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Timer = System.Threading.Timer;

namespace BachelorLibAPI.Map
{
    public sealed class OpenStreetGreatMap : IMap
    {
        private PointLatLng? StartPoint { get; set; }
        private PointLatLng? EndPoint { get; set; }

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
            _gmap.Overlays.Add(_polygonsOverlay);
            _gmap.OnMarkerClick += GmapOnMarkerClick;
            _gmap.MouseWheel += GmapOnWheel;

            SetActions();
            var mTransitsDrawing = new Task(_transitsDrawingAction);
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

        private void GmapOnWheel(object sender, MouseEventArgs e)
        {
            var rect = Rectangle.FromLTRB(Cursor.Position.X - 16, Cursor.Position.Y, Cursor.Position.X + 16,
                Cursor.Position.Y + 32);

            lock (_transitMarkers)
            {
                var arr =
                    _transitMarkers.Where(delegate(TransitMarker x)
                    {
                        var pntLoc = _gmap.FromLatLngToLocal(x.Marker.Position);
                        var pnt = new Point((int) pntLoc.X, (int) pntLoc.Y);
                        var pntToScreen = _gmap.PointToScreen(pnt);
                        return rect.Contains(pntToScreen);
                    }).ToArray();
                if (!arr.Any()) return;

                // ReSharper disable once PossibleLossOfFraction
                _gmap.Zoom += e.Delta/120;
                _gmap.Position = arr[0].Marker.Position;
            }
            Cursor.Position =
                _gmap.PointToScreen(new Point((int) _gmap.FromLatLngToLocal(_gmap.Position).X,
                    (int) _gmap.FromLatLngToLocal(_gmap.Position).Y - 10));
        }

        private void GmapOnMarkerClick(object sender, MouseEventArgs mouseEventArgs)
        {
            var menu = new ContextMenuStrip();

            menu.Items.Add(@"Удалить", new Bitmap("..\\..\\Map\\Resources\\cross.png"), (o, args) => RemoveMarkerAdvanced(sender as GMarkerGoogle));
            menu.Items.Add(@"Приблизить", new Bitmap("..\\..\\Map\\Resources\\in.png"), (o, args) =>
            {
                _gmap.Position = ((GMarkerGoogle)sender).Position;
                _gmap.Zoom = 15;
            });
            menu.Show(Cursor.Position);
        }

        public event MarkerRemovedEventHandler MarkerRemove;

        private void OnMarkerRemove(MarkerRemoveEventArgs e)
        {
            var handler = MarkerRemove;
            if (handler != null)
                handler(this, e);
        }

        private void RemoveMarkerAdvanced(GMapMarker marker)
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
            else
            {
                var isMchs = false;
                var isTransit = false;
                var args = new MarkerRemoveEventArgs();
                lock (_mchsMarkers)
                {
                    var m = _mchsMarkers.Where(x => x.Marker == marker).ToList();
                    if (m.Count != 0)
                    {
                        isMchs = true;
                        if (MessageBox.Show(@"Вы уверены, что хотите удалить пункт реагирования?", @"Внимание!", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) != DialogResult.Yes) return;
                        _mchsMarkers.Remove(m.First());
                        args.MarkerType = MarkerType.Staff; 
                        args.Id = m.First().MchsPoint.Id;
                    }
                }
                lock (_transitMarkers)
                {
                    var m = _transitMarkers.Where(x => x.Marker == marker).ToList();
                    if (m.Count != 0)
                    {
                        isTransit = true;
                        if (MessageBox.Show(@"Вы уверены, что хотите удалить перевозку?", @"Внимание!",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) != DialogResult.Yes) return;

                        _transitMarkers.Remove(m.First());
                        args.MarkerType = MarkerType.Transit; 
                        args.Id = m.First().Transit.Id;
                    }
                }
                if (!isTransit && !isMchs) return;
                _markersOverlay.Markers.Remove(marker);
                OnMarkerRemove(args);
            }
        }

        /// <summary>
        /// Заполняю данные о перевозки, которые будут в дальнейшем отображаться при наведении указателя на значок перевозки
        /// Если местоположение не удалось определить, то маркер будет добавле как только его текущее местоположение определится
        /// Перевозки, которые в стадии завершения (основываясь на моём приближении), отображаются чёрным "разгруженным" грузовиком
        /// Находящиеся в пути - жёлтым "загруженным"
        /// </summary>
        /// <param name="transit"></param>
        public void AddTransitMarker(TransitInfo transit)
        {
            var m = new TransitMarker {Transit = transit};
            var pic = transit.IsFinshed
                ? new Bitmap("..\\..\\Map\\Resources\\tractorunitblack.png")
                : new Bitmap("..\\..\\Map\\Resources\\truckyellow.png");

            m.Marker = new GMarkerGoogle(transit.CurrentPlace.Position, new Bitmap(pic, new Size(32, 32)))
            {
                ToolTipText =
                    string.Format(
                        "Перевозка #{0}\nОткуда: {1}\nКуда: {2}\nГруз: {3}, {4} т.\nВодитель: {5}\nНомер телефона: {6}\nАвтомобиль: {7}\nГРЗ: {8}\nТекущее местоположение: {9}",
                        m.Transit.Id, m.Transit.From.Address, m.Transit.To.Address, 
                        m.Transit.Consignment, m.Transit.ConsignmentCapacity, m.Transit.Driver,
                        m.Transit.DriverNumber, m.Transit.Car, m.Transit.Grz, m.Transit.CurrentPlace.Address)
            };

            lock (_transitMarkers)
                _transitMarkers.Add(m);

            //лист тут не при чём оказался.. просто элемент управления, созданный в каком-то потоке, может меняться только!! в этом потоке!
            if(_gmap.InvokeRequired)
                _gmap.Invoke((MethodInvoker)delegate { _markersOverlay.Markers.Add(m.Marker); });
            else _markersOverlay.Markers.Add(m.Marker);
        }

        public void AddMchsMarker(MchsPointInfo mchsPoint)
        {
            var m = new MchsMarkers { MchsPoint = mchsPoint };
            var pic = new Bitmap("..\\..\\Map\\Resources\\fireescape.png");

            m.Marker = new GMarkerGoogle(mchsPoint.Place.Position, new Bitmap(pic, new Size(32, 32)))
            {
                ToolTipText =
                    string.Format(
                        "Пункт реагирования МЧС #{0}\nМожет перевезти обезвреживающиего вещества (всего): {1} т.\nМожет перевезти людей: {2}" +
                        "\nДоступно людей: {3}\nДоступные обезвреживающие вещества:\n{4}",
                        m.MchsPoint.Id, m.MchsPoint.CanSuggest, m.MchsPoint.PeopleReady, m.MchsPoint.PeopleCount,
                        m.MchsPoint.AntiSubstances.Select(x => string.Format("\t{0} {1} т.\n", x.Key, x.Value))
                            .ToList()
                            .Aggregate("", (current, antiSubstance) => current + antiSubstance))
            };

            lock (_mchsMarkers)
                _mchsMarkers.Add(m);

            if (_gmap.InvokeRequired)
                _gmap.Invoke((MethodInvoker)delegate { _markersOverlay.Markers.Add(m.Marker); });
            else _markersOverlay.Markers.Add(m.Marker);
        }

        public void RemoveTransitMarker(int transitId)
        {
            GMapMarker m;
            lock (_transitMarkers)
                m = _transitMarkers.Where(x => x.Transit.Id == transitId).Select(x => x.Marker).ToArray()[0];
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

        public Tuple<double, double> GetLatLong(int x, int y)
        {
            var pnt = _gmap.FromLocalToLatLng(x, y);
            return new Tuple<double, double>(pnt.Lat, pnt.Lng);
        }

        public void SetCurrentViewPoint(PointLatLng pnt)
        {
            _gmap.Position = pnt;
            _gmap.Zoom = 12;
        }

        public void SetDanger(int transitId, PointLatLng pnt)
        {
            lock (_transitMarkers)
            {
                var m = _transitMarkers.Where(x => x.Transit.Id == transitId).ToList().First();
                m.Transit.IsInAccident = true;
                _markersOverlay.Markers.Remove(m.Marker);
                m.Marker = new GMarkerGoogle(pnt, new Bitmap(new Bitmap("..\\..\\Map\\Resources\\danger.png"), new Size(32, 32)))
                {
                    ToolTipText =
                        string.Format(
                            "Перевозка #{0} АВАРИЯ!!!\nОткуда: {1}\nКуда: {2}\nГруз: {3}, {4} т.\nВодитель: {5}\nНомер телефона: {6}\nАвтомобиль: {7}\nГРЗ: {8}\nТекущее местоположение: {9}",
                            m.Transit.Id, m.Transit.From.Address, m.Transit.To.Address,
                            m.Transit.Consignment, m.Transit.ConsignmentCapacity, m.Transit.Driver,
                            m.Transit.DriverNumber, m.Transit.Car, m.Transit.Grz, m.Transit.CurrentPlace.Address)
                };
                _markersOverlay.Markers.Add(m.Marker);
            }
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

            lock(_middlePoints) _middlePoints.Add(mid);
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

            var r = new GMapRoute(_generatedRoutePoints, "New route")
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
            // ReSharper disable once PossibleInvalidOperationException
            var placemarkEnd = GetPlacemark(EndPoint.Value, out st);
            // ReSharper disable once PossibleInvalidOperationException
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
            //if (!task.IsCompleted)
                cts.Cancel();
            /*else */if (task.IsCanceled)
                Debug.WriteLine("Задача отменена");
            //else if (task.IsCompleted || task.IsCanceled)
            //    _stadiesGeneration.Dispose();
        }

        /// <summary>
        /// Собирает точки из подмаршрутов в один маршрут, 
        /// инициирует фоновую задачу расчёта времени пребывания автомобиля в каждой промежуточной стадии
        /// стадии фиксируются в идеале через 1 минуту
        /// </summary>
        public void ConstructShortTrack()
        {
            CancelTask(_stadiesGenerationCts, _stadiesGeneration);

            if (StartPoint == null)
                throw new Exception(@"Не задана начальная точка");
            if (EndPoint == null)
                throw new Exception(@"Не задана конечная точка");

            _generatedRoutePoints.Clear();
            var curStart = StartPoint.Value;
            _distance = 0;
            MapRoute route;
            _detailedRoute.Clear();

            lock (_middlePoints)
            {
                foreach (var middlePoint in _middlePoints)
                {
                    route = ((OpenStreetMapProvider) _gmap.MapProvider).GetRoute(curStart, middlePoint, false, false, 11);
                    if (route == null || route.Points.Count == 0)
                        throw new Exception(Resources.CannotConstructShortTrack);

                    _generatedRoutePoints.AddRange(route.Points);
                    _generatedRoutePoints.RemoveAt(_generatedRoutePoints.Count - 1);
                    curStart = middlePoint;
                    _distance += route.Distance;
                }
            }

            route = ((OpenStreetMapProvider) _gmap.MapProvider).GetRoute(curStart, EndPoint.Value, false, false, 11);
            if (route == null || route.Points.Count == 0)
                throw new Exception(Resources.CannotConstructShortTrack);

            _generatedRoutePoints.AddRange(route.Points);
            _distance += route.Distance;

            _stadiesGenerationCts = new CancellationTokenSource();
            var cancellationToken = _stadiesGenerationCts.Token;
            var diff = (int) (_generatedRoutePoints.Count/_distance);
            while (_distance < diff) diff /= 2;
            _stadiesGeneration = Task.Run(() =>
                GenerateStadies(cancellationToken, _generatedRoutePoints, diff < 1 ? 2 : diff*2, 20), cancellationToken);
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
            return ((OpenStreetMapProvider) _gmap.MapProvider).GetPlacemark(pnt, out st);
        }

        public string GetPlacemark(PointLatLng pnt)
        {
            GeoCoderStatusCode st;
            var placemark = GetPlacemark(pnt, out st);
            return placemark != null ? placemark.Value.Address : @"Метоположение не определено";
        }

        /// <summary>
        /// Инициирует динамическое изменение полигона, отражающего состояние загрязнения со временем
        /// </summary>
        /// <param name="crashInfo"></param>
        public void DrawDangerRegion(CrashInfo crashInfo)
        {
            _crashInfo = crashInfo;
            var radius = Math.Sqrt(2*_crashInfo.Area/Math.PI);
            GetCurrentDangerRegion(radius);
        }

        /// <summary>
        /// Метод для получения полигона точек, задающих текущее положение распространения облака.
        /// </summary>
        private void GetCurrentDangerRegion(double radius)
        {
            // ReSharper disable once PossibleLossOfFraction
            const int delta = 180/Seg;

            var polygon = new List<PointLatLng>();

            var startAngle = (270 + _crashInfo.WindDirection) > 360
                ? _crashInfo.WindDirection - 90
                : (270 + _crashInfo.WindDirection);
            for (var i = 0; i <= Seg; ++i)
            {
                var bearing = startAngle + delta * i;
                if (bearing > 360) bearing -= 360;
                polygon.Add(LatLongWorker.PointFromStartBearingDistance(_crashInfo.Center.Position, bearing, radius));
                Debug.WriteLine("{0} - {1}", radius, LatLongWorker.DistanceFromLatLonInKm(_crashInfo.Center.Position, polygon.Last()));
            }

            var p = new GMapPolygon(polygon, "danger")
            {
                Fill = new SolidBrush(Color.FromArgb(50, Color.Red)),
                Stroke = new Pen(Color.Red, 1)
            };

            _polygonsOverlay.Polygons.Add(p);
        }

        private void GenerateStadiesPart(CancellationToken token, int start, int diff, int increment,
            IReadOnlyList<PointLatLng> routePoints, ICollection<KeyValuePair<PointLatLng, double>> res)
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

                res.Add(new KeyValuePair<PointLatLng, double>(routePoints[i],
                    (60*r.Distance/Settings.Default.AvegareVelocity)));

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
            Debug.WriteLine("Добавлено: {0} - {1}", routePoints[0], 0);

            var results = new List<List<KeyValuePair<PointLatLng, double>>>();
            var tasks = new List<Task>();
            var progressBar = new ProgressBarForm("Расчёт промежуточных стадий...", (routePoints.Count - 1) / diff * 1000 + 1000);

            try
            {
                for (var i = 0; i < threadsCount; ++i)
                {
                    results.Add(new List<KeyValuePair<PointLatLng, double>>());
                    var i1 = i;
                    tasks.Add(
                        Task.Run(() => GenerateStadiesPart(token, i1, diff, diff*threadsCount, routePoints, results[i1]),
                            token));
                }

                var ost = (routePoints.Count - 1)%diff;
                var lastHandledPoint = routePoints.Count - 1 - ost;
                var counter = 0;
                var currentTime = 0.0;

                var detailedRoute = new List<KeyValuePair<PointLatLng, double>>
                {
                    new KeyValuePair<PointLatLng, double>(routePoints[0], 0)
                };
                // жёсткая логика для вытаскивания данных из другого потока
                while (counter < (routePoints.Count - 1)/(diff*threadsCount))
                {
                    for (var i = 0; i < results.Count; i++)
                    {
                        progressBar.Progress(1000);
                        var result = results[i];
                        var added = false;
                        while (!added)
                        {
                            if (tasks[i].IsCompleted)
                            {
                                if (result.Count > counter)
                                    detailedRoute.Add(new KeyValuePair<PointLatLng, double>(result[counter].Key,
                                        currentTime += result[counter].Value));
                                added = true;
                            }
                            else if (result.Count > counter)
                            {
                                detailedRoute.Add(new KeyValuePair<PointLatLng, double>(result[counter].Key,
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
                progressBar.Progress(1000);
                var wholeTime = detailedRoute.Last().Value;
                var shouldBe = (60 * _distance / Settings.Default.AvegareVelocity);
                if (ost != 0)
                {
                    var r = ((OpenStreetMapProvider) _gmap.MapProvider).GetRoute(routePoints[lastHandledPoint],
                        routePoints.Last(), false, false, 11);
                    wholeTime = detailedRoute.Last().Value + (60*r.Distance/Settings.Default.AvegareVelocity);
                    detailedRoute.Add(new KeyValuePair<PointLatLng, double>(routePoints.Last(), wholeTime > shouldBe ? wholeTime : shouldBe));
                }
                else
                {
                    var tmp = detailedRoute.Last();
                    detailedRoute.Remove(tmp);
                    detailedRoute.Add(new KeyValuePair<PointLatLng, double>(tmp.Key, wholeTime > shouldBe ? wholeTime : shouldBe));
                }
                _detailedRoute.AddRange(detailedRoute.Select(x => new KeyValuePair<PointLatLng, int>(x.Key, (int)Math.Round(x.Value))).ToList());

                progressBar.Complete();
                progressBar.Close();

                Debug.WriteLine(
                    "Обработано {0} объектов через каждые {1}. Итоговое время: {2} минут.\nНа расчёт затрачено {3} секунд",
                    routePoints.Count, diff, _detailedRoute.Last().Value, (DateTime.Now.Ticks - st)/TimeSpan.TicksPerSecond);
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
                progressBar.Close();
            }
        }

        private void SetActions()
        {
            _transitsDrawingAction = () =>
            {
                var drawingEvent = new AutoResetEvent(false);
                TimerCallback tcb = DrawTransits;
                var sleep = 60 - DateTime.Now.Second;
                // ReSharper disable once ObjectCreationAsStatement
                new Timer(tcb, drawingEvent, sleep*1000, 60000);

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

        public struct MchsMarkers
        {
            public MchsPointInfo MchsPoint;
            public GMapMarker Marker;
        }

        private const string TempFilesDir = @"..\..\TempStadiesFiles\";

        private void DrawTransits(object stateInfo)
        {
            var autoEvent = (AutoResetEvent) stateInfo;
            var filename = TempFilesDir + DateTime.Now.ToString(Resources.TimeFileFormat);
            if (!File.Exists(filename)) return;

            var transitsForUpdated = new List<KeyValuePair<int, PointLatLng>>();
            using (var sr = new StreamReader(new FileStream(filename, FileMode.Open)))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var splitted = line.Split(';').ToList();
                    transitsForUpdated.Add(new KeyValuePair<int, PointLatLng>(int.Parse(splitted[0]),
                        new PointLatLng(double.Parse(splitted[1]),
                            double.Parse(splitted[2]))));
                }
            }

            try
            {
                lock (_transitMarkers)
                {
                    foreach (var transit in _transitMarkers)
                    {
                        var t = transit;
                        var markerPos = transitsForUpdated.Where(x => x.Key == t.Transit.Id).ToArray();
                        if (!markerPos.Any() || t.Transit.IsInAccident) continue;

                        t.Marker.Position = t.Transit.CurrentPlace.Position = markerPos.First().Value;
                        t.Transit.CurrentPlace.Address = GetPlacemark(t.Marker.Position);
                        t.Marker.ToolTipText =
                            string.Format(
                                "Перевозка #{0}\nОткуда: {1}\nКуда: {2}\nГруз: {3}, {4} т.\nВодитель: {5}\nНомер телефона: {6}\nАвтомобиль: {7}\nГРЗ: {8}\nТекущее местоположение: {9}",
                                t.Transit.Id, t.Transit.From.Address, t.Transit.To.Address,
                                t.Transit.Consignment, t.Transit.ConsignmentCapacity, t.Transit.Driver,
                                t.Transit.DriverNumber, t.Transit.Car, t.Transit.Grz,
                                t.Transit.CurrentPlace.Address);
                    }
                }
                Debug.WriteLine(string.Format("{0} Текущие положения перевозок обновлены!",
                    DateTime.Now.ToString(Resources.TimeFileFormat)));
                autoEvent.Set();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private readonly GMapControl _gmap;
        private readonly GMapOverlay _startMarkerOverlay = new GMapOverlay("startMarker");
        private readonly GMapOverlay _middleMarkersOverlay = new GMapOverlay("midMarkers");
        private readonly GMapOverlay _endMarkerOverlay = new GMapOverlay("endMarker");
        private readonly GMapOverlay _routesOverlay = new GMapOverlay("routes");
        private readonly GMapOverlay _markersOverlay = new GMapOverlay("markers");
        private readonly GMapOverlay _polygonsOverlay = new GMapOverlay("polygons");

        private readonly List<PointLatLng> _generatedRoutePoints = new List<PointLatLng>(); 
        private readonly List<KeyValuePair<PointLatLng, int>> _detailedRoute = new List<KeyValuePair<PointLatLng, int>>();
        private readonly List<PointLatLng> _middlePoints = new List<PointLatLng>(); 

        private readonly List<TransitMarker> _transitMarkers = new List<TransitMarker>();
        private readonly List<MchsMarkers> _mchsMarkers = new List<MchsMarkers>();

        private CancellationTokenSource _stadiesGenerationCts = new CancellationTokenSource();
        private Task _stadiesGeneration;
        private Action _transitsDrawingAction;

        private CrashInfo _crashInfo;
        const int Seg = 20; // 180/20 = 9 - градусов отступ
    }
}
