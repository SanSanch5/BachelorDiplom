using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GMap.NET;

namespace BachelorLibAPI.Program
{
    public struct DegMinSec
    {
        private const char D = '°';
        private const char M = '′';
        private const char S = '″';


        public int Degrees;
        public int Minutes;
        public int Seconds;
        public char Direction;

        public DegMinSec(string value)
        {
            var val = Regex.Replace(value, " ", "");
            var deg = val.Split(D);
            Degrees = deg[0] == "" ? 0 : int.Parse(deg[0]);
            var min = deg[1].Split(M);
            Minutes = min[0] == "" ? 0 : int.Parse(min[0]);
            var sec = min[1].Split(S);
            Seconds = sec[0] == "" ? 0 : int.Parse(sec[0]);
            Direction = sec[1] == "" ? ' ' : sec[1][0];
        }
        public override string ToString()
        {
            var deg = Degrees.ToString();
            while (deg.Length != 3) deg = " " + deg;
            var min = Minutes.ToString();
            while (min.Length != 2) min = " " + min; 
            var sec = Seconds.ToString();
            while (sec.Length != 2) sec = " " + sec;
            return deg + min + sec + Direction;
        }
    }
    public struct FullPointDescription
    {
        public string Address;
        public PointLatLng Position;
    }

    public struct CrashInfo
    {
        public int Id;
        public FullPointDescription Center;
        public double Area;
        public DateTime UntilTime;
        public double WindDirection;
        public string Consignment;
        public double ConsignmentCapacity;
    }

    public struct MchsPointInfo
    {
        public int Id;
        public FullPointDescription Place;
        /// сколько вещества может перевезти (сумма вместительностей топлива всех доступных автомобилей)
        public Dictionary<string, double> AntiSubstances; /// название - имеющееся количество (в тоннах)
        public double CanSuggest;
        /// сколько работников может быть доставлено
        public int PeopleReady;
        public int SuperCarCount;
        public int PeopleCount;
        public bool IsAvailable;
    }

    public struct TransitInfo
    {
        public int Id;

        public FullPointDescription From;
        public FullPointDescription To;
        public string Consignment;
        public double ConsignmentCapacity;
        public string Driver;
        public string DriverNumber;
        public string Car;
        public string Grz;
        public FullPointDescription CurrentPlace;
        public int StadiesCount;
        public bool IsFinshed;
        public bool IsInAccident;
    }
}
