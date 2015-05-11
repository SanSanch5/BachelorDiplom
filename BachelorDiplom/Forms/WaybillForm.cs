﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

using BachelorLibAPI.Program;

namespace BachelorLibAPI.Forms
{
    public partial class WaybillForm : Form
    {
        private QueriesHandler QueriesHandler { get; set; }
        private readonly List<string> _consNames;
        private static readonly Bitmap PicOk = new Bitmap(@"..\..\Resources\Pictures\ok.png");
        private static readonly Bitmap PicNotOk = new Bitmap(@"..\..\Resources\Pictures\not_ok.png");

        public WaybillForm(QueriesHandler qh)
        {
            InitializeComponent();
//            TopMost = true;
            QueriesHandler = qh;
            _consNames = QueriesHandler.GetConsignmentsNames();

            if(QueriesHandler.HasMiddlePoints())
                MessageBox.Show(@"Отмеченные промежуточные точки уже учтены.", @"Информация");

            FillForm();
        }

        private void FillForm()
        {
            foreach (var cons in _consNames)
                cmbCons.Items.Add(cons);
            var screen = Screen.PrimaryScreen;
            Left = /*screen.WorkingArea.Right - Width - */25;
            Top = screen.WorkingArea.Top + 50;
        }

        private void AddNewWaybill(object sender, EventArgs e)
        {
            try
            {
                var num = Regex.Replace(edtDriverPhoneNumber.Text, "[^0-9]", "");
                var consName = cmbCons.Text;
                var driverName = edtDriverName.Text;
                var grz = edtGRZ.Text;

                if((num == "" || num.Length != 10) || consName == "" || driverName == "" || grz == "")
                    throw new FormatException("Звёздочкой (*) отмечены поля для обязательного заполнения!");
                if (!_consNames.Contains(consName))
                    throw new FormatException("Выберите груз.");

                if (edtMid.Text != "")
                {
                    MessageBox.Show(@"Добавьте введённую промежуточную точку или сотрите её из поля ввода", @"Внимание!");
                    return;
                }

                WindowState = FormWindowState.Minimized;
                if (!QueriesHandler.CheckBeforeAdding()) return;
                QueriesHandler.AddNewWaybill(driverName, num, grz, consName, dtpStart.Value, ttForOk.GetToolTip(edtFrom),
                    ttForOk.GetToolTip(edtTo));
                MessageBox.Show(@"Новая перевозка зарегистрирована.", @"Информация");
            }
            catch (FormatException ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, @"Предупреждение");
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                _needUpdate = false;
                MessageBox.Show(ex.Message, @"Ошибка");
            }
        }

        private void AddMoreMidCities(Object sender, EventArgs e)
        {
            QueriesHandler.SetMiddlePoint(edtMid.Text);
            edtMid.Text = "";
            CheckAndConstructRoute();
        }

        private bool _hasStart;
        private bool _hasEnd;

        private bool _needUpdate;
        private void CheckAndConstructRoute()
        {
            if (_hasStart && _hasEnd)
            {
                try
                {
                    btnNewWaybill.Enabled = true;
                    Cursor.Current = Cursors.WaitCursor;
                    QueriesHandler.ConstructShortTrack();
                    Cursor.Current = Cursors.Default;
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
            var hasAdress = QueriesHandler.CheckAdress(ref txt);
            textBox.Text = txt;

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
                if (_hasStart)
                {
                    QueriesHandler.SetStartPoint(txt);
                    CheckAndConstructRoute();
                }
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
                if (_hasEnd)
                {
                    QueriesHandler.SetEndPoint(txt);
                    CheckAndConstructRoute();
                }
            }
            Cursor.Current = Cursors.Default;
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
            edtFrom.Text = QueriesHandler.GetStartPoint();
            if (edtFrom.Text != "")
            {
                _hasStart = true;
                picFrom.Image = new Bitmap(PicOk, new Size(16, 16));
            }

            edtTo.Text = QueriesHandler.GetEndPoint();
            if (edtTo.Text != "")
            {
                _hasEnd = true;
                picTo.Image = new Bitmap(PicOk, new Size(16, 16));
            }
            if (_needUpdate) CheckAndConstructRoute();
            else _needUpdate = true;
        }
    }
}
