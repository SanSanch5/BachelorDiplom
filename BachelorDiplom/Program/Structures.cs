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
            var deg = value.Split(D);
            Degrees = int.Parse(Regex.Replace(deg[0], " ", ""));
            var min = deg[1].Split(M);
            Minutes = int.Parse(Regex.Replace(min[0], " ", ""));
            var sec = min[1].Split(S);
            Seconds = int.Parse(Regex.Replace(sec[0], " ", ""));
            Direction = sec[1][0];
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
        public FullPointDescription Center;
        public double Area;
        public DateTime StartTime;
        public DateTime SpreadingTime;
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
        public int CanSuggest;
        /// сколько работников может быть доставлено
        public int PeopleReady;
        public int PeopleCount;
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

    public struct AnalyseReturnType
    {
        public string ConsName;
        public int DangerDegree;
        public string AfterCrash;
        public string DriversName;
        public string DriversSurname;
        public List<string> DriversNumbers;
        public string City;
        public DateTime Location;
    }
}
