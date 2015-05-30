using System.Collections.Generic;
using System.Threading;
using BachelorLibAPI.Map;
using GMap.NET;
using GMap.NET.WindowsForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BachelorDiplomaTests
{
    [TestClass]
    public class TestStadiesGeneration
    {
        [TestMethod]
        public void TestEmptyRoute()
        {
            var gmap = new GMapControl();
            var map = new OpenStreetGreatMap(ref gmap);

            map.GenerateStadies(new CancellationTokenSource().Token, new List<PointLatLng>(), 5);

            var res = map.GetShortTrack();
            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void TestOnePointRoute()
        {
            var gmap = new GMapControl();
            var map = new OpenStreetGreatMap(ref gmap);

            map.GenerateStadies(new CancellationTokenSource().Token, new List<PointLatLng> { new PointLatLng(50, 40) }, 5);

            var res = map.GetShortTrack();
            Assert.AreEqual(1, res.Count);
        }

        [TestMethod]
        public void TestDividedByDiff()
        {
            var gmap = new GMapControl();
            var map = new OpenStreetGreatMap(ref gmap);

            map.GenerateStadies(new CancellationTokenSource().Token, new List<PointLatLng>
            {
                new PointLatLng(50, 40),
                new PointLatLng(50, 40.1),
                new PointLatLng(50.1, 40),
                new PointLatLng(50.1, 40.2),
                new PointLatLng(50.1, 40.1)
            }, 2);

            var res = map.GetShortTrack();
            Assert.AreEqual(3, res.Count);

            map.GenerateStadies(new CancellationTokenSource().Token, new List<PointLatLng>
            {
                new PointLatLng(50, 40),
                new PointLatLng(50, 40.1),
                new PointLatLng(50.1, 40),
                new PointLatLng(50.1, 40.1),
                new PointLatLng(50.1, 40),
                new PointLatLng(50.1, 40.1),
                new PointLatLng(50.1, 40),
                new PointLatLng(50.1, 40),
                new PointLatLng(50.1, 40.2),
                new PointLatLng(50.1, 40.1)
            }, 3);

            res = map.GetShortTrack();
            Assert.AreEqual(res.Count, 4);
        }

        [TestMethod]
        public void TestNotDividedByDiff()
        {
            var gmap = new GMapControl();
            var map = new OpenStreetGreatMap(ref gmap);

            map.GenerateStadies(new CancellationTokenSource().Token, new List<PointLatLng>
            {
                new PointLatLng(50, 40),
                new PointLatLng(50, 40.1),
                new PointLatLng(50.1, 40),
                new PointLatLng(50.1, 40.1)
            }, 2);

            var res = map.GetShortTrack();
            Assert.AreEqual(3, res.Count);

            map.GenerateStadies(new CancellationTokenSource().Token, new List<PointLatLng>
            {
                new PointLatLng(50, 40),
                new PointLatLng(50, 40.1),
                new PointLatLng(50.1, 40),
                new PointLatLng(50.1, 40.1),
                new PointLatLng(50.1, 40),
                new PointLatLng(50.1, 40.1),
                new PointLatLng(50.1, 40),
                new PointLatLng(50.1, 40),
                new PointLatLng(50.1, 40.1)
            }, 3);

            res = map.GetShortTrack();
            Assert.AreEqual(res.Count, 4);
        }
    }
}