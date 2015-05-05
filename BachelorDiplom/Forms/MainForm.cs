using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

using BachelorLibAPI.RoadsMap;
using BachelorLibAPI.Data;
using BachelorLibAPI.Data.Course_DB;
using BachelorLibAPI.TestsGenerator;

namespace BachelorLibAPI.Forms
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Предусмотрена возможность расширения: привязка к другой карте и к другой базе данных.
        /// Обработчик запросов отвечает за логику запросов: проверки, исключения, ошибки и т.п.
        /// </summary>
        private QueriesHandler _queriesHandler;

        public MainForm()
        {
            InitializeComponent();

            //_dataHandler = DataHandler.Instance;
            //_map = Graph.Instance;
            _queriesHandler = new QueriesHandler(DataHandler.Instance, Graph.Instance);
            _queriesHandler.AnalyseProgress = pbAnalyse;
            FillMainForm();
        }

        void FillMainForm()
        {
            foreach (var city in _queriesHandler.GetCitiesNames())
            {
                cmbCrashPlace.Items.Add(city);
            }
        }

        private void LoadBuildInMapClick(object sender, EventArgs e)
        {
            _queriesHandler.Map = Graph.Instance;
            MessageBox.Show("Карта успешно загружена. Продолжайте работу.", "Информация");
        }

        private void AddDriverClick(object sender, EventArgs e)
        {
            DriverAddForm driverAddingForm = new DriverAddForm(_queriesHandler);
            driverAddingForm.Show();
            driverAddingForm.TopMost = true;
        }

        private void AddConsignmentClick(object sender, EventArgs e)
        {
            ConsignmentAddForm consAddingForm = new ConsignmentAddForm(_queriesHandler);
            consAddingForm.Show();
        }

        private void AddRegionClick(object sender, EventArgs e)
        {

        }

        private void AddCityClick(object sender, EventArgs e)
        {

        }

        private void DelDriverClick(object sender, EventArgs e)
        {
            string password = "admin";
            try
            {
                PasswordBox passwordBox = new PasswordBox();
                passwordBox.ShowDialog();
                if (passwordBox.DialogResult == DialogResult.OK)
                {
                    if (passwordBox.Password == password)
                    {
                        passwordBox.Close();
                        DriverDelForm driverDelForm = new DriverDelForm(_queriesHandler);
                        driverDelForm.Show();
                    }
                    else
                        throw new InvalidExpressionException("Неверный пароль!");
                }
            }
            catch (InvalidExpressionException ex)
            {
                MessageBox.Show(ex.Message, "Отказано в доступе.");
            }
        }

        private void DelConsignmentClick(object sender, EventArgs e)
        {

        }

        private void DelTransitClick(object sender, EventArgs e)
        {

        }

        private void DelEndedTransits(object sender, EventArgs e)
        {
            string password = "admin";
            try
            {
                PasswordBox passwordBox = new PasswordBox();
                passwordBox.ShowDialog();
                if (passwordBox.DialogResult == DialogResult.OK)
                {
                    if (passwordBox.Password == password)
                    {
                        passwordBox.Close();
                        DialogResult dr = MessageBox.Show("Вы действительно хотите удалить все завершённые перевозки?", "Предупреждение", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                            _queriesHandler.DelEndedTransits();
                    }
                    else
                        throw new InvalidExpressionException("Неверный пароль!");
                }
            }
            catch (InvalidExpressionException ex)
            {
                MessageBox.Show(ex.Message, "Отказано в доступе.");
            }
        }

        private void DelTransitsBefore(object sender, EventArgs e)
        {
            string password = "admin";
            try
            {
                PasswordBox passwordBox = new PasswordBox();
                passwordBox.ShowDialog();
                if (passwordBox.DialogResult == DialogResult.OK)
                {
                    if (passwordBox.Password == password)
                    {
                        passwordBox.Close();
                        TransitsDelBeforeForm transitsDelBeforeForm = new TransitsDelBeforeForm(_queriesHandler);
                        transitsDelBeforeForm.Show();
                    }
                    else
                        throw new InvalidExpressionException("Неверный пароль!");
                }
            }
            catch (InvalidExpressionException ex)
            {
                MessageBox.Show(ex.Message, "Отказано в доступе.");
            }
        }

        private void DelRegionClick(object sender, EventArgs e)
        {

        }

        private void DelCityClick(object sender, EventArgs e)
        {

        }

        private void FindDriverInfoClick(object sender, EventArgs e)
        {
            DriverInfoForm driverInfoForm = new DriverInfoForm(_queriesHandler);
            driverInfoForm.Show();
        }

        private void FindConsignmentInfoClick(object sender, EventArgs e)
        {

        }

        private void FindTransitInfoClick(object sender, EventArgs e)
        {

        }

        private void FindRegionInfoClick(object sender, EventArgs e)
        {

        }

        private void FindCityInfoClick(object sender, EventArgs e)
        {

        }

        private void NewWaybillClick(object sender, EventArgs e)
        {
            WaybillForm waybillForm = new WaybillForm(_queriesHandler);
            waybillForm.Show();
        }

        private void EndingStatusClick(object sender, EventArgs e)
        {
            EndingStatusForm endingStatusForm = new EndingStatusForm(_queriesHandler);
            endingStatusForm.Show();
        }

        private void DangerAnalyseClick(object sender, EventArgs e)
        {
            try
            {
                DateTime since = new DateTime();
                DateTime until = new DateTime();
                if(dtpPrecTime.Enabled)
                {
                    since = dtpPrecTime.Value;
                    until = since;
                }
                else
                {
                    since = dtpSince.Value;
                    until = dtpUntil.Value;
                }

                string place = cmbCrashPlace.Text;
                if(place == "")
                    throw new FormatException("Укажите место аварии!");

                AnalyseResultsForm analyseReturnForm = 
                    new AnalyseResultsForm(_queriesHandler.AnalyseDanger(since, until, place));
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Все поля обязательны для заполнения!");
                cmbCrashPlace.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка базы данных.");
            }
        }

        private void LoadTestDataClick(object sender, EventArgs e)
        {
            string password = "test";
            try
            {
                PasswordBox passwordBox = new PasswordBox();
                passwordBox.ShowDialog();
                if (passwordBox.DialogResult == DialogResult.OK)
                {
                    if (passwordBox.Password == password)
                    {
                        passwordBox.Close();
                        TestsGeneratorForm testsGeneratorForm = new TestsGeneratorForm(_queriesHandler);
                        testsGeneratorForm.Show();
                    }
                    else
                        throw new InvalidExpressionException("Неверный пароль!");
                }
            }
            catch(InvalidExpressionException ex)
            {
                MessageBox.Show(ex.Message, "Отказано в доступе.");
            }
        }

        private void chbTimeInterval_CheckedChanged(object sender, EventArgs e)
        {
            bool b = dtpPrecTime.Enabled;
            dtpPrecTime.Enabled = !b;

            dtpSince.Enabled = b;
            dtpUntil.Enabled = b;
        }

        private void rbtCity_Click(object sender, EventArgs e)
        {
            cmbCrashPlace.Items.Clear();
            List<string> cities = _queriesHandler.GetCitiesNames();
            foreach (var city in cities)
                cmbCrashPlace.Items.Add(city);
            cmbCrashPlace.Text = "";
        }

        private void rbtRegion_Click(object sender, EventArgs e)
        {
            cmbCrashPlace.Items.Clear();
            List<string> regions = _queriesHandler.GetRegionsNames();
            foreach (var reg in regions)
                cmbCrashPlace.Items.Add(reg);
            cmbCrashPlace.Text = "";
        }

        private void fMain_Activated(object sender, EventArgs e)
        {
            pbAnalyse.Visible = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            openStreetMapToolStripMenuItem_Click(sender, e);
            gmap.SetPositionByKeywords("Moscow, Russian");
            gmap.DragButton = MouseButtons.Left;
            gmap.DisableFocusOnMouseEnter = true;
            gmap.RoutesEnabled = true;
            gmap.Overlays.Clear();
            gmap.Overlays.Add(routesOverlay);
            gmap.Overlays.Add(startMarkerOverlay);
            gmap.Overlays.Add(endMarkerOverlay);
        }

        private Placemark? getPlacemark(PointLatLng pnt, out GeoCoderStatusCode st)
        {
            Placemark? plc = null;
            st = GeoCoderStatusCode.G_GEO_BAD_KEY;

            if (openStreetMapToolStripMenuItem.Checked)
                plc = ((OpenStreetMapProvider)gmap.MapProvider).GetPlacemark(pnt, out st);
            
            return plc;
        }

        private void gmap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GeoCoderStatusCode st;
            Placemark? plc = getPlacemark(gmap.FromLocalToLatLng(e.X, e.Y), out st);
            if (st == GeoCoderStatusCode.G_GEO_SUCCESS && plc != null)
            {
                Debug.WriteLine("Accuracy: " + plc.Value.Accuracy + ", " + plc.Value.ThoroughfareName + ", " + plc.Value.Neighborhood + ", " + plc.Value.HouseNo 
                    + ", PostalCodeNumber: " + plc.Value.PostalCodeNumber);
            }
        }

        private void openStreetMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gmap.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gmap.Zoom = 11;
        }

        private PointLatLng start, end;
        private Point menuClickPos;
        private GMapOverlay startMarkerOverlay = new GMapOverlay("startMarkers");
        private GMapOverlay endMarkerOverlay = new GMapOverlay("endMarkers");
        private GMapOverlay routesOverlay = new GMapOverlay("routes");

        private void markStartPointClick(object sender, EventArgs e)
        {
            start = gmap.FromLocalToLatLng(gmap.PointToClient(menuClickPos).X, gmap.PointToClient(menuClickPos).Y);
            Bitmap pic = new Bitmap("..\\..\\Map\\Resources\\truckyellow.png");
            GMarkerGoogle marker = new GMarkerGoogle(start, new Bitmap(pic, new Size(32, 32)));
            startMarkerOverlay.Clear();
            startMarkerOverlay.Markers.Add(marker);
        }

        private void markEndPointClick(object sender, EventArgs e)
        {
            end = gmap.FromLocalToLatLng(gmap.PointToClient(menuClickPos).X, gmap.PointToClient(menuClickPos).Y);
            Bitmap pic = new Bitmap("..\\..\\Map\\Resources\\tractorunitblack.png");
            GMarkerGoogle marker = new GMarkerGoogle(end, new Bitmap(pic, new Size(32, 32)));
            endMarkerOverlay.Clear();
            endMarkerOverlay.Markers.Add(marker);
            
        }

        private void getRouteClick(object sender, EventArgs e)
        {
            MapRoute route = ((OpenStreetMapProvider)gmap.MapProvider).GetRoute(start, end, false, false, 11);
            if(route == null || route.Points.Count == 0)
            {
                MessageBox.Show("Не удалось построить маршрут.");
                return;
            }

            GMapRoute r = new GMapRoute(route.Points, "My route");
            r.Stroke.Width = 5;
            r.Stroke.Color = Color.Black;

            routesOverlay.Routes.Add(r);
            gmap.ZoomAndCenterRoute(r);

            ltMainOptions.Visible = false;
            if(MessageBox.Show("Использовать этот маршрут? Вы можете добавить промежуточные точки для уточнения", "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                startMarkerOverlay.Clear();
                endMarkerOverlay.Clear();
                // сохранить
            }
            
            ltMainOptions.Visible = true;
            routesOverlay.Clear();
        }

        private void mapMenu_Opening(object sender, CancelEventArgs e)
        {
            menuClickPos = Cursor.Position;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }
    }
}