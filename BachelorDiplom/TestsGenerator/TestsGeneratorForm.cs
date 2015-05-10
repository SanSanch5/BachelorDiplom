using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BachelorLibAPI.Program;

namespace BachelorLibAPI.TestsGenerator
{
    public partial class TestsGeneratorForm : Form
    {
        private QueriesHandler _queriesHandler;
        private readonly MyDataReader _data;
        private readonly Random _random = new Random();
        private const int DriversCount = 1200;
        private const int TransitsCount = 1000;
        private int _datesCount = 100;

        private List<string> _cities;
        private List<string> _consignments;
        private List<string> _numbers;
        private List<string> _dates;

        public TestsGeneratorForm(QueriesHandler qh)
        {
            InitializeComponent();
            _queriesHandler = qh;

            _data = new MyDataReader();
            _data.LoadFromFiles();

            //cities = _queriesHandler.GetCitiesNames();
            _consignments = QueriesHandler.GetConsignmentsNames();
            _numbers = _queriesHandler.GetNumbers();
            _dates = new List<string>();

            pbTransits.Minimum = 0;
            pbTransits.Maximum = TransitsCount;
            pbTransits.Value = 0;

            GenerateDates();
        }

        private void GenerateDates()
        {
            var dt = DateTime.Now;

            for (var i = 0; i < _datesCount; ++i)
            {
                var year = dt.Year.ToString();
                var month = _random.Next(dt.Month - 1, dt.Month + 1).ToString();
                var day = _random.Next(1, 28).ToString();
                var hour = _random.Next(0, 23).ToString();
                var minute = _random.Next(0, 59).ToString();
                var second = _random.Next(0, 59).ToString();

                _dates.Add(year + " " + month + " " + day + " " + hour + " " + minute + " " + second);
            }
        }

        private void AddDrivers()
        {
            for (var i = 0; i < DriversCount; ++i)
            {
                var name = _data.Names.Random(_random);
                var midName = _data.Midnames.Random(_random);
                var lastName = _data.Lastnames.Random(_random);
                var inum1 = 9999999999 - i;
                var inum2 = 4999999999 - i;
                var num1 = inum1.ToString();
                var num2 = inum2.ToString();

                _queriesHandler.AddNewDriver(lastName, name, midName, num1, num2);
            }
        }

        private void AddTransits()
        {
            for (var i = 0; i < TransitsCount; ++i)
            {               
                var num = _numbers.Random(_random);
                var cons = _consignments.Random(_random);

                var citiesLst = new List<string>();
                var citiesCount = _random.Next(2, 5);
                for(var j = 0; j < citiesCount; ++j)
                {
                    var city = _cities.Random(_random);
                    while(citiesLst.Contains(city))
                        city = _cities.Random(_random);
                    citiesLst.Add(city);
                }

                var startDate = _dates.Random(_random);
                var date = startDate.Split().Where(x => x != "").Select(x => int.Parse(x)).ToArray();
                var start = new DateTime(date[0], date[1], date[2], date[3], date[4], date[5]);

                //_queriesHandler.AddNewWaybill(num, cons, citiesLst, start);

                ++pbTransits.Value;
            }
        }

        private void GenAndAddClick(object sender, EventArgs e)
        {
            try
            {
                //AddDrivers();
                AddTransits();
                MessageBox.Show("Данные загружены", "Информация");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
