using System;
using System.Collections.Generic;
using GMap.NET;

namespace BachelorLibAPI.Program
{
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
    }

    public struct TransitInfo
    {
        public int Id;

        public FullPointDescription From;
        public FullPointDescription To;
        public string Consignment;
        public string Driver;
        public string DriverNumber;
        public string Car;
        public string Grz;
        public FullPointDescription CurrentPlace;
        public bool IsFinshed;
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

    public struct DriverInfoType
    {
        public int Id;
        public string Surname;
        public string Name;
        public string MName;
        public List<string> Numbers;
        public string ConsName;
        public int DangerDegree;
        public string StartLocation;
        public DateTime Start;
        public string GoalLocation;
        public DateTime ProbableArrival;
        public DateTime Arrival;
        public string ProbableLocation;
        public bool Status;
    }

    
}
