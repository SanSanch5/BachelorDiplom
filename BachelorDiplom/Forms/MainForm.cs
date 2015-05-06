using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using BachelorLibAPI.RoadsMap;
using BachelorLibAPI.Data;
using BachelorLibAPI.TestsGenerator;

namespace BachelorLibAPI.Forms
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Предусмотрена возможность расширения: привязка к другой карте и к другой базе данных.
        /// Обработчик запросов отвечает за логику запросов: проверки, исключения, ошибки и т.п.
        /// </summary>
        private QueriesHandler m_queriesHandler;

        public MainForm()
        {
            InitializeComponent();

            m_queriesHandler = new QueriesHandler(PgSqlDataHandler.Instance, new OpenStreetGreatMap(ref gmap));
            m_queriesHandler.AnalyseProgress = pbAnalyse;
            FillMainForm();
        }

        void FillMainForm()
        {
            //foreach (var city in m_queriesHandler.GetCitiesNames())
            //{
            //    cmbCrashPlace.Items.Add(city);
            //}
        }

        private void AddDriverClick(object sender, EventArgs e)
        {
            DriverAddForm driverAddingForm = new DriverAddForm(m_queriesHandler);
            driverAddingForm.Show();
            driverAddingForm.TopMost = true;
        }

        private void AddConsignmentClick(object sender, EventArgs e)
        {
            ConsignmentAddForm consAddingForm = new ConsignmentAddForm(m_queriesHandler);
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
                        DriverDelForm driverDelForm = new DriverDelForm(m_queriesHandler);
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
                        TransitsDelBeforeForm transitsDelBeforeForm = new TransitsDelBeforeForm(m_queriesHandler);
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
            DriverInfoForm driverInfoForm = new DriverInfoForm(m_queriesHandler);
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
            WaybillForm waybillForm = new WaybillForm(m_queriesHandler);
            waybillForm.Show();
        }

        private void EndingStatusClick(object sender, EventArgs e)
        {
            
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
                    new AnalyseResultsForm(m_queriesHandler.AnalyseDanger(since, until, place));
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
                        TestsGeneratorForm testsGeneratorForm = new TestsGeneratorForm(m_queriesHandler);
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
            
        }

        private void rbtRegion_Click(object sender, EventArgs e)
        {
            
        }

        private void fMain_Activated(object sender, EventArgs e)
        {
            pbAnalyse.Visible = false;
        }

        private Point menuClickPos;

        private void gmap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string adress = m_queriesHandler.Map.getPlacemark(e.X, e.Y);
            MessageBox.Show(adress);
        }


        private void markStartPointClick(object sender, EventArgs e)
        {
            m_queriesHandler.Map.setStartPoint(gmap.FromLocalToLatLng(gmap.PointToClient(menuClickPos).X, gmap.PointToClient(menuClickPos).Y));
        }

        private void markEndPointClick(object sender, EventArgs e)
        {
            m_queriesHandler.Map.setEndPoint(gmap.FromLocalToLatLng(gmap.PointToClient(menuClickPos).X, gmap.PointToClient(menuClickPos).Y));       
        }

        private void getRouteClick(object sender, EventArgs e)
        {
            ltMainOptions.Visible = false;
            m_queriesHandler.Map.constructShortTrack();
            ltMainOptions.Visible = true;
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