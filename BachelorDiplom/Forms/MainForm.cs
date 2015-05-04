﻿using System;
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

        private void AddTransitClick(object sender, EventArgs e)
        {

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
            // Connectionstring
            string connStr = "Server=localhost;Port=5432;User Id=postgres;password=4;Database=routing";
            // Name of table in database, you may prepend the schema (e.g. \"public\".\"Roads\")
            string tablename = "at_2po_4pgr";
            // Name of object ID column - MUST be integer and unique!
            string idColumn = "id";

            // Initialize map object
            sharpMap.Map = new SharpMap.Map(new System.Drawing.Size(500, 250));

            //Create layer
            SharpMap.Layers.VectorLayer layRoads = new SharpMap.Layers.VectorLayer("RF map");
            //Set the datasource to the PostgreSQL table
            layRoads.DataSource = new SharpMap.Data.Providers.PostGIS(connStr, tablename, "geom_way", idColumn);
            //layRoads.DataSource = new SharpMap.Data.Providers.ShapeFile("P:\\Downloads\\states_ugl\\states_ugl.shp", true);
            /*
             * Note: Additionally you can specifiy the name of the geometry column to use, in case you 
             * have more than one.
             * layRoads.DataSource = 
                   new SharpMap.Providers.PostGis(connStr, tablename, "geom3857", idColumn); 
             */

            
            //Render map
            //Image img = myMap.GetMap();
            sharpMap.Map.Layers.Add(layRoads);
            sharpMap.Map.ZoomToExtents();
            sharpMap.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
            sharpMap.Refresh();
        }
    }
}