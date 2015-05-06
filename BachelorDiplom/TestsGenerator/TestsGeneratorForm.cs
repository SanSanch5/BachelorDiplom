using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BachelorLibAPI.TestsGenerator
{
    public partial class TestsGeneratorForm : Form
    {
        private QueriesHandler _queriesHandler;
        private readonly MyDataReader _data;
        private readonly Random random = new Random();
        private const int _driversCount = 1200;
        private const int _transitsCount = 1000;
        private int _datesCount = 100;

        private List<string> cities;
        private List<string> consignments;
        private List<string> numbers;
        private List<string> dates;

        public TestsGeneratorForm(QueriesHandler _qh)
        {
            InitializeComponent();
            _queriesHandler = _qh;

            _data = new MyDataReader();
            _data.LoadFromFiles();

            //cities = _queriesHandler.GetCitiesNames();
            consignments = _queriesHandler.GetConsignmentsNames();
            numbers = _queriesHandler.GetNumbers();
            dates = new List<string>();

            pbTransits.Minimum = 0;
            pbTransits.Maximum = _transitsCount;
            pbTransits.Value = 0;

            GenerateDates();
        }

        private void GenerateDates()
        {
            DateTime dt = DateTime.Now;

            for (int i = 0; i < _datesCount; ++i)
            {
                string year = dt.Year.ToString();
                string month = random.Next(dt.Month - 1, dt.Month + 1).ToString();
                string day = random.Next(1, 28).ToString();
                string hour = random.Next(0, 23).ToString();
                string minute = random.Next(0, 59).ToString();
                string second = random.Next(0, 59).ToString();

                dates.Add(year + " " + month + " " + day + " " + hour + " " + minute + " " + second);
            }
        }

        private void AddDrivers()
        {
            for (int i = 0; i < _driversCount; ++i)
            {
                string name = _data.Names.Random(random);
                string midName = _data.Midnames.Random(random);
                string lastName = _data.Lastnames.Random(random);
                long inum1 = 9999999999 - i;
                long inum2 = 4999999999 - i;
                string num1 = inum1.ToString();
                string num2 = inum2.ToString();

                _queriesHandler.AddNewDriver(lastName, name, midName, num1, num2);
            }
        }

        private void AddTransits()
        {
            for (int i = 0; i < _transitsCount; ++i)
            {               
                string num = numbers.Random(random);
                string cons = consignments.Random(random);

                List<string> citiesLst = new List<string>();
                int citiesCount = random.Next(2, 5);
                for(int j = 0; j < citiesCount; ++j)
                {
                    string city = cities.Random(random);
                    while(citiesLst.Contains(city))
                        city = cities.Random(random);
                    citiesLst.Add(city);
                }

                string startDate = dates.Random(random);
                int [] date = startDate.Split().Where(x => x != "").Select(x => int.Parse(x)).ToArray();
                DateTime start = new DateTime(date[0], date[1], date[2], date[3], date[4], date[5]);

                _queriesHandler.AddNewWaybill(num, cons, citiesLst, start);

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
