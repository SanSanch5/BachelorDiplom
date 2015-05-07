using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

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
            m_gmap.Zoom = 11;
            m_gmap.SetPositionByKeywords("Moscow, Russian");
            m_gmap.DragButton = MouseButtons.Left;
            m_gmap.DisableFocusOnMouseEnter = true;
            m_gmap.RoutesEnabled = true;
            m_gmap.Overlays.Clear();
            m_gmap.Overlays.Add(m_routesOverlay);
            m_gmap.Overlays.Add(m_markersOverlay);
            m_gmap.Overlays.Add(m_startMarkerOverlay);
            m_gmap.Overlays.Add(m_endMarkerOverlay);

            m_stadiesGenerationAction = (object _route) =>
                {
                    m_stadiesGenerationThread = Thread.CurrentThread;

                    m_detailedRoute = new List<KeyValuePair<PointLatLng, int>>();
                    List<PointLatLng> route = ((MapRoute)_route).Points;

                    PointLatLng start = route[0];
                    m_detailedRoute.Add(new KeyValuePair<PointLatLng, int>(start, 0));
                    Debug.WriteLine("Добавлено: {0} - {1}", start, 0);

                    for(int i = 100; i < route.Count; i += 100)
                    {
                        MapRoute r = ((OpenStreetMapProvider)m_gmap.MapProvider).GetRoute(/*route[i-10]*/start, route[i], false, false, 11);
                        m_detailedRoute.Add(new KeyValuePair<PointLatLng, int>(route[i], (int)(60 * r.Distance / Properties.Settings.Default.AvegareVelocity)));
                            /*m_detailedRoute.Last().Value + (int)(60*r.Distance / Properties.Settings.Default.AvegareVelocity)));*/
                    }
                    
                    int ost = (route.Count-1) % 100;
                    if(ost != 0)
                        m_detailedRoute.Add(new KeyValuePair<PointLatLng, int>(route.Last(), (int)((MapRoute)_route).Distance));
                            //m_detailedRoute.Last().Value + (int)(60 * (
                            //(OpenStreetMapProvider)m_gmap.MapProvider).GetRoute(route[route.Count - ost], route.Last(), false, false, 11)
                            //.Distance / Properties.Settings.Default.AvegareVelocity)));
                    
                    Debug.WriteLine("Обработано {0} объектов через каждые 100. Итоговое время: {1} минут", route.Count, (int)((MapRoute)_route).Distance);
                };
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
            m_startMarkerOverlay.Clear();
            m_startMarkerOverlay.Markers.Add(marker);
        }
        public void setEndPoint(PointLatLng _end)
        {
            m_end = _end;
            Bitmap pic = new Bitmap("..\\..\\Map\\Resources\\tractorunitblack.png");
            GMarkerGoogle marker = new GMarkerGoogle(_end, new Bitmap(pic, new Size(32, 32)));
            m_endMarkerOverlay.Clear();
            m_endMarkerOverlay.Markers.Add(marker);
        }

        public void constructShortTrack()
        {
            if (m_stadiesGeneration != null)
                m_stadiesGenerationThread.Abort();

            MapRoute route = ((OpenStreetMapProvider)m_gmap.MapProvider).GetRoute(m_start, m_end, false, false, 11);
            if (route == null || route.Points.Count == 0)
            {
                MessageBox.Show("Не удалось построить маршрут.");
                return;
            }

            GMapRoute r = new GMapRoute(route.Points, "My route");
            //m_route = route.Points;
            r.Stroke.Width = 5;
            r.Stroke.Color = Color.Black;

            m_routesOverlay.Routes.Add(r);
            m_gmap.ZoomAndCenterRoute(r);

            if (MessageBox.Show(route.Distance.ToString("N2") + " км.\n" + "Использовать этот маршрут для новой перевозки?\nВы можете добавить промежуточные точки для уточнения", 
                "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                m_startMarkerOverlay.Clear();
                m_endMarkerOverlay.Clear();
                    // сохранить
                    // запустить таском генерацию всех подмаршрутов...
                m_stadiesGeneration = new Task(m_stadiesGenerationAction, route);
                m_stadiesGeneration.Start();
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

        private GMapControl m_gmap;
        private GMapOverlay m_startMarkerOverlay = new GMapOverlay("startMarker");
        private GMapOverlay m_endMarkerOverlay = new GMapOverlay("endMarker");
        private GMapOverlay m_routesOverlay = new GMapOverlay("routes");
        private GMapOverlay m_markersOverlay = new GMapOverlay("markers");
        private PointLatLng m_start, m_end;
        List<KeyValuePair<PointLatLng, int>> m_detailedRoute;

        private Task m_stadiesGeneration;
        private Action<object> m_stadiesGenerationAction;
        private Thread m_stadiesGenerationThread;
    }
}
