using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BachelorLibAPI.Program;

namespace BachelorLibAPI.Forms
{
    public partial class WaybillForm : Form
    {
        QueriesHandler _queriesHandler;
        private static List<string> _citiesNamesList;
        private List<string> _midCities;
        private List<string> _consNames;

        public WaybillForm(QueriesHandler qh)
        {
            InitializeComponent();
            _queriesHandler = qh;

            _midCities = new List<string>();
            _citiesNamesList = new List<string>();
            _consNames = QueriesHandler.GetConsignmentsNames();

            FillCombos();
        }

        private void FillCombos()
        {
            foreach (var city in _citiesNamesList)
            {
                cmbStart.Items.Add(city);
                cmbMid.Items.Add(city);
                cmbArr.Items.Add(city);
            }

            foreach (var cons in _consNames)
                cmbCons.Items.Add(cons);
        }

        private void AddNewWaybill(Object sender, EventArgs e)
        {
            /*
             * на форме телефон водителя, наименование груза, названия начального и конечного пунктов и время отправления
             * дописать, чтоб название груза выбирать из выпадающего списка.
             */
            try
            {
                var num = Regex.Replace(edtDriverPhoneNumber.Text, "[^0-9]", "");
                var consName = cmbCons.Text;
                var firstCity = cmbStart.Text;
                var lastCity = cmbArr.Text;

                if((num == "" || num.Length != 10) || consName == "" || firstCity == "" || lastCity == "")
                    throw new FormatException("Звёздочкой (*) отмечены поля для обязательного заполнения!");

                var citiesLst = new List<string>();
                citiesLst.Add(firstCity);

                var city = cmbMid.Text;
                if (_citiesNamesList.Contains(city))
                    _midCities.Add(city);

                citiesLst.AddRange(_midCities);
                citiesLst.Add(lastCity);

                var dt = dtpStart.Value;

                _queriesHandler.AddNewWaybill(num, consName, citiesLst, dt);
                MessageBox.Show("Новая перевозка зарегистрирована.", "Информация");
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Нет данных в базе!");
            }
        }

        private void AddMoreMidCities(Object sender, EventArgs e)
        {
            var city = cmbMid.Text;
            if (_citiesNamesList.Contains(city))
            {
                _midCities.Add(city);
                cmbMid.Text = "";
            }
            else
            {
                MessageBox.Show("Нет города " + city + " в базе данных", "Предупреждение");
            }
        }
    }
}
