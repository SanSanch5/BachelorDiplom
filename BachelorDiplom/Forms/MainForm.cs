using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BachelorLibAPI.Data;
using BachelorLibAPI.Map;
using BachelorLibAPI.Program;
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


            _queriesHandler = new QueriesHandler(PgSqlDataHandler.Instance, new OpenStreetGreatMap(ref gmap))
            {
                AnalyseProgress = pbAnalyse
            };
            
            FillMainForm();
        }

        void FillMainForm()
        {
            _queriesHandler.PutTransitsFromDbToMap();
         
            //var pb = new ProgressBarForm("asdasdasdasd", 121231);
            //for(int i = 0; i < 121231; ++i)
            //    pb.Progress();
            //foreach (var city in m_queriesHandler.GetCitiesNames())
            //{
            //    cmbCrashPlace.Items.Add(city);
            //}
        }

        private void AddDriverClick(object sender, EventArgs e)
        {
            var driverAddingForm = new DriverAddForm(_queriesHandler);
            driverAddingForm.Show();
            driverAddingForm.TopMost = true;
        }

        private void AddConsignmentClick(object sender, EventArgs e)
        {
            var consAddingForm = new ConsignmentAddForm(_queriesHandler);
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
            var password = "admin";
            try
            {
                var passwordBox = new PasswordBox();
                passwordBox.ShowDialog();
                if (passwordBox.DialogResult == DialogResult.OK)
                {
                    if (passwordBox.Password == password)
                    {
                        passwordBox.Close();
                        var driverDelForm = new DriverDelForm(_queriesHandler);
                        driverDelForm.Show();
                    }
                    else
                        throw new InvalidExpressionException("Неверный пароль!");
                }
            }
            catch (InvalidExpressionException ex)
            {
                MessageBox.Show(ex.Message, @"Отказано в доступе.");
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
            
        }

        private void DelTransitsBefore(object sender, EventArgs e)
        {
            var password = "admin";
            try
            {
                var passwordBox = new PasswordBox();
                passwordBox.ShowDialog();
                if (passwordBox.DialogResult == DialogResult.OK)
                {
                    if (passwordBox.Password == password)
                    {
                        passwordBox.Close();
                        var transitsDelBeforeForm = new TransitsDelBeforeForm(_queriesHandler);
                        transitsDelBeforeForm.Show();
                    }
                    else
                        throw new InvalidExpressionException("Неверный пароль!");
                }
            }
            catch (InvalidExpressionException ex)
            {
                MessageBox.Show(ex.Message, @"Отказано в доступе.");
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
            var driverInfoForm = new DriverInfoForm(_queriesHandler);
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
            var waybillForm = new WaybillForm(_queriesHandler);
            waybillForm.Show();
        }

        private void EndingStatusClick(object sender, EventArgs e)
        {
            
        }

        private void DangerAnalyseClick(object sender, EventArgs e)
        {
            try
            {
                DateTime since;
                DateTime until;
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

                var place = cmbCrashPlace.Text;
                if(place == "")
                    throw new FormatException("Укажите место аварии!");

                new AnalyseResultsForm(_queriesHandler.AnalyseDanger(since, until, place));
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, @"Все поля обязательны для заполнения!");
                cmbCrashPlace.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Ошибка базы данных.");
            }
        }

        private void LoadTestDataClick(object sender, EventArgs e)
        {
            var password = "test";
            try
            {
                var passwordBox = new PasswordBox();
                passwordBox.ShowDialog();
                if (passwordBox.DialogResult == DialogResult.OK)
                {
                    if (passwordBox.Password == password)
                    {
                        passwordBox.Close();
                        var testsGeneratorForm = new TestsGeneratorForm(_queriesHandler);
                        testsGeneratorForm.Show();
                    }
                    else
                        throw new InvalidExpressionException("Неверный пароль!");
                }
            }
            catch(InvalidExpressionException ex)
            {
                MessageBox.Show(ex.Message, @"Отказано в доступе.");
            }
        }

        private void chbTimeInterval_CheckedChanged(object sender, EventArgs e)
        {
            var b = dtpPrecTime.Enabled;
            dtpPrecTime.Enabled = !b;

            dtpSince.Enabled = b;
            dtpUntil.Enabled = b;
        }

        private void rbtCity_Click(object sender, EventArgs e)
        {
            
        }

        private void rbtRegion_Click(object sender, EventArgs e)
        {
            
        }

        private void fMain_Activated(object sender, EventArgs e)
        {
            pbAnalyse.Visible = false;
        }

        private Point _menuClickPos;

        private void gmap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var adress = _queriesHandler.Map.GetPlacemark(e.X, e.Y);
            MessageBox.Show(adress);
        }


        private void MarkStartPointClick(object sender, EventArgs e)
        {
            _queriesHandler.Map.SetStartPoint(gmap.FromLocalToLatLng(gmap.PointToClient(_menuClickPos).X, gmap.PointToClient(_menuClickPos).Y));
        }

        private void MarkEndPointClick(object sender, EventArgs e)
        {
            _queriesHandler.Map.SetEndPoint(gmap.FromLocalToLatLng(gmap.PointToClient(_menuClickPos).X, gmap.PointToClient(_menuClickPos).Y));       
        }

        private void MarkMiddlePointClick(object sender, EventArgs e)
        {
            _queriesHandler.Map.SetMiddlePoint(gmap.FromLocalToLatLng(gmap.PointToClient(_menuClickPos).X, gmap.PointToClient(_menuClickPos).Y));
        }

        private void GetRouteClick(object sender, EventArgs e)
        {
            ltMainOptions.Visible = false;
            _queriesHandler.Map.ConstructShortTrack();
            ltMainOptions.Visible = true;
        }

        private void mapMenu_Opening(object sender, CancelEventArgs e)
        {
            _menuClickPos = Cursor.Position;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }
    }
}