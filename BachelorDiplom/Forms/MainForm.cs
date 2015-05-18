using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.XPath;
using BachelorLibAPI.Algorithms;
using BachelorLibAPI.Data;
using BachelorLibAPI.Map;
using BachelorLibAPI.Program;
using GMap.NET;

namespace BachelorLibAPI.Forms
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Предусмотрена возможность расширения: привязка к другой карте и к другой базе данных.
        /// Обработчик запросов отвечает за логику запросов: проверки, исключения, ошибки и т.п.
        /// </summary>
        private readonly QueriesHandler _queriesHandler;
        private static readonly Bitmap PicOk = new Bitmap(@"..\..\Resources\Pictures\ok.png");
        private static readonly Bitmap PicNotOk = new Bitmap(@"..\..\Resources\Pictures\not_ok.png");

        public MainForm()
        {
            InitializeComponent();

            _queriesHandler = new QueriesHandler(PgSqlDataHandler.Instance, new OpenStreetGreatMap(ref gmap));
            QueriesHandler.DeleteTransitStadiesOlderThenYesterday();

            FillMainForm();

            var p1 = new PointLatLng(LatLongWorker.DegMinSecToDecimalDegree(new DegMinSec("53°19′14″N"), true), 
                LatLongWorker.DegMinSecToDecimalDegree(new DegMinSec("001°43′47″W"), false));
            var p2 = new PointLatLng(LatLongWorker.DegMinSecToDecimalDegree(new DegMinSec("53°11′18″N"), true), 
                LatLongWorker.DegMinSecToDecimalDegree(new DegMinSec("000°08′00″E"), false));
            //var p2 = LatLongWorker.PointFromStartBearingDistance(p1, 345, 125.8);
            var dist = LatLongWorker.DistanceFromLatLonInKm(p1, p2);
        }

        private void FillMainForm()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)FillMainForm);
                return;
            }
            

            Cursor.Current = Cursors.WaitCursor;
            _queriesHandler.PutTransitsFromDbToMap();
            _queriesHandler.PutMchsPointsFromDbToMap();
            Cursor.Current = Cursors.Default;
        }

        private void NewWaybillClick(object sender, EventArgs e)
        {
            var waybillForm = new WaybillForm(_queriesHandler);
            waybillForm.Show();
        }

        private void DangerAnalyseClick(object sender, EventArgs e)
        {
            
        }

        private Point _menuClickPos;

        private void gmap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var pnt = _queriesHandler.Map.GetLatLong(e.X, e.Y);
                var dmsLat = LatLongWorker.DecimalDegreeToDegMinSec(pnt.Item1, true);
                var dmsLong = LatLongWorker.DecimalDegreeToDegMinSec(pnt.Item2, false);
                edtLat.Text = dmsLat.ToString();
                edtLong.Text = dmsLong.ToString();
                edtCrashPlace.Text = _queriesHandler.Map.GetPlacemark(e.X, e.Y);
                picCheck.Image = new Bitmap(PicOk, new Size(16, 16));
            }
            catch (PlacemarkGettingException ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void mapMenu_Opening(object sender, CancelEventArgs e)
        {
            _menuClickPos = Cursor.Position;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private void btnSetCrashPlace_Click(object sender, EventArgs e)
        {
            var pnt = new PointLatLng(LatLongWorker.DegMinSecToDecimalDegree(new DegMinSec(edtLat.Text), true), 
                LatLongWorker.DegMinSecToDecimalDegree(new DegMinSec(edtLong.Text), false));
            _queriesHandler.SetCurrentPointOfView(pnt.Lat, pnt.Lng);
            if (MessageBox.Show(@"Анализировать опасность в этой точке?", @"Внимание", MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
                _queriesHandler.AnalyseDanger(dtpPrecTime.Value, new FullPointDescription
                {
                    Address = edtCrashPlace.Text,
                    Position = pnt
                });
        }
    }
}