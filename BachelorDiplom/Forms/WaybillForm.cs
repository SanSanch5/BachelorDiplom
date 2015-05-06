using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BachelorLibAPI.Forms
{
    public partial class WaybillForm : Form
    {
        QueriesHandler _queriesHandler;
        private static List<string> citiesNamesList;
        private List<string> midCities;
        private List<string> consNames;

        public WaybillForm(QueriesHandler qh)
        {
            InitializeComponent();
            _queriesHandler = qh;

            midCities = new List<string>();
            citiesNamesList = new List<string>();
            consNames = _queriesHandler.GetConsignmentsNames();

            FillCombos();
        }

        private void FillCombos()
        {
            foreach (var city in citiesNamesList)
            {
                cmbStart.Items.Add(city);
                cmbMid.Items.Add(city);
                cmbArr.Items.Add(city);
            }

            foreach (var cons in consNames)
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
                string num = System.Text.RegularExpressions.Regex.Replace(edtDriverPhoneNumber.Text, "[^0-9]", "");
                string consName = cmbCons.Text;
                string firstCity = cmbStart.Text;
                string lastCity = cmbArr.Text;

                if((num == "" || num.Length != 10) || consName == "" || firstCity == "" || lastCity == "")
                    throw new FormatException("Звёздочкой (*) отмечены поля для обязательного заполнения!");

                List<string> citiesLst = new List<string>();
                citiesLst.Add(firstCity);

                string city = cmbMid.Text;
                if (citiesNamesList.Contains(city))
                    midCities.Add(city);

                citiesLst.AddRange(midCities);
                citiesLst.Add(lastCity);

                DateTime dt = dtpStart.Value;

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
            string city = cmbMid.Text;
            if (citiesNamesList.Contains(city))
            {
                midCities.Add(city);
                cmbMid.Text = "";
            }
            else
            {
                MessageBox.Show("Нет города " + city + " в базе данных", "Предупреждение");
            }
        }
    }
}
