using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BachelorLibAPI.Algorithms;
using BachelorLibAPI.Map;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;

namespace ThreadsExperiment
{
    public partial class MapReduceExperiment : Form
    {
        public MapReduceExperiment()
        {
            InitializeComponent();

            CreateGraphic();
        }

        private readonly string _routeDirPath = @"C:\Users\Alex\AppData\Local\GMap.NET\RouteCache\";

        private void RemoveCacheInfo()
        {
            if (!Directory.Exists(_routeDirPath)) return;
            foreach (var fileName in new DirectoryInfo(_routeDirPath).GetFiles().Select(x => x.Name))
            {
                File.Delete(_routeDirPath + fileName);
            }
        }

        private void FillSpecialKmGraphic(DataPointCollection points, PointLatLng start, double km/*, int dictIndex*/)
        {
            var end = LatLongWorker.PointFromStartBearingDistance(start, 90, km);
            var route = ((OpenStreetMapProvider)_gmap.MapProvider).GetRoute(start, end, false, false, 11);
            for (var bearing = 0; bearing < 360 && route.Points.Count == 0; ++bearing)
            {
                end = LatLongWorker.PointFromStartBearingDistance(start, bearing, km);
                route = ((OpenStreetMapProvider)_gmap.MapProvider).GetRoute(start, end, false, false, 11);
            }
            Debug.WriteLine("{0} - {1}", km, route.Distance);

            var routePoints = route.Points;
            var diff = (int)(routePoints.Count / route.Distance);
            while (route.Distance < diff) diff /= 2;
            diff = diff < 1 ? 2 : diff * 2;

            for (var i = 1; i < 40; ++i)
            {
                RemoveCacheInfo();
                var startTime = DateTime.Now.Ticks;
                _map.GenerateStadies(new CancellationToken(), routePoints, diff, i);
                var y = 1000 * (DateTime.Now.Ticks - startTime) / TimeSpan.TicksPerSecond;

                if(InvokeRequired)
                {
                    var i1 = i;
                    Invoke((MethodInvoker) delegate
                    {
                        points.AddXY(i1, y);
                    });
                } 
                else 
                    points.AddXY(i, y);
            }
        }
        
        private void CreateGraphic()
        {
            _gmap = new GMapControl();
            _map = new OpenStreetGreatMap(ref _gmap);

            try
            {
                var pnt = _map.GetPoint("москва");
                var startLatLng = new PointLatLng(pnt.Item1, pnt.Item2);

                Task.Run(() =>
                {
                    FillSpecialKmGraphic(chart1.Series[0].Points, startLatLng, 100);
                    FillSpecialKmGraphic(chart1.Series[1].Points, startLatLng, 500);
                    FillSpecialKmGraphic(chart1.Series[2].Points, startLatLng, 800);
                    FillSpecialKmGraphic(chart1.Series[3].Points, startLatLng, 1200);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private OpenStreetGreatMap _map;
        private GMapControl _gmap;
    }
}
