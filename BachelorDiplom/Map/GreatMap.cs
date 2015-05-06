using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

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
            MapRoute route = ((OpenStreetMapProvider)m_gmap.MapProvider).GetRoute(m_start, m_end, false, false, 11);
            if (route == null || route.Points.Count == 0)
            {
                MessageBox.Show("Не удалось построить маршрут.");
                return;
            }

            GMapRoute r = new GMapRoute(route.Points, "My route");
            m_route = route.Points;
            r.Stroke.Width = 5;
            r.Stroke.Color = Color.Black;

            m_routesOverlay.Routes.Add(r);
            m_gmap.ZoomAndCenterRoute(r);

            if (MessageBox.Show("Использовать этот маршрут для новой перевозки?\nВы можете добавить промежуточные точки для уточнения", "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                m_startMarkerOverlay.Clear();
                m_endMarkerOverlay.Clear();
                // сохранить
                // запустить таском генерацию всех подмаршрутов...
            }

            m_routesOverlay.Clear();
        }

        public List<KeyValuePair<PointLatLng, int>> getShortTrack(PointLatLng start, PointLatLng goal, int startValue = 0)
        {
            List<KeyValuePair<PointLatLng, int>> res = new List<KeyValuePair<PointLatLng,int>>();



            return res;
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
        private List<PointLatLng> m_route;
    }
}
