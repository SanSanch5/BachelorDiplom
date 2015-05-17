using System;
using BachelorLibAPI.Program;
using GMap.NET;

namespace BachelorLibAPI.Algorithms
{
    public static class LatLongWorker
    {
        private const int R = 6371; // Radius of the earth in km

        public static PointLatLng PointFromStartBearingDistance(PointLatLng start, double bearing, double distance)
        {
            var lat1 = Deg2Rad(start.Lat);
            var lon1 = Deg2Rad(start.Lng);
            var b = Deg2Rad(bearing);
            var res = new PointLatLng
            {
                Lat = Math.Asin(Math.Sin(lat1)*Math.Cos(distance/R) +
                                         Math.Cos(lat1)*Math.Sin(distance/R)*Math.Cos(b))
            };
            res.Lng = lon1 + Math.Atan2(Math.Sin(b)*Math.Sin(distance/R)*Math.Cos(lat1),
                    Math.Cos(distance / R) - Math.Sin(lat1) * Math.Sin(res.Lat));

            return new PointLatLng(Rad2Deg(res.Lat), Rad2Deg(res.Lng));
        }

        private static double DistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
        {
            var dLat = Deg2Rad(lat2 - lat1); // Deg2Rad below
            var dLon = Deg2Rad(lon2 - lon1);
            var a =
                Math.Sin(dLat/2)*Math.Sin(dLat/2) +
                Math.Cos(Deg2Rad(lat1))*Math.Cos(Deg2Rad(lat2))*
                Math.Sin(dLon/2)*Math.Sin(dLon/2)
                ;
            var c = 2*Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R*c; // Distance in km
            return d;
        }

        public static double DistanceFromLatLonInKm(PointLatLng p1, PointLatLng p2)
        {
            return DistanceFromLatLonInKm(p1.Lat, p1.Lng, p2.Lat, p2.Lng);
        }

        public static DegMinSec DecimalDegreeToDegMinSec(double degree, bool isLatitude)
        {
            var b = degree > 0;
            degree = Math.Abs(degree);
            var res = new DegMinSec {Degrees = (int) Math.Truncate(degree)};
            var minsec = (degree - res.Degrees)*60;
            res.Minutes = (int)Math.Truncate(minsec);
            res.Seconds = (int) Math.Truncate((minsec - res.Minutes)*60);
            res.Direction = b && isLatitude ? 'N' : b ? 'W' : isLatitude ? 'S' : 'E';
            return res;
        }
        public static double DegMinSecToDecimalDegree(DegMinSec deg, bool isLatitude)
        {
            var sign = deg.Direction == 'S' && isLatitude || deg.Direction == 'E' && !isLatitude ? -1 : 1; 
            var res = deg.Degrees + (deg.Minutes + (double)deg.Seconds/60)/60;
            var d = Math.Truncate(res);
            return (res - d + (int)d % 360)*sign;
        }

        private static double Rad2Deg(double rad)
        {
            return rad*180/Math.PI;
        }
        private static double Deg2Rad(double deg)
        {
            return deg*(Math.PI/180);
        }
    }
}
