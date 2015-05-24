using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BachelorLibAPI.Program;

namespace BachelorLibAPI.Forms
{
    public partial class WaybillForm : Form
    {
        private QueriesHandler QueriesHandler { get; set; }
        private readonly List<string> _consNames;
        private readonly List<string> _grzList;
        private static readonly Bitmap PicOk = new Bitmap(@"..\..\Resources\Pictures\ok.png");
        private static readonly Bitmap PicNotOk = new Bitmap(@"..\..\Resources\Pictures\not_ok.png");

        public WaybillForm(QueriesHandler qh)
        {
            InitializeComponent();
            var screen = Screen.PrimaryScreen;
            Left = 25;
            Top = screen.WorkingArea.Top + 150;

            QueriesHandler = qh;
            _consNames = QueriesHandler.GetConsignmentsNames();
            _grzList = QueriesHandler.GetGrzList();

            if(QueriesHandler.HasMiddlePoints())
                MessageBox.Show(@"Отмеченные промежуточные точки уже учтены.", @"Информация");

            FillForm();
        }

        private void FillForm()
        {
            foreach (var cons in _consNames)
                cmbCons.Items.Add(cons);
            
            foreach (var grz in _grzList)
                cmbGRZ.Items.Add(grz);
        }

        private void AddNewWaybill(object sender, EventArgs e)
        {
            try
            {
                var num = Regex.Replace(edtDriverPhoneNumber.Text, "[^0-9]", "");
                var consName = cmbCons.Text;
                var driverName = cmbDriverName.Text;
                var grz = cmbGRZ.Text;
                var cap = edtConsCapacity.Text;

                if ((num == "" || num.Length != 10) || consName == "" || driverName == "" || grz == "" || cap == "")
                    throw new FormatException("Звёздочкой (*) отмечены поля для обязательного заполнения!");
                if (!_consNames.Contains(consName))
                    throw new FormatException("Выберите груз.");
                if (!_grzList.Contains(grz))
                    throw new FormatException("Выберите номер (ГРЗ).");

                if (edtMid.Text != "")
                {
                    MessageBox.Show(@"Добавьте введённую промежуточную точку или сотрите её из поля ввода", @"Внимание!");
                    return;
                }

                WindowState = FormWindowState.Minimized;
                if (!QueriesHandler.CheckBeforeAdding()) return;
                QueriesHandler.AddNewWaybill(driverName, num, grz, consName, double.Parse(cap), dtpStart.Value);
                MessageBox.Show(@"Новая перевозка зарегистрирована.", @"Информация");
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, @"Предупреждение");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Ошибка");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                _needUpdate = false;
            }
        }

        private void AddMoreMidCities(object sender, EventArgs e)
        {
            QueriesHandler.SetMiddlePoint(edtMid.Text);
            edtMid.Text = "";
            CheckAndConstructRoute();
        }

        private bool _hasStart;
        private bool _hasEnd;

        private bool _needUpdate = true;
        private void CheckAndConstructRoute()
        {
            if (_hasStart && _hasEnd)
            {
                try
                {
                    btnNewWaybill.Enabled = true;
                    if (_needUpdate)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        QueriesHandler.ConstructShortTrack();
                        Cursor.Current = Cursors.Default;
                    }
                    else _needUpdate = true;
                }
                catch (Exception e)
                {
                    _needUpdate = false;
                    MessageBox.Show(e.Message);
                }
            }
            else
                btnNewWaybill.Enabled = false;
        }

        private void OnFocusOut(object sender, EventArgs e)
        {
            var textBox = (TextBox) sender;
            var txt = textBox.Text;
            if (txt == "") return;

            Cursor.Current = Cursors.WaitCursor;
            var hasAdress = QueriesHandler.CheckAdress(txt);
            //textBox.Text = txt;

            try
            {
                ttForOk.SetToolTip(textBox, hasAdress ? QueriesHandler.GetCorrectAdress(txt) : txt);

                var pic = hasAdress
                    ? new Bitmap(PicOk, new Size(16, 16))
                    : new Bitmap(PicNotOk, new Size(16, 16));
                var ttText = hasAdress ? "Адрес найден на карте" : "Адрес не найден на карте, проверьте и исправьте";
                if (textBox == edtFrom)
                {
                    picFrom.Image = pic;
                    ttForOk.SetToolTip(picFrom, ttText);
                    _hasStart = hasAdress;
                    if (!_hasStart) return;
                    QueriesHandler.SetStartPoint(txt);
                    CheckAndConstructRoute();
                }
                else if (textBox == edtMid)
                {
                    picMid.Image = pic;
                    ttForOk.SetToolTip(picMid, ttText);
                    btnMoreMid.Enabled = hasAdress;
                }
                else if (textBox == edtTo)
                {
                    picTo.Image = pic;
                    ttForOk.SetToolTip(picTo, ttText);
                    _hasEnd = hasAdress;
                    if (!_hasEnd) return;
                    QueriesHandler.SetEndPoint(txt);
                    CheckAndConstructRoute();
                }
            }
            catch (UnknownPlacemark up)
            {
                MessageBox.Show(up.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox == edtFrom)
            {
                picFrom.Image = null;
            }
            else if (textBox == edtMid)
            {
                picMid.Image = null;
                btnMoreMid.Enabled = false;
            }
            else if (textBox == edtTo)
            {
                picTo.Image = null;
            }
        }

        private void WaybillForm_Enter(object sender, EventArgs e)
        {
            if (edtFrom.Text == "") 
                edtFrom.Text = QueriesHandler.GetStartPoint();
            if (edtFrom.Text != "")
            {
                _hasStart = true;
                picFrom.Image = new Bitmap(PicOk, new Size(16, 16));
            }

            if (edtTo.Text == "") 
                edtTo.Text = QueriesHandler.GetEndPoint();
            if (edtTo.Text != "")
            {
                _hasEnd = true;
                picTo.Image = new Bitmap(PicOk, new Size(16, 16));
            }
            CheckAndConstructRoute();
        }

        private void cmbGRZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            var grz = cmbGRZ.Text;
            var car = QueriesHandler.GetCarInfo(grz);

            ttForOk.SetToolTip(cmbGRZ, car);
            ttForOk.SetToolTip(picGrz, car);
            picGrz.Image = car != @"Не удалось определить автомобиль"
                ? new Bitmap(PicOk, new Size(16, 16))
                : new Bitmap(PicNotOk, new Size(16, 16));
        }

        private void edtDriverPhoneNumber_Leave(object sender, EventArgs e)
        {
            cmbDriverName.Items.Clear();
            cmbDriverName.Items.AddRange((QueriesHandler.GetNamesByNumber(
                Regex.Replace(edtDriverPhoneNumber.Text, "[^0-9]", ""))).Select(x => (object)x).ToArray());
        }
    }
}
